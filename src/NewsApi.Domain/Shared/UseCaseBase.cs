using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using FluentValidation;

namespace NewsApi.Domain.Shared
{
    public abstract class UseCaseBase<TRequest, TResponse> : IUseCase<TRequest, TResponse>
    {
        private readonly ILogger<UseCaseBase<TRequest, TResponse>> _logger;
        private readonly IValidator<TRequest> _validator;
        private readonly (string Code, string Msg) _error;
        protected readonly UseCaseResponse<TResponse> _response;
        protected UseCaseBase(
            ILogger<UseCaseBase<TRequest, TResponse>> logger,
            IValidator<TRequest> validator,
            (string Code, string Msg) error)
        {
            _logger = logger;
            _validator = validator;
            _error = error;
            _response = new UseCaseResponse<TResponse>();
        }

        public async Task<UseCaseResponse<TResponse>> Execute(TRequest request)
        {
            try
            {
                var validate = await _validator.ValidateAsync(request);
                if (!validate.IsValid)
                    return _response.SetRequestValidatorError(validate.Errors);
                var result = await this.OnExecute(request);
                return result;
            }
            catch (Exception e)
            {
                var errorMsg = new ErrorMessage(_error.Code, e.Message ?? _error.Msg);
                _logger.LogError(e, errorMsg.Message);
                return _response.SetGenericError(new ErrorMessage[] { errorMsg });
            }
        }

        protected abstract Task<UseCaseResponse<TResponse>> OnExecute(TRequest request);
    }

    public abstract class UseCaseBase<TResponse> : IUseCase<TResponse>
    {
        private readonly ILogger<UseCaseBase<TResponse>> _logger;
        private readonly (string Code, string Msg) _error;
        protected readonly UseCaseResponse<TResponse> _response;
        protected UseCaseBase(
            ILogger<UseCaseBase<TResponse>> logger,
            (string Code, string Msg) error)
        {
            _logger = logger;
            _error = error;
            _response = new UseCaseResponse<TResponse>();
        }

        public async Task<UseCaseResponse<TResponse>> Execute()
        {
            try
            {
                var result = await this.OnExecute();
                return result;
            }
            catch (Exception e)
            {
                var errorMsg = new ErrorMessage(_error.Code, e.Message ?? _error.Msg);
                _logger.LogError(e, errorMsg.Message);
                return _response.SetGenericError(new ErrorMessage[] { errorMsg });
            }
        }

        protected abstract Task<UseCaseResponse<TResponse>> OnExecute();
    }
}