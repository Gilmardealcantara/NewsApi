
using System;
using System.Threading.Tasks;
using NewsApi.Application.Entities;
using NewsApi.Application.Services.Repositories;

namespace NewsApi.Services.Repositories
{
    public partial class NewsRepository : INewsRepository
    {
        public Task AddComment(Guid newsId, Comment comment)
        {
            throw new NotImplementedException();
        }

        public Task RemoveComment(Guid newsId, Guid commentId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateComment(Guid newsId, Comment comment)
        {
            throw new NotImplementedException();
        }
    }
}