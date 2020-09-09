using System;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Extensions.Logging;
using NewsApi.Application.Dtos;
using NewsApi.Application.Entities;
using NewsApi.Application.Services.Repositories;
using NewsApi.Application.Shared;
using NewsApi.Application.UseCases.Interfaces;

namespace NewsApi.Application.UseCases
{
    public class UpdateNewsUseCase : UseCaseBase<UpdateNewsRequest, News>, IUpdateNewsUseCase
    {
        INewsRepository _repository;

        public UpdateNewsUseCase(
            ILogger<UpdateNewsUseCase> logger,
            IValidator<UpdateNewsRequest> validator,
            INewsRepository repository)
            : base(logger, validator, ("03", "Error unexpected when update news"))
           => _repository = repository;

        protected override async Task<UseCaseResponse<News>> OnExecute(UpdateNewsRequest request)
        {
            var oldNews = await _repository.GetById(request.Id);
            if (oldNews is null)
                return _response.SetResourceNotFountError();

            var updatedNews = request.ToNews(oldNews);

            await _repository.Update(updatedNews);
            return _response.SetResult(updatedNews);
        }
    }
}