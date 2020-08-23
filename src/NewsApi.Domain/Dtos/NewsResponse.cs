using System;

namespace NewsApi.Domain.Dtos
{
    public class NewsResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int NumLikes { get; set; }
        public int NumComments { get; set; }
    }
}