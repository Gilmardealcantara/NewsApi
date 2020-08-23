using System;
using NewsApi.Domain.Entities;

namespace NewsApi.Domain.Dtos
{
    public class CreateNewsRequest
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public Author Author { get; set; }
        public string ThumbnailLocalURL { get; set; }
        public News ToNews() => new News(Title, Content, Author);
    }

    public class UpdateNewsRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Author Author { get; set; }
        public string ThumbnailLocalURL { get; set; }
        public News ToNews(News oldNews)
            => new News(this.Id, Title ?? oldNews.Title, Content ?? oldNews.Content, Author ?? oldNews.Author);
    }
}