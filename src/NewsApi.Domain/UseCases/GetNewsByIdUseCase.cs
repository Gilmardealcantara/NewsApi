using System;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Extensions.Logging;
using NewsApi.Domain.Dtos;
using NewsApi.Domain.Services.Repositories;
using NewsApi.Domain.Shared;

namespace NewsApi.Domain.UseCases
{
    public class GetNewsByIdUseCase : UseCaseBase<Guid, NewsResponse>
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
            var result = new NewsResponse
            {
                Id = news.Id,
                Title = news.Title,
                Content = news.Content
            };
            return this._response.SetResult(result);
        }
    }
}