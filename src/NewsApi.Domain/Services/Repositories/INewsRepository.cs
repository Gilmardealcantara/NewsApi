using System.Threading.Tasks;
using NewsApi.Domain.Entities;

namespace NewsApi.Domain.Services.Repositories
{
    public interface INewsRepository
    {
        Task Save(News news);
    }
}