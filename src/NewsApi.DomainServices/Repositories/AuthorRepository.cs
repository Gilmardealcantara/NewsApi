using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using NewsApi.Application.Entities;
using NewsApi.Application.Services.Repositories;
using Newtonsoft.Json;

namespace NewsApi.DomainServices.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {

        private readonly IDbConnection _dbConnection;

        public AuthorRepository(IDbConnection dbConnection)
            => _dbConnection = dbConnection;

        public async Task<Author> GeyByUserName(string userName)
        {
            var query = @"SELECT 
                [AuthorId] Id
                ,[UserName]
                ,[Name]
            FROM [news-dev].[dbo].[Authors]
            WHERE UserName = @userName";

            var result = await _dbConnection.QueryFirstOrDefaultAsync(query, new { userName });
            return result is null ? null : new Author((Guid)result.Id, result.UserName, result.Name);
        }

        public async Task Save(Author author)
        {
            var keys = "(AuthorId, UserName, [Name])";
            var values = "(@Id, @UserName, @Name)";
            var comand = $"INSERT INTO Authors {keys} VALUES {values}";
            await _dbConnection.ExecuteAsync(comand, author);
        }
    }
}