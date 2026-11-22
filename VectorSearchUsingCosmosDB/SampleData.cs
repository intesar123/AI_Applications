namespace VectorDBTest
{
    public static class SampleData
    {
        public static List<Product> GetProducts()
        {
            return new List<Product>
    {
        new Product
        {
            Id = "1",
            Name = "Developer Laptop",
            Description =
                "High performance laptop for software development, coding and programming.",
            Category = "Computer"
        },

        new Product
        {
            Id = "2",
            Name = "Smartphone",
            Description =
                "Modern smartphone with excellent camera, fast processor and long battery life.",
            Category = "Mobile"
        },

        new Product
        {
            Id = "3",
            Name = "Wireless Headphones",
            Description =
                "Wireless noise cancelling headphones for music, travel and entertainment.",
            Category = "Audio"
        },

        new Product
        {
            Id = "4",
            Name = "Gaming PC",
            Description =
                "Powerful desktop computer designed for gaming, graphics and high performance applications.",
            Category = "Computer"
        }
    };
        }
    }
}