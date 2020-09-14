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
        Task AddComment(Guid newsId, Comment comment);
        Task UpdateComment(Guid newsId, Comment comment);
        Task RemoveComment(Guid newsId, Guid commentId);
    }
}