using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using NewsApi.Application.Services.Repositories;
using NewsApi.DomainServices.Repositories;

namespace NewsApi.Api.Configurations
{
    public static class RepositoriesConfig
    {
        public static void ConfigureRepository(this IServiceCollection services, ApplicationConfig config)
        {
            services.AddTransient<IDbConnection>((sp) => new SqlConnection(config.Database.ConnectionString));
            services.AddScoped<INewsRepository, NewsRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
        }
    }
}