using Microsoft.Extensions.DependencyInjection;
using NewsApi.Application.UseCases.Interfaces.News;
using NewsApi.Application.UseCases.Interfaces.Thumbnail;
using NewsApi.Application.UseCases.News;
using NewsApi.Application.UseCases.Thumbnail;

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
            services.AddTransient<ICreateOrUpdateThumbnailUseCase, CreateOrUpdateThumbnailUseCase>();
        }
    }
}