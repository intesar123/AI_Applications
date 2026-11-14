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
            string sql = $"Select TOP @topK, c.id, c.name, c.description, c.category, VectorDistance(c.descriptionVector, @queryVector) as distance from c"
                         +" ORDER BY distance ASC";

//                               string sql = """
//  SELECT TOP @topK
//      c.id,
//      c.name,
//      c.description,
//      c.category,
//      VectorDistance(
//          c.descriptionVector,
//          @queryVector
//      ) AS distance
//  FROM c
//  ORDER BY VectorDistance(
//      c.descriptionVector,
//      @queryVector
//  )
//  """;
            QueryDefinition queryDefinition = new QueryDefinition(sql)
                .WithParameter("@topK", topK)
                .WithParameter("@queryVector", queryVector);
            FeedIterator<Product> resultSet = _container.GetItemQueryIterator<Product>(queryDefinition);

            while (resultSet.HasMoreResults)
            {
                FeedResponse<Product> response = await resultSet.ReadNextAsync();
                foreach (var item in response)
                {
                    Console.WriteLine($"Id: {item.Id}, Name: {item.Name}, Description: {item.Description}, Category: {item.Category}");
                }
            }
        }
    }
}