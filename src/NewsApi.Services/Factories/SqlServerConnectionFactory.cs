using System.Data;
using System.Data.SqlClient;
using NewsApi.Application.Services.Repositories;
using NewsApi.Application.Shared;

namespace NewsApi.Services.Factories
{
    public class SqlServerConnectionFactory : IConnectionFactory
    {
        private readonly ApplicationConfig _config;
        public SqlServerConnectionFactory(ApplicationConfig config)
            => _config = config;

        public IDbConnection GetConnection()
            => new SqlConnection(_config.Database.ConnectionString);
    }
}