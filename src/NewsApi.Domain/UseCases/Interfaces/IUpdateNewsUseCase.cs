using NewsApi.Domain.Dtos;
using NewsApi.Domain.Entities;
using NewsApi.Domain.Shared;

namespace NewsApi.Domain.UseCases.Interfaces
{
    public interface IUpdateNewsUseCase : IUseCase<UpdateNewsRequest, News>
    {

    }
}