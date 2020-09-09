using System;
using NewsApi.Application.Entities;
using Newtonsoft.Json;

namespace NewsApi.Application.Dtos
{
    public class CreateNewsRequest
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public Author Author { get; set; }
        public News ToNews() => new News(Title, Content, Author);
    }

    public class UpdateNewsRequest
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Author Author { get; set; }
        public News ToNews(News oldNews)
            => new News(this.Id, Title ?? oldNews.Title, Content ?? oldNews.Content, Author ?? oldNews.Author);

        public UpdateNewsRequest WithId(Guid id)
        {
            this.Id = id;
            return this;
        }
    }
}