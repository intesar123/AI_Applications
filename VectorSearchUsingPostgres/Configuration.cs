namespace VectorSearchUsingPostgres;

public class Configuration
{
    public PostgresDB PostgresDB { get; set; } = new();
    public OpenAI OpenAI { get; set; } = new();
}

public class PostgresDB
{
    public string ConnectionString { get; set; } = string.Empty;
}

public class OpenAI
{
    public string ApiKey { get; set; } = string.Empty;
    public string Endpoint { get; set; } = string.Empty;
    public string EmbeddingDeployment { get; set; } = string.Empty;
}