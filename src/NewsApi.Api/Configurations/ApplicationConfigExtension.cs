using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewsApi.Application.Shared;

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
}