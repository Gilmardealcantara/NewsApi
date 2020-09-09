using System.Collections.Generic;
using NewsApi.Application.Dtos;
using NewsApi.Application.Shared;

namespace NewsApi.Application.UseCases.Interfaces.News
{
    public interface IListNewsUseCase : IUseCase<IEnumerable<NewsListItem>>
    {

    }
}