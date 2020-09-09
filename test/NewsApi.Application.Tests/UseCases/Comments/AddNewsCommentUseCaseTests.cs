using System;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NewsApi.Application.Dtos;
using NewsApi.Application.Entities;
using NewsApi.Application.Services.Repositories;
using NewsApi.Application.Shared;
using NewsApi.Application.Tests.Validadors;
using NewsApi.Application.UseCases.Comments;
using Xunit;

namespace NewsApi.Application.Tests.UseCases.Comments
{
    public class AddNewsCommentUseCaseTests
    {
        [Fact]
        public async Task UseCase_WhenOk_ReturnCommentList()
        {
            var createCommentRequest = new CreateCommentRequest
            {
                NewsId = Guid.NewGuid(),
                Text = "Que massa !!!",
                Author = new AuthorRequest
                {
                    UserName = "gilmardealcantara@gmail.com",
                    Name = "Gilmar"
                }
            };

            var logger = new Mock<ILogger<AddNewsCommentUseCase>>().Object;
            var validator = ValidatorFactory.GetValidValidator<CreateCommentRequest>();
            var authorRepoMock = new Mock<IAuthorRepository>();
            var newsRepoMock = new Mock<INewsRepository>();

            newsRepoMock.Setup(x => x.GetById(createCommentRequest.NewsId))
                .ReturnsAsync(new Entities.News(createCommentRequest.NewsId, "", "", new Author("", "")));

            var usecase = new AddNewsCommentUseCase(logger, validator, authorRepoMock.Object, newsRepoMock.Object);
            var response = await usecase.Execute(createCommentRequest);

            response.Status.Should().Be(UseCaseResponseStatus.Success);
            response.Result.Should().NotBeEmpty();

            authorRepoMock.Verify(v => v.GeyByUserName(createCommentRequest.Author.UserName), Times.Once);
            authorRepoMock.Verify(v => v.Save(It.IsAny<Author>()), Times.Once);
            newsRepoMock.Verify(v => v.GetById(createCommentRequest.NewsId), Times.Once);
            newsRepoMock.Verify(v => v.AddComment(createCommentRequest.NewsId, It.IsAny<Comment>()), Times.Once);
        }
    }
}