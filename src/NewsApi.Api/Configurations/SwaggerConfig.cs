using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace NewsApi.Api.Configurations
{
    public static class SwaggerConfig
    {
        public static void ConfigureSwagger(this IServiceCollection services)
        {

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo { Title = "News API", Version = "v1" });

            });
        }

        public static void UseSwaggerExtension(this IApplicationBuilder app)
        {

            app.UseSwagger(option => option.RouteTemplate = "swagger/{documentName}/swagger.json");
            app.UseSwaggerUI(option => option.SwaggerEndpoint("v1/swagger.json", "News API"));
        }
    }
}