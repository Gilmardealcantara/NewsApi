using NewsApi.Application.Dtos;
using NewsApi.Application.Shared;

namespace NewsApi.Application.UseCases.Interfaces.News
{
    public interface IUpdateNewsUseCase : IUseCase<UpdateNewsRequest, Entities.News>
    {

    }
}