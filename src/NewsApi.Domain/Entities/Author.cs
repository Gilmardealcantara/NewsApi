using System;

namespace NewsApi.Domain.Entities
{
    public class Author : BaseEntity
    {
        public Author(string userName) => UserName = userName;
        public Author(Guid id, string userName)
            => (Id, UserName) = (id, userName);
        public string UserName { get; }
    }
}