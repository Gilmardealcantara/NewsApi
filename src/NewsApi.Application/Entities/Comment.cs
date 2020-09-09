using System;

namespace NewsApi.Application.Entities
{
    public class Comment : BaseEntity
    {
        public string Text { get; }
        public Author Author { get; }

        public Comment(string text, Author author)
            => (Text, Author) = (text, author);

        public Comment(Guid id, string text, Author author)
            => (Id, Text, Author) = (id, text, author);
    }
}