using System.ClientModel;
using OpenAI;
using OpenAI.Embeddings;

namespace VectorSearchUsingPostgres
{
    public class EmbeddingService
    {
        private readonly EmbeddingClient _embeddingClient;
        public EmbeddingService(string endpoint, string key, string deploymentName)
        {
            var clientOptions = new OpenAIClientOptions
            {
                Endpoint = new Uri(endpoint)
            };

            _embeddingClient = new EmbeddingClient(
                        model: deploymentName,
                        credential: new ApiKeyCredential(key),
                        options: clientOptions
                    );
        }

        public async Task<float[]> GenerateEmbeddingAsync(string input)
        {
            OpenAIEmbedding response = await _embeddingClient.GenerateEmbeddingAsync(input);
            ReadOnlyMemory<float> vector = response.ToFloats();
            return vector.ToArray();
        }
    }
}