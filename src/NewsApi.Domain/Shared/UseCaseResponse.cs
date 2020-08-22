using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace NewsApi.Domain.Shared
{
    public class UseCaseResponse<T>
    {
        public T Result { get; private set; }
        public IEnumerable<ErrorMessage>? Errors { get; private set; }
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
            this.Status = UseCaseResponseStatus.GenericExceptionError;
            return this.SetErrors(errors);
        }


        public UseCaseResponse<T> SetRequestValidatorError(IEnumerable<ValidationFailure> validationErrors)
        {
            this.Status = UseCaseResponseStatus.ValidateError;
            var errors = validationErrors.Select(x => new ErrorMessage(x.ErrorCode, x.ErrorMessage));
            return this.SetErrors(errors);
        }

        public bool Success()
            => Result != null && (Errors == null || Errors.Count() <= 0);
    }
}