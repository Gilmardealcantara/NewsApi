using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using NewsApi.Application.Entities;
using NewsApi.Application.Services.Repositories;
using Newtonsoft.Json;

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
                a.[UserName] Author_UserName,
                a.[Name] Author_Name
            FROM [News] n 
            JOIN Authors a ON n.AuthorId = a.AuthorId;";

            return (await _dbConnection.QueryAsync(sql))
                .Select(f => new News(
                    (Guid)f.Id, f.Title, f.Content,
                    new Author((Guid)f.Author_Id, (string)f.Author_UserName, (string)f.Author_Name))
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
                a.[UserName] Author_UserName,
                a.[Name] Author_Name
            FROM [News] n 
            JOIN Authors a ON n.AuthorId = a.AuthorId
            WHERE n.NewsId=@id";

            var newsQueryResult = await _dbConnection.QueryFirstOrDefaultAsync(newsQuery, new { id = id });
            var news = newsQueryResult is null ? null : new News(
                (Guid)newsQueryResult.Id,
                newsQueryResult.Title,
                newsQueryResult.Content,
                new Author((Guid)newsQueryResult.Author_Id, (string)newsQueryResult.Author_UserName, (string)newsQueryResult.Author_Name))
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
            JOIN Authors a ON c.AuthorId = a.AuthorId
            Where NewsId = @id";

            return (await _dbConnection.QueryAsync(sql, new { id, limit }))
                .Select(f => new Comment(f.Text, new Author((Guid)f.Author_Id, f.Author_UserName, f.Author_Name)));
        }

        public async Task Save(News news)
        {
            var keys = "(NewsId, Title, Content, AuthorId)";
            var values = "(@NewsId, @Title, @Content, @AuthorId)";
            var command = $"INSERT INTO News {keys} values {values}";

            var parameters = new { news.Title, news.Content, NewsId = news.Id, AuthorId = news.Author.Id };
            await _dbConnection.ExecuteAsync(command, parameters);
        }

        public async Task Update(News news)
        {
            var command = @"UPDATE News 
                SET Title = @Title,
                Content = @Content,
                AuthorId = @AuthorId
                WHERE NewsId = @NewsId";

            var parameters = new { news.Title, news.Content, NewsId = news.Id, AuthorId = news.Author.Id };
            await _dbConnection.ExecuteAsync(command, parameters);
        }
    }
}