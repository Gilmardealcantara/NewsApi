using System.Collections.Generic;
using System.Threading.Tasks;
using NewsApi.Domain.Entities;

namespace NewsApi.Domain.Services.Repositories
{
    public interface INewsRepository
    {
        Task Save(News news);
        Task Update(News news);
        Task<IEnumerable<News>> List(News news);
        Task Delete(News news);
    }
}