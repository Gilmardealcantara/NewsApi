using System;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Extensions.Logging;
using NewsApi.Domain.Dtos;
using NewsApi.Domain.Services.Repositories;
using NewsApi.Domain.Shared;

namespace NewsApi.Domain.UseCases
{
    public class UpdateNewsUseCase : UseCaseBase<UpdateNewsRequest, Guid>
    {
        INewsRepository _repository;
        public UpdateNewsUseCase(
            ILogger<UpdateNewsUseCase> logger,
            IValidator<UpdateNewsRequest> validator,
            INewsRepository repository) : base(logger, validator, ("03", "Error unexpected when update news"))
           => _repository = repository;

        protected override async Task<UseCaseResponse<Guid>> OnExecute(UpdateNewsRequest request)
        {
            var news = request.ToNews();
            await _repository.Update(news);
            return _response.SetResult(news.Id);
        }
    }
}