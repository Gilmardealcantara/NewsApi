using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NewsApi.Domain.Entities;

namespace NewsApi.Domain.Services.Repositories
{
    public interface INewsRepository
    {
        Task Save(News news);
        Task Update(News news);
        Task<IEnumerable<News>> GetAll();
        Task<News> GetById(Guid id);
        Task Delete(News news);
    }
}