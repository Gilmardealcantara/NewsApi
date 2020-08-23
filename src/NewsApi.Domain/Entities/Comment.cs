namespace NewsApi.Domain.Entities
{
    public class Comment : BaseEntity
    {
        public string Text { get; }
        public Author Author { get; set; }

        public Comment(string text, Author author)
            => (Text, Author) = (text, author);
    }
}