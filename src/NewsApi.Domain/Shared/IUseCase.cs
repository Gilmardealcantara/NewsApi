using System.Threading.Tasks;

namespace NewsApi.Domain.Shared
{
    public interface IUseCase<TRequest, TResponse> where TResponse : class
    {
        Task<UseCaseResponse<TResponse>> Execute(TRequest request);
    }
}