using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace NewsApi.Application.Shared
{
    public class UseCaseResponse<T>
    {
        public T Result { get; private set; }
        public IEnumerable<ErrorMessage> Errors { get; private set; }
        public UseCaseResponseStatus Status { get; private set; }

        public UseCaseResponse<T> SetResult(T result)
        {
            this.Status = UseCaseResponseStatus.Success;
            this.Result = result;
            return this;
        }

        private UseCaseResponse<T> SetErrors(IEnumerable<ErrorMessage> errors)
        {
            Errors = errors;
            return this;
        }

        public UseCaseResponse<T> SetGenericError(IEnumerable<ErrorMessage> errors)
        {
            this.Status = UseCaseResponseStatus.ExceptionError;
            return this.SetErrors(errors);
        }

        public UseCaseResponse<T> SetResourceNotFountError(IEnumerable<ErrorMessage> errors = null)
        {
            this.Status = UseCaseResponseStatus.ResourceNotFountError;
            return this.SetErrors(errors);
        }

        public UseCaseResponse<T> SetRequestValidatorError(IEnumerable<ValidationFailure> validationErrors)
        {
            this.Status = UseCaseResponseStatus.ValidateError;
            var errors = validationErrors.Select(x => new ErrorMessage(x.ErrorCode, x.ErrorMessage));
            return this.SetErrors(errors);
        }

        public bool Success()
            => Status == UseCaseResponseStatus.Success && (Errors == null || Errors.Count() <= 0);
    }
}