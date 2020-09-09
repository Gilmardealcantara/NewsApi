using System;
using FluentAssertions;
using NewsApi.Application.Dtos;
using NewsApi.Application.Validator;
using Xunit;

namespace NewsApi.Application.Tests.Validadors
{
    public class NewsRequestTests
    {
        [Theory]
        [InlineData("Title test", "Content Test", true)]
        [InlineData("", "Content Test ", false)]
        [InlineData("Title Test", " ", false)]

        public void CreateValidate_Author_Tests(string title, string content, bool result)
        {
            var author = new AuthorRequest { UserName = "gilmardealcantara@gmail.com", Name = "Gilmar" };

            var authorReq = new CreateNewsRequest { Title = title, Content = content, Author = author };
            var validate = new CreateNewsRequestValidator().Validate(authorReq);
            validate.IsValid.Should().Be(result);
        }

        [Theory]
        [InlineData("00000000-0000-0000-0000-000000000000", "Title test", "Content Test", false)]
        [InlineData("d572667e-bd6f-4ef9-bb03-3a4dbf9c2d00", "Title test", "Content Test", true)]
        public void UpdateValidate_Author_Tests(string id, string title, string content, bool result)
        {
            var author = new AuthorRequest { UserName = "gilmardealcantara@gmail.com", Name = "Gilmar" };

            var authorReq = new UpdateNewsRequest { Id = Guid.Parse(id), Title = title, Content = content, Author = author };
            var validate = new UpdateNewsRequestValidator().Validate(authorReq);
            validate.IsValid.Should().Be(result);
        }
    }
}