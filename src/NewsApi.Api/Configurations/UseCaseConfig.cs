using Microsoft.Extensions.DependencyInjection;
using NewsApi.Domain.UseCases;
using NewsApi.Domain.UseCases.Interfaces;

namespace NewsApi.Api.Configurations
{
    public static class UseCaseConfig
    {
        public static void ConfigureUseCase(this IServiceCollection services)
        {
            services.AddTransient<IListNewsUseCase, ListNewsUseCase>();
            services.AddTransient<IGetNewsByIdUseCase, GetNewsByIdUseCase>();
            // services.AddTransient<ICreateNewsUseCase, CreateNewsUseCase>();
            // services.AddTransient<IUpdateNewsUseCase, UpdateNewsUseCase>();
        }
    }
}