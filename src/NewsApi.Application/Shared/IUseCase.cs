using System.Threading.Tasks;

namespace NewsApi.Application.Shared
{
    public interface IUseCase<TRequest, TResponse>
    {
        Task<UseCaseResponse<TResponse>> Execute(TRequest request);
    }

    public interface IUseCase<TResponse>
    {
        Task<UseCaseResponse<TResponse>> Execute();
    }
}