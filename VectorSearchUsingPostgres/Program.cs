using Npgsql;
using OpenAI.Embeddings;
using Pgvector;

var connectionString = "Host=localhost;Port=5432;Database=vectordb;Username=postgres;Password=your_password";
var openAiApiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY")!;

// 1. Configure Npgsql with pgvector support
var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
dataSourceBuilder.UseVector();
await using var dataSource = dataSourceBuilder.Build();

// 2. Initialize OpenAI EmbeddingClient
var embeddingClient = new EmbeddingClient("text-embedding-3-small", openAiApiKey);

// 3. Generate and insert embeddings
string[] sampleTexts = [
    "PostgreSQL is an open-source relational database.",
    "pgvector adds vector similarity search to Postgres.",
    "C# and .NET are great for building scalable backends."
];

await using var conn = await dataSource.OpenConnectionAsync();

foreach (var text in sampleTexts)
{
    OpenAIEmbedding embeddingResult = await embeddingClient.GenerateEmbeddingAsync(text);
    var vector = new Vector(embeddingResult.ToFloats());

    await using var insertCmd = new NpgsqlCommand(
        "INSERT INTO documents (content, embedding) VALUES ($1, $2)", conn);
    insertCmd.Parameters.AddWithValue(text);
    insertCmd.Parameters.AddWithValue(vector);
    await insertCmd.ExecuteNonQueryAsync();
}

// 4. Query by semantic similarity using Cosine Distance (<=>)
var queryText = "How do I store vectors in Postgres?";
OpenAIEmbedding queryEmbeddingResult = await embeddingClient.GenerateEmbeddingAsync(queryText);
var queryVector = new Vector(queryEmbeddingResult.ToFloats());

await using var searchCmd = new NpgsqlCommand(
    "SELECT content, 1 - (embedding <=> $1) AS similarity FROM documents ORDER BY embedding <=> $1 LIMIT 3", conn);
searchCmd.Parameters.AddWithValue(queryVector);

await using var reader = await searchCmd.ExecuteReaderAsync();
while (await reader.ReadAsync())
{
    Console.WriteLine($"Score: {reader.GetDouble(1):F4} | Content: {reader.GetString(0)}");
}
