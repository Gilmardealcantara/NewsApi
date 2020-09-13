using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using NewsApi.Application.Services.Repositories;
using NewsApi.Application.Shared;
using NewsApi.Services.Factories;
using NewsApi.Services.Repositories;

namespace NewsApi.Api.Configurations
{
    public static class RepositoriesConfig
    {
        public static void ConfigureRepository(this IServiceCollection services, ApplicationConfig config)
        {

            services.AddScoped<IConnectionFactory, SqlServerConnectionFactory>();
            services.AddScoped<INewsRepository, NewsRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
        }
    }
}