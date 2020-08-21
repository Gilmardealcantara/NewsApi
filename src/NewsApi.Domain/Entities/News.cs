namespace NewsApi.Domain.Entities
{
    public class News : BaseEntity
    {
        public News(string title)
            => Title = title;

        public string Title { get; }
    }
}