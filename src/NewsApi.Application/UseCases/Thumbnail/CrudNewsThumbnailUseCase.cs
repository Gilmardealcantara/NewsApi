using System.IO;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Extensions.Logging;
using NewsApi.Application.Dtos;
using NewsApi.Application.Services.ExternalApis;
using NewsApi.Application.Services.Repositories;
using NewsApi.Application.Shared;
using NewsApi.Application.UseCases.Interfaces.Thumbnail;

namespace NewsApi.Application.UseCases.Thumbnail
{
    public class CrudNewsThumbnailUseCase : UseCaseBase<ThumbnailRequest, string>, ICrudNewsThumbnailUseCase
    {
        private readonly INewsRepository _repository;
        private readonly IImageStorageService _imageService;
        public CrudNewsThumbnailUseCase(
            INewsRepository repository,
            IImageStorageService imageService,
            ILogger<CrudNewsThumbnailUseCase> logger,
            IValidator<ThumbnailRequest> validator)
            : base(logger, validator, ("07", "Error unexpected when add thumbnail in news"))
            => (_repository, _imageService) = (repository, imageService);

        protected override async Task<UseCaseResponse<string>> OnExecute(ThumbnailRequest request)
        {
            var news = await _repository.GetById(request.NewsId);
            if (news is null)
                return _response.SetResourceNotFountError("0.10", $"News with id = {request.NewsId} not exists");

            var extension = Path.GetExtension(request.FileName);
            var keyName = $"{news.Id}{extension}";
            if (request.Type == ThumbnailRequestType.Create)
                news.ThumbnailURL = await _imageService.Upload(keyName, request.FileLocalPath);

            if (request.Type == ThumbnailRequestType.Update)
                news.ThumbnailURL = await _imageService.Update(keyName, request.FileLocalPath);

            if (request.Type == ThumbnailRequestType.Delete)
            {
                await _imageService.Delete(keyName);
                news.ThumbnailURL = null;
            }

            await _repository.Update(news);

            return _response.SetResult(news.ThumbnailURL);
        }
    }
}