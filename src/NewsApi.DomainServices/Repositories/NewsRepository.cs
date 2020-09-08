using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using NewsApi.Application.Entities;
using NewsApi.Application.Services.Repositories;

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
                n.[Content],
                n.[ThumbnailURL],
                n.[AuthorId] Author_Id, 
                a.[UserName] Author_UserName
            FROM [News] n 
            JOIN Authors a ON n.AuthorId = a.AuthorId;";

            return (await _dbConnection.QueryAsync(sql))
                .Select(f => new News(
                    (Guid)f.Id, f.Title, f.Content,
                    new Author(f.Author_Id, f.Author_UserName))
                {
                    ThumbnailURL = f.ThumbnailURL
                });
        }

        public async Task<News> GetById(Guid id)
        {

            const string newsQuery = @"SELECT 
                n.[NewsId] Id,
                n.[Title],
                n.[Content],
                n.[ThumbnailURL],
                n.[AuthorId] Author_Id, 
                a.[UserName] Author_UserName
            FROM [News] n 
            JOIN Authors a ON n.AuthorId = a.AuthorId
            WHERE n.NewsId=@id";

            var newsQueryResult = await _dbConnection.QueryFirstOrDefaultAsync(newsQuery, new { id = id });
            var news = new News(
                (Guid)newsQueryResult.Id,
                newsQueryResult.Title,
                newsQueryResult.Content,
                new Author(newsQueryResult.Author_Id, newsQueryResult.Author_UserName))
            {
                ThumbnailURL = newsQueryResult.ThumbnailURL
            };

            return news;
        }

        public async Task<IEnumerable<Comment>> GetComments(Guid id, int limit = 10)
        {
            const string sql = @"SELECT TOP (@limit)
                c.[CommentId] Id
                ,c.[Text]
                ,c.[NewsId]
                ,c.[AuthorId] Author_Id
                ,a.UserName Author_UserName
            FROM [Comments] c
            JOIN Authors a ON c.AuthorId = c.AuthorId
            Where NewsId = @id";

            return (await _dbConnection.QueryAsync(sql, new { id, limit }))
                .Select(f => new Comment(f.Text, new Author(f.Author_Id, f.Author_UserName)));
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