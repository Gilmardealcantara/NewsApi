using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NewsApi.Api.Configurations
{
    public static class ApplicationConfigExtension
    {
        public static ApplicationConfig ConfigureApp(this IServiceCollection services, IConfiguration config)
        {
            var appConfig = config.Get<ApplicationConfig>();
            appConfig.Database.ConnectionString = config.GetConnectionString("DefaultConnection");
            services.AddSingleton(appConfig);
            return appConfig;
        }
    }

    public class ApplicationConfig
    {
        public ApplicationConfig()
        {
            Database = new Database();
        }

        public Database Database { get; set; }
    }

    public class Database
    {
        public string ConnectionString { get; set; }
    }
}