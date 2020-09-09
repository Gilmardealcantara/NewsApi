using Microsoft.Extensions.DependencyInjection;
using NewsApi.Application.UseCases;
using NewsApi.Application.UseCases.Interfaces;

namespace NewsApi.Api.Configurations
{
    public static class UseCaseConfig
    {
        public static void ConfigureUseCase(this IServiceCollection services)
        {
            services.AddTransient<IListNewsUseCase, ListNewsUseCase>();
            services.AddTransient<IGetNewsByIdUseCase, GetNewsByIdUseCase>();
            services.AddTransient<ICreateNewsUseCase, CreateNewsUseCase>();
            services.AddTransient<IUpdateNewsUseCase, UpdateNewsUseCase>();
        }
    }
}