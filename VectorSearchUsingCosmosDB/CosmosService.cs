
using System.Collections.ObjectModel;
using Microsoft.Azure.Cosmos;

namespace VectorDBTest
{
    public class CosmosService
    {
        private readonly CosmosClient _client;
        private readonly Database _database;
        private readonly Container _container;

        public CosmosService(string endpointUri, string key, string database, string container)
        {
            _client = new CosmosClient(endpointUri, key);

            _database = _client.CreateDatabaseIfNotExistsAsync(database).GetAwaiter().GetResult();

            var containerProperties = new ContainerProperties(container, "/category");

            var embeddings = new Collection<Embedding>
            {
                new Embedding
                {
                    Path = "/descriptionVector",
                    DistanceFunction = DistanceFunction.Cosine,
                    Dimensions = 1536
                }
            };

            containerProperties.VectorEmbeddingPolicy = new VectorEmbeddingPolicy(embeddings);

            // Vector Index
            containerProperties.IndexingPolicy.VectorIndexes = new Collection<VectorIndexPath>
            {
                new VectorIndexPath
                {
                    Path = "/descriptionVector",
                    Type = VectorIndexType.DiskANN
                }
            };

            containerProperties.IndexingPolicy.IncludedPaths.Add(new IncludedPath
            {
                Path = "/*"
            });

            // Don't use normal normal indexing for the vector array
            containerProperties.IndexingPolicy.ExcludedPaths.Add(new ExcludedPath
            {
                Path = "/descriptionVector/*"
            });

            _container = _database.CreateContainerIfNotExistsAsync(containerProperties).GetAwaiter().GetResult();
        }

        public async Task InsertAsync(Product product)
        {
            await _container.CreateItemAsync(product,new PartitionKey(product.Category));
        }

        public Container GetContainer()
        {
            return _container;
        }
    }
}