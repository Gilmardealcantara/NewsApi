using NewsApi.Domain.Dtos;
using NewsApi.Domain.Entities;
using NewsApi.Domain.Shared;

namespace NewsApi.Domain.UseCases.Interfaces
{
    public interface ICreateNewsUseCase : IUseCase<CreateNewsRequest, News>
    {

    }
}