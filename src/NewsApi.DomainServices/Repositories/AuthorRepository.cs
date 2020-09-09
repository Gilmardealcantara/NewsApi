using System.Threading.Tasks;
using NewsApi.Application.Entities;
using NewsApi.Application.Services.Repositories;

namespace NewsApi.DomainServices.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        public Task<Author> GeyByUserName(string userName)
        {
            throw new System.NotImplementedException();
        }

        public Task Save(Author author)
        {
            throw new System.NotImplementedException();
        }
    }
}