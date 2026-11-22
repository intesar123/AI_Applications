

using System.ClientModel;
using OpenAI;
using OpenAI.Embeddings;
using VectorDBTest;

var path = Path.Combine(AppContext.BaseDirectory, "appSettings.json");
var json = File.ReadAllText(path);
var config = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(json);

var cosmosDbConfig = config["CosmosDb"];
var openAiConfig = config["OpenAI"];


var cosmosDbAccountEndpoint = cosmosDbConfig["AccountEndpoint"];
var cosmosDbAccountKey = cosmosDbConfig["AccountKey"];
var cosmosDbDatabaseName = cosmosDbConfig["DatabaseName"];
var cosmosDbContainerName = cosmosDbConfig["ContainerName"];

var openAiApiKey = openAiConfig["ApiKey"];
var openAiEndpoint = openAiConfig["Endpoint"];
var openAiEmbeddingDeployment = openAiConfig["EmbeddingDeployment"];

// List<Product> products = SampleData.GetProducts();

var embeddingService = new EmbeddingService(openAiEndpoint, openAiApiKey, openAiEmbeddingDeployment);
var cosmosService = new CosmosService(cosmosDbAccountEndpoint, cosmosDbAccountKey, cosmosDbDatabaseName, cosmosDbContainerName);
// // Insert products into the database or perform other operations
// foreach (var product in products)
// {
//     product.descriptionVector = await embeddingService.GenerateEmbeddingAsync(product.Description);
//     Console.WriteLine($"Generated embedding for product: {product.Name} with {product.descriptionVector.Length} dimensions.");

//     await cosmosService.InsertAsync(product);

// }
// Console.WriteLine("All products have been processed and inserted into Cosmos DB.");


//-----------------Search Data ---------------------------------

var vectorSearchService = new VectorSearchService(cosmosService.GetContainer());

Console.WriteLine();
Console.WriteLine("Enter a search query:");

// Example search query
var query = Console.ReadLine();

if (!string.IsNullOrWhiteSpace(query))
{
    var queryVector = await embeddingService.GenerateEmbeddingAsync(query);

    Console.WriteLine($"Generated query vector with {queryVector.Length} dimensions.");

    Console.WriteLine();
    Console.WriteLine("Vector Search Results");
    Console.WriteLine("=====================");

    await vectorSearchService.SearchAsync(queryVector, topK: 5);

}