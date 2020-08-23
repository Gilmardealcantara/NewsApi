namespace NewsApi.Domain.Entities
{
    public class Comment : BaseEntity
    {
        public string Text { get; }
        public Author Author { get; set; }

        public Comment(Author author, string text)
            => (Author, Text) = (author, text);
    }
}