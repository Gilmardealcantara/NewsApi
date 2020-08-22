using System;

namespace NewsApi.Domain.Entities
{
    public class News : BaseEntity
    {
        public News(string title)
            => Title = title;
        public News(Guid id, string title) : base(id)
           => Title = title;

        public string Title { get; }
        public string Content { get; private set; }

        public void SetContent(string content)
        {
            this.Content = content;
        }
    }
}