using System;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Extensions.Logging;
using NewsApi.Domain.Dtos;
using NewsApi.Domain.Entities;
using NewsApi.Domain.Services.ExternalApis;
using NewsApi.Domain.Services.Repositories;
using NewsApi.Domain.Shared;

namespace NewsApi.Domain.UseCases
{
    public class UpdateNewsUseCase : UseCaseBase<UpdateNewsRequest, News>
    {
        INewsRepository _repository;
        IImageStorageService _imageStorage;

        public UpdateNewsUseCase(
            ILogger<UpdateNewsUseCase> logger,
            IValidator<UpdateNewsRequest> validator,
            INewsRepository repository,
            IImageStorageService imageStorage)
            : base(logger, validator, ("03", "Error unexpected when update news"))
           => (_repository, _imageStorage) = (repository, imageStorage);

        protected override async Task<UseCaseResponse<News>> OnExecute(UpdateNewsRequest request)
        {
            var oldNews = await _repository.GetById(request.Id);
            if (oldNews is null)
                return _response.SetResourceNotFountError();

            string thumbnailRemoteURL = null;
            if (oldNews.ThumbnailURL is null && request.ThumbnailLocalURL != null)
                thumbnailRemoteURL = await _imageStorage.Upload(oldNews.Id.ToString(), request.ThumbnailLocalURL);

            if (oldNews.ThumbnailURL != null && request.ThumbnailLocalURL != null)
                thumbnailRemoteURL = await _imageStorage.Update(oldNews.Id.ToString(), request.ThumbnailLocalURL);

            var updatedNews = request.ToNews(oldNews);
            updatedNews.ThumbnailURL = thumbnailRemoteURL ?? oldNews.ThumbnailURL;

            await _repository.Update(updatedNews);
            return _response.SetResult(updatedNews);
        }
    }
}