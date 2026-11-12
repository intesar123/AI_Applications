

using System.ClientModel;
using OpenAI;
using OpenAI.Embeddings;

var path = Path.Combine(AppContext.BaseDirectory, "appSettings.json");
var json = File.ReadAllText(path);
var config = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(json);

var openAiConfig = config["OpenAI"];

var openAiApiKey = openAiConfig["ApiKey"];
var openAiEndpoint = openAiConfig["Endpoint"];
var openAiEmbeddingDeployment = openAiConfig["EmbeddingDeployment"];

var clientOptions = new OpenAIClientOptions
{
    Endpoint = new Uri(openAiEndpoint)
};

var embeddingClient = new EmbeddingClient(
            model: openAiEmbeddingDeployment, // "gemini-embedding-001" Or "gemini-embedding-2"
            credential: new ApiKeyCredential(openAiApiKey),
            options: clientOptions
        );

OpenAIEmbedding embedding = await embeddingClient.GenerateEmbeddingAsync(
           "How do I pair Gemini embeddings with Groq?"
       );

ReadOnlyMemory<float> vector = embedding.ToFloats();
Console.WriteLine($"Generated embedding with {vector.Length} dimensions.");