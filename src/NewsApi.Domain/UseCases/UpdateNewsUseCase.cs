using System;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Extensions.Logging;
using NewsApi.Domain.Dtos;
using NewsApi.Domain.Services.ExternalApis;
using NewsApi.Domain.Services.Repositories;
using NewsApi.Domain.Shared;

namespace NewsApi.Domain.UseCases
{
    public class UpdateNewsUseCase : UseCaseBase<UpdateNewsRequest, Guid>
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

        protected override async Task<UseCaseResponse<Guid>> OnExecute(UpdateNewsRequest request)
        {
            var news = await _repository.GetById(request.Id);
            if (news is null)
                return _response.SetResourceNotFountError();

            var updatedNews = request.ToNews(news);

            string thumbnailRemoteURL = "";
            if (news.ThumbnailURL is null && request.ThumbnailLocalURL != null)
                thumbnailRemoteURL = await _imageStorage.Upload(news.Id.ToString(), request.ThumbnailLocalURL);

            if (news.ThumbnailURL != null && request.ThumbnailLocalURL != null)
                thumbnailRemoteURL = await _imageStorage.Update(news.Id.ToString(), request.ThumbnailLocalURL);

            news.ThumbnailURL = thumbnailRemoteURL;

            await _repository.Update(updatedNews);
            return _response.SetResult(updatedNews.Id);
        }
    }
}