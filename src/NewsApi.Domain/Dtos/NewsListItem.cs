using System;

namespace NewsApi.Domain.Dtos
{
    public class NewsListItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string ContentPreview { get; set; }
        public string ThumbnailURL { get; set; }
    }
}