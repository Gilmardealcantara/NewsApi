using System.Collections.Generic;
using System.Linq;

namespace NewsApi.Domain.Shared
{
    public class UseCaseResponse<T> where T : class
    {
        public T? Result { get; private set; }
        public IEnumerable<ErrorMessage>? Errors { get; private set; }

        public void SetResult(T result)
            => Result = result;

        public void SetErrors(IEnumerable<ErrorMessage> errors)
            => Errors = errors;

        public bool Success() => Result != null && Errors != null && Errors.Count() > 0;
    }
}