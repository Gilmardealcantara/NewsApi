using NewsApi.Application.Dtos;

namespace NewsApi.Application.Tests.Builders
{
    public class AuthorRequestBuilder
    {
        private readonly AuthorRequest _instance;
        public AuthorRequestBuilder()
        {
            _instance = new AuthorRequest
            {
                UserName = "gilmardealcantara@gmail.com",
                Name = "Gilmar Alcantara"
            };
        }

        public AuthorRequestBuilder WithUserName(string userName)
        {
            _instance.UserName = userName;
            return this;
        }

        public AuthorRequestBuilder WithName(string name)
        {
            _instance.Name = name;
            return this;
        }

        public AuthorRequest Build() => _instance;
    }
}