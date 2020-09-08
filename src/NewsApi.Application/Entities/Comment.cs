namespace NewsApi.Application.Entities
{
    public class Comment : BaseEntity
    {
        public string Text { get; }
        public Author Author { get; }

        public Comment(string text, Author author)
            => (Text, Author) = (text, author);
    }
}