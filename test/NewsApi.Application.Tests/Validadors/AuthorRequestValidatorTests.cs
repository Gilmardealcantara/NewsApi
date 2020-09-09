using FluentAssertions;
using NewsApi.Application.Dtos;
using NewsApi.Application.Validator;
using Xunit;

namespace NewsApi.Application.Tests.Validadors
{
    public class AuthorRequestValidatorTests
    {
        [Theory]
        [InlineData("gilmardealcantara@gmail.com", "Gilmar ", true)]
        [InlineData("gilmardealcantara", "Gilmar ", false)]
        [InlineData("", "Gilmar ", false)]
        [InlineData("gilmardealcantara", " ", false)]

        public void Validate_Author_Tests(string userName, string name, bool result)
        {
            var authorReq = new AuthorRequest { UserName = userName, Name = name };
            var validate = new AuthorRequestValidator().Validate(authorReq);
            validate.IsValid.Should().Be(result);
        }
    }
}