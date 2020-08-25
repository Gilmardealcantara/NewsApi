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
            // services.AddSingleton<IGetNewsByIdUseCase, GetNewsByIdUseCase>();
            // services.AddSingleton<ICreateNewsUseCase, CreateNewsUseCase>();
            // services.AddSingleton<IUpdateNewsUseCase, UpdateNewsUseCase>();
        }
    }
}