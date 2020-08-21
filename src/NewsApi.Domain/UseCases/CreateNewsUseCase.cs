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
    public class CreateNewsUseCase : IUseCase<CreateNewsRequest, News>
    {
        ILogger<CreateNewsUseCase> _logger;
        IValidator<CreateNewsRequest> _validator;
        INewsRepository _repository;

        public CreateNewsUseCase(
            ILogger<CreateNewsUseCase> logger,
            IValidator<CreateNewsRequest> validator,
            INewsRepository repository)
        {
            _logger = logger;
            _validator = validator;
            _repository = repository;
        }

        public async Task<UseCaseResponse<News>> Execute(CreateNewsRequest request)
        {
            var response = new UseCaseResponse<News>();
            try
            {
                var validate = _validator.Validate(request);
                if (!validate.IsValid)
                    return response.SetRequestValidatorError(validate.Errors);

                var news = request.ToNews();
                await _repository.Save(news);
                return response.SetResult(news);
            }
            catch (Exception e)
            {
                var errorMsg = new ErrorMessage("01", "Error unexpected when create news");
                _logger.LogError(e, errorMsg.Message);
                return response.SetErrors(new ErrorMessage[] { errorMsg });
            }
        }
    }
}