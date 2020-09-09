using System;

namespace NewsApi.Application.Entities
{
    public class Author : BaseEntity
    {
        public Author(string userName, string name)
            => (UserName, Name) = (userName, name);
        public Author(Guid id, string userName, string name)
            => (Id, UserName, Name) = (id, userName, name);
        public string UserName { get; }
        public string Name { get; }
    }
}