using System;

namespace NewsApi.Application.Dtos
{
    public class NewsListItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string ContentPreview { get; set; }
        public string ThumbnailURL { get; set; }
    }
}