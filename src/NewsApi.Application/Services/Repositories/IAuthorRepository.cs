using System.Threading.Tasks;
using NewsApi.Application.Entities;

namespace NewsApi.Application.Services.Repositories
{
    public interface IAuthorRepository
    {
        Task Save(Author author);
        Task<Author> GeyByUserName(string userName);
    }
}