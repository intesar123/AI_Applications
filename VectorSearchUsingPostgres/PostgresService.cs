using Npgsql;
using OpenAI.Embeddings;
using Pgvector;

namespace VectorSearchUsingPostgres
{
    public class PostgresService
    {
        private readonly string _connectionString;
        private readonly NpgsqlDataSource _dataSource;
        private readonly EmbeddingService _embeddingService;

        public PostgresService(string connectionString, EmbeddingService embeddingService)
        {
            _connectionString = connectionString;
            _embeddingService = embeddingService;
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
            dataSourceBuilder.UseVector();
            _dataSource = dataSourceBuilder.Build();
        }

        // create table on postgress for vector db
        public async Task CreateTable()
        {
            await using var conn = await _dataSource.OpenConnectionAsync();
            await using var createTableCmd = new NpgsqlCommand(
                "CREATE TABLE IF NOT EXISTS documents (content TEXT, embedding VECTOR)", conn);
            await createTableCmd.ExecuteNonQueryAsync();
        }

        // insert sample data into the documents table
        public async Task InsertData(string input)
        {
            string[] sampleTexts = [
                "PostgreSQL is an open-source relational database.",
                "pgvector adds vector similarity search to Postgres.",
                "C# and .NET are great for building scalable backends."
            ];
            await using var conn = await _dataSource.OpenConnectionAsync();

            foreach (var text in sampleTexts)
            {
                //OpenAIEmbedding embeddingResult = await _embeddingService.GenerateEmbeddingAsync(text);
                var vector = await _embeddingService.GenerateEmbeddingAsync(text);

                await using var insertCmd = new NpgsqlCommand(
                    "INSERT INTO documents (content, embedding) VALUES ($1, $2)", conn);
                insertCmd.Parameters.AddWithValue(text);
                insertCmd.Parameters.AddWithValue(vector);
                await insertCmd.ExecuteNonQueryAsync();
            }

        }

        public async Task<List<string>> SearchAsync(string queryText)
        {
            var vectorData = await _embeddingService.GenerateEmbeddingAsync(queryText);

            var results = new List<string>();

            await using var conn = await _dataSource.OpenConnectionAsync();
            await using var searchCmd = new NpgsqlCommand(
                "SELECT content FROM documents ORDER BY embedding <=> $1 LIMIT 5", conn);
            searchCmd.Parameters.AddWithValue(vectorData);

            await using var reader = await searchCmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                results.Add(reader.GetString(0));
            }

            return results;
        }
    }
}