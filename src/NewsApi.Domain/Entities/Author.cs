namespace NewsApi.Domain.Entities
{
    public class Author : BaseEntity
    {
        public Author(string userName) => UserName = userName;
        public string UserName { get; }
    }
}