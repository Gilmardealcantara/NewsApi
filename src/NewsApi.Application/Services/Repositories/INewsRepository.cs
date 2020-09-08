using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NewsApi.Application.Entities;

namespace NewsApi.Application.Services.Repositories
{
    public interface INewsRepository
    {
        Task Save(News news);
        Task Update(News news);
        Task<IEnumerable<News>> GetAll();
        Task<News> GetById(Guid id);
        Task Delete(Guid newsId);
        Task<IEnumerable<Comment>> GetComments(Guid id, int limit = 10);

    }
}