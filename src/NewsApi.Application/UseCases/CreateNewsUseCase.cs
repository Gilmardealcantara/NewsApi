using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using FluentValidation;
using NewsApi.Application.Dtos;
using NewsApi.Application.Entities;
using NewsApi.Application.Services.Repositories;
using NewsApi.Application.Shared;
using NewsApi.Application.Services.ExternalApis;
using NewsApi.Application.UseCases.Interfaces;

namespace NewsApi.Application.UseCases
{
    public class CreateNewsUseCase : UseCaseBase<CreateNewsRequest, News>, ICreateNewsUseCase
    {
        INewsRepository _repository;
        IImageStorageService _imageStorage;
        public CreateNewsUseCase(
            ILogger<CreateNewsUseCase> logger,
            IValidator<CreateNewsRequest> validator,
            INewsRepository repository,
            IImageStorageService imageStorage)
            : base(logger, validator, ("01", "Error unexpected when create news"))
           => (_repository, _imageStorage) = (repository, imageStorage);

        protected override async Task<UseCaseResponse<News>> OnExecute(CreateNewsRequest request)
        {
            var news = request.ToNews();
            if (request.ThumbnailLocalURL != null)
            {
                var thumbnailRemoteURL = await _imageStorage.Upload(news.Id.ToString(), request.ThumbnailLocalURL);
                news.ThumbnailURL = thumbnailRemoteURL;
            }
            await _repository.Save(news);
            return _response.SetResult(news);
        }
    }
}