using Npgsql;

namespace VectorSearchUsingPostgres
{
    public class PostgresService
    {
        private readonly string _connectionString;

        public PostgresService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task InsertData(string input)
        {
            
        }
    }
}