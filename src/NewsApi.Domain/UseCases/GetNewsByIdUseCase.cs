using System;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Extensions.Logging;
using NewsApi.Domain.Dtos;
using NewsApi.Domain.Services.Repositories;
using NewsApi.Domain.Shared;
using NewsApi.Domain.UseCases.Interfaces;

namespace NewsApi.Domain.UseCases
{
    public class GetNewsByIdUseCase : UseCaseBase<Guid, NewsResponse>, IGetNewsByIdUseCase
    {
        private readonly INewsRepository _repository;
        public GetNewsByIdUseCase(
            ILogger<GetNewsByIdUseCase> logger,
            IValidator<Guid> validator,
            INewsRepository repository)
            : base(logger, validator, ("04", "Error unexpected when get news by id"))
            => _repository = repository;
        protected override async Task<UseCaseResponse<NewsResponse>> OnExecute(Guid request)
        {
            var news = await _repository.GetById(request);
            if (news is null)
                return this._response.SetResourceNotFountError();
            var result = new NewsResponse
            {
                Id = news.Id,
                Title = news.Title,
                Content = news.Content,
                ThumbnailURL = news.ThumbnailURL,
                Comments = news.Comments?.Take(10)
            };
            return this._response.SetResult(result);
        }
    }
}