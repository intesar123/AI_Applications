using Newtonsoft.Json;

namespace VectorDBTest
{
    public class Product
    {
        [JsonProperty("id")]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }
        public float[] descriptionVector { get; set; }
    }
}