using Microsoft.Azure.Cosmos;

namespace VectorDBTest
{
    public class VectorSearchService
    {
        private readonly Container _container;

        public VectorSearchService(Container container)
        {
            _container = container;
        }

        public async Task SearchAsync(float[] queryVector, int topK = 3)
        {
            string sql = "SELECT TOP @topK c.id, c.name, c.description, c.category, " +
                         "VectorDistance(c.descriptionVector, @queryVector) AS distance FROM c " +
                         "ORDER BY VectorDistance(c.descriptionVector, @queryVector)";
            QueryDefinition queryDefinition = new QueryDefinition(sql)
                .WithParameter("@topK", topK)
                .WithParameter("@queryVector", queryVector);
            FeedIterator<dynamic> resultSet = _container.GetItemQueryIterator<dynamic>(queryDefinition);

            while (resultSet.HasMoreResults)
            {
                FeedResponse<dynamic> response = await resultSet.ReadNextAsync();
                foreach (var item in response)
                {
                    Console.WriteLine($"Id: {item.id}, Name: {item.name}, Description: {item.description}, Category: {item.category}, Distance: {item.distance}");
                }
            }
        }
    }
}