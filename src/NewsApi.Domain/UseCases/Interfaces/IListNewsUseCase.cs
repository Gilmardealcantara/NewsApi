using System.Collections.Generic;
using NewsApi.Domain.Dtos;
using NewsApi.Domain.Shared;

namespace NewsApi.Domain.UseCases.Interfaces
{
    public interface IListNewsUseCase : IUseCase<IEnumerable<NewsListItem>>
    {

    }
}