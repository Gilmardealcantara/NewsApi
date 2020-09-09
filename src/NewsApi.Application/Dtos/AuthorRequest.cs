using NewsApi.Application.Entities;

namespace NewsApi.Application.Dtos
{
    public class AuthorRequest
    {
        public string UserName { get; set; }
        public string Name { get; set; }

        public Author ToAuthor() => new Author(this.UserName, this.Name);
    }
}