using NewsApi.Application.Dtos;
using NewsApi.Application.Shared;

namespace NewsApi.Application.UseCases.Interfaces
{
    public interface ICreateNewsUseCase : IUseCase<CreateNewsRequest, Entities.News>
    {

    }
}