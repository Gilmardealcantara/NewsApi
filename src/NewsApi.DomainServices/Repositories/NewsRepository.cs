using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using NewsApi.Domain.Entities;
using NewsApi.Domain.Services.Repositories;

namespace NewsApi.DomainServices.Repositories
{
    public class NewsRepository : INewsRepository
    {
        private readonly IDbConnection _dbConnection;

        public NewsRepository(IDbConnection dbConnection)
            => _dbConnection = dbConnection;

        public async Task Delete(Guid newsId)
        {
            await _dbConnection.ExecuteAsync(@"delete from News where id = @id", new { id = newsId });
        }

        public async Task<IEnumerable<News>> GetAll()
        {
            const string sql = @"SELECT 
                n.[NewsId] Id,
                n.[Title],
                n.[ThumbnailURL],
                n.[AuthorId] Author_Id, 
                a.[UserName] Author_UserName,
                c.CommentId Comment_Id,
                c.Text Comment_Text,
                c.AuthorId Comment_Author_Id,
                ac.UserName Comment_Author_UserName
            FROM [News] n 
            JOIN Authors a ON n.AuthorId = a.AuthorId;";

            return await _dbConnection.QueryAsync<News>(sql);
        }

        public async Task<News> GetById(Guid id)
        {
            const string sql = @"SELECT 
                n.[NewsId] Id,
                n.[Title],
                n.[ThumbnailURL],
                n.[AuthorId] Author_Id, 
                a.[UserName] Author_UserName,
                c.CommentId Comment_Id,
                c.Text Comment_Text,
                c.AuthorId Comment_Author_Id,
                ac.UserName Comment_Author_UserName
            FROM [News] n 
            JOIN Authors a ON n.AuthorId = a.AuthorId
            JOIN Comments c ON c.NewsId = n.NewsId 
            JOIN Authors ac ON c.AuthorId = c.AuthorId
            WHERE n.Id=@id";

            return await _dbConnection.QueryFirstOrDefaultAsync<News>(sql, new { id = id });
        }

        public async Task<IEnumerable<Comment>> GetComments(Guid id)
        {
            const string sql = @"SELECT 
                c.[CommentId] Id
                ,c.[Text]
                ,c.[NewsId]
                ,c.[AuthorId] Author_Id
                ,a.UserName Author_UserName
            FROM [Comments] c
            JOIN Authors a ON c.AuthorId = c.AuthorId
            Where NewsId = id";

            return await _dbConnection.QueryAsync<Comment>(sql, new { id = id });
        }

        public Task Save(News news)
        {
            throw new NotImplementedException();
        }

        public Task Update(News news)
        {
            throw new NotImplementedException();
        }
    }
}