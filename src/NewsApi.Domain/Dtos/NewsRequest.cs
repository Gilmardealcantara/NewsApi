using System;
using NewsApi.Domain.Entities;

namespace NewsApi.Domain.Dtos
{
    public class CreateNewsRequest
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public News ToNews()
        {
            var news = new News(Title);
            news.SetContent(Content);
            return news;
        }
    }

    public class UpdateNewsRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public News ToNews()
        {
            var news = new News(this.Id, Title);
            news.SetContent(Content);
            return news;
        }
    }
}