using System;
using System.Threading.Tasks;
using NewsApi.Domain.Dtos;
using NewsApi.Domain.Entities;
using NewsApi.Domain.Shared;
using FluentValidation;
using NewsApi.Domain.Services.Repositories;
using Microsoft.Extensions.Logging;

namespace NewsApi.Domain.UseCases
{
    public class CreateNewsUseCase : UseCaseBase<CreateNewsRequest, News>
    {
        INewsRepository _repository;
        public CreateNewsUseCase(
            ILogger<CreateNewsUseCase> logger,
            IValidator<CreateNewsRequest> validator,
            INewsRepository repository) : base(logger, validator, ("01", "Error unexpected when create news"))
        {
            _repository = repository;
        }

        protected override async Task<UseCaseResponse<News>> OnExecute(CreateNewsRequest request)
        {
            var news = request.ToNews();
            await _repository.Save(news);
            return _response.SetResult(news);
        }
    }
}