using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using FluentValidation;
using NewsApi.Domain.Dtos;
using NewsApi.Domain.Entities;
using NewsApi.Domain.Services.Repositories;
using NewsApi.Domain.Shared;
using NewsApi.Domain.Services.ExternalApis;

namespace NewsApi.Domain.UseCases
{
    public class CreateNewsUseCase : UseCaseBase<CreateNewsRequest, News>
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