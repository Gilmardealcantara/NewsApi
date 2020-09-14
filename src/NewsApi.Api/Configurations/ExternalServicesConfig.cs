using Microsoft.Extensions.DependencyInjection;
using NewsApi.Application.Services.External;
using NewsApi.Services.ExternalApis;

namespace NewsApi.Api.Configurations
{
    public static class ExternalServicesConfig
    {
        public static void ConfigureExternalServices(this IServiceCollection services)
        {
            services.AddScoped<IImageStorageService, S3Service>();
        }
    }
}