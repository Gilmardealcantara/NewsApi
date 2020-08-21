using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace NewsApi.Domain.Shared
{
    public class UseCaseResponse<T> where T : class
    {
        public T? Result { get; private set; }
        public IEnumerable<ErrorMessage>? Errors { get; private set; }

        public UseCaseResponse<T> SetResult(T result)
        {
            this.Result = result;
            return this;
        }

        public UseCaseResponse<T> SetErrors(IEnumerable<ErrorMessage> errors)
        {
            Errors = errors;
            return this;
        }

        public UseCaseResponse<T> SetRequestValidatorError(IEnumerable<ValidationFailure> errors)
            => this.SetErrors(errors.Select(x => new ErrorMessage(x.ErrorCode, x.ErrorMessage)));

        public bool Success()
            => Result != null && Errors != null && Errors.Count() > 0;
    }
}