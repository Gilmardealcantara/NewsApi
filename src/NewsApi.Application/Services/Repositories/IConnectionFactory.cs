using System.Data;

namespace NewsApi.Application.Services.Repositories
{
    public interface IConnectionFactory
    {
        IDbConnection GetConnection();
    }
}