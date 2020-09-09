using System;
using NewsApi.Application.Entities;
using Newtonsoft.Json;

namespace NewsApi.Application.Dtos
{
    public class CreateNewsRequest
    {
        public string Title { get; set; }
        public string Content { get; set; }
        [JsonIgnore]
        public AuthorRequest Author { get; set; }
        public News ToNews(Author author) => new News(Title, Content, author);
    }

    public class UpdateNewsRequest
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        [JsonIgnore]
        public AuthorRequest Author { get; set; }
        public News ToNews(Author author)
            => new News(this.Id, Title, Content, author);

        public UpdateNewsRequest WithId(Guid id)
        {
            this.Id = id;
            return this;
        }
    }
}