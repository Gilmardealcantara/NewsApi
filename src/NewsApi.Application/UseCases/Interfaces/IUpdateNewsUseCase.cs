using NewsApi.Application.Dtos;
using NewsApi.Application.Entities;
using NewsApi.Application.Shared;

namespace NewsApi.Application.UseCases.Interfaces
{
    public interface IUpdateNewsUseCase : IUseCase<UpdateNewsRequest, News>
    {

    }
}