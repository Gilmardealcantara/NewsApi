using System.Collections.Generic;
using NewsApi.Application.Dtos;
using NewsApi.Application.Shared;

namespace NewsApi.Application.UseCases.Interfaces
{
    public interface IListNewsUseCase : IUseCase<IEnumerable<NewsListItem>>
    {

    }
}