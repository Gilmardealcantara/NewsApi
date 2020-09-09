using System;
using NewsApi.Application.Entities;
using Newtonsoft.Json;

namespace NewsApi.Application.Dtos
{
    public abstract class NewsRequest
    {
        public string Title { get; set; }
        public string Content { get; set; }
        [JsonIgnore]
        public AuthorRequest Author { get; set; }
        public NewsRequest WithAuthor(AuthorRequest author)
        {
            this.Author = author;
            return this;
        }
    }

    public class CreateNewsRequest : NewsRequest
    {
        public News ToNews(Author author) => new News(this.Title, this.Content, author);
    }

    public class UpdateNewsRequest : NewsRequest
    {
        [JsonIgnore]
        public Guid Id { get; set; }

        public News ToNews(Author author)
            => new News(this.Id, Title, Content, author);

        public UpdateNewsRequest WithId(Guid id)
        {
            this.Id = id;
            return this;
        }
    }
}