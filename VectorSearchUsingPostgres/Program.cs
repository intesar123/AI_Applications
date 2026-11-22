using Npgsql;
using OpenAI.Embeddings;
using Pgvector;
using VectorSearchUsingPostgres;

var path = Path.Combine(AppContext.BaseDirectory,"appSettings.json");
var json = File.ReadAllText(path);
var configuration = System.Text.Json.JsonSerializer.Deserialize<Configuration>(json)!;

var embeddingService = new EmbeddingService(configuration.OpenAI.Endpoint,configuration.OpenAI.ApiKey, configuration.OpenAI.EmbeddingDeployment);
PostgresService postgresService = new PostgresService(configuration.PostgresDB.ConnectionString, embeddingService);

// create table if it does not exist
await postgresService.CreateTable();
// ------------ insert data to the documents table - uncomment to insert sample data
//await postgresService.InsertData(string.Empty);

// ------------ search for similar documents
var queryText = "How do I store vectors in Postgres?";
var results = await postgresService.SearchAsync(queryText);
foreach (var result in results)
{
    Console.WriteLine(result);
}
