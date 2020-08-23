using System;

namespace NewsApi.Domain.Entities
{
    public class News : BaseEntity
    {
        public News(string title, string content, Author author)
            => (Title, Content, Author) = (title, content, author);
        public News(Guid id, string title, string content, Author author) : base(id)
           => (Title, Content, Author) = (title, content, author);

        public string Title { get; }
        public string Content { get; }
        public Author Author { get; }
    }
}