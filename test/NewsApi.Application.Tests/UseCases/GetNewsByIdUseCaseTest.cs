using System;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NewsApi.Application.Entities;
using NewsApi.Application.Services.Repositories;
using NewsApi.Application.Shared;
using NewsApi.Application.Tests.Validadors;
using NewsApi.Application.UseCases;
using Xunit;

namespace NewsApi.Application.Tests.UseCases
{
    public class GetNewsByIdUseCaseTest
    {
        [Fact]
        public async Task UseCase_WhenOk_ResultNewsWithIdTitleAndContent()
        {
            var fakeNews = new Faker<News>()
                .CustomInstantiator(f => new News(
                    Guid.NewGuid(),
                    f.Lorem.Sentence(3),
                    f.Lorem.Paragraphs(3),
                    new Author(f.Person.UserName, f.Person.FullName)
                ))
                .RuleFor(n => n.ThumbnailURL, (f, n) => $"https://s3.amazonaws.com/bucketname/{n.Id}.png")
                .Generate();

            var logger = new Mock<ILogger<GetNewsByIdUseCase>>().Object;
            var validator = ValidatorsFactory.GetValidValidator<Guid>();

            var repositoryMock = new Mock<INewsRepository>();
            repositoryMock.Setup(r => r.GetById(fakeNews.Id))
                .ReturnsAsync(fakeNews);

            var useCase = new GetNewsByIdUseCase(logger, validator, repositoryMock.Object);
            var response = await useCase.Execute(fakeNews.Id);

            response.Status.Should().Be(UseCaseResponseStatus.Success);
            response.Result.Id.Should().Be(fakeNews.Id);
            response.Result.Title.Should().Be(fakeNews.Title);
            response.Result.Content.Should().Be(fakeNews.Content);
            response.Result.ThumbnailURL.Should().Be(fakeNews.ThumbnailURL);
            repositoryMock.Verify(r => r.GetById(fakeNews.Id), Times.Once);
        }

        [Fact]
        public async Task UseCase_WhenOk_ResultNewsListOfComments()
        {
            int numCommentByNews = 10;
            var fakeNews = new Faker<News>()
                .CustomInstantiator(f => new News(
                    Guid.NewGuid(),
                    f.Lorem.Sentence(3),
                    f.Lorem.Paragraphs(3),
                    new Author(f.Person.UserName, f.Person.FullName)))
                .Generate();


            var fakeComments = new Faker<Comment>()
                .CustomInstantiator(f => new Comment(f.Lorem.Paragraph(), new Author(f.Person.UserName, f.Person.FullName)))
                .Generate(10);

            var logger = new Mock<ILogger<GetNewsByIdUseCase>>().Object;
            var validator = ValidatorsFactory.GetValidValidator<Guid>();

            var repositoryMock = new Mock<INewsRepository>();
            repositoryMock.Setup(r => r.GetById(fakeNews.Id)).ReturnsAsync(fakeNews);
            repositoryMock.Setup(r => r.GetComments(fakeNews.Id, 10)).ReturnsAsync(fakeComments);

            var useCase = new GetNewsByIdUseCase(logger, validator, repositoryMock.Object);
            var response = await useCase.Execute(fakeNews.Id);

            response.Result.NumComments.Should().Be(numCommentByNews);
            response.Result.Comments.Should()
                .NotContain(x => x.Text == null || x.Id == Guid.Empty || x.Author == null || x.Author.UserName == null);
            repositoryMock.Verify(r => r.GetById(fakeNews.Id), Times.Once);
        }

        [Fact]
        public async Task UseCase_WhenNewsNotExists_ReturnNotFount()
        {

            var personId = Guid.NewGuid();
            var logger = new Mock<ILogger<GetNewsByIdUseCase>>().Object;
            var validator = ValidatorsFactory.GetValidValidator<Guid>();

            var repositoryMock = new Mock<INewsRepository>();
            repositoryMock.Setup(r => r.GetById(personId))
                .ReturnsAsync((News)null);

            var useCase = new GetNewsByIdUseCase(logger, validator, repositoryMock.Object);
            var response = await useCase.Execute(personId);

            response.Status.Should().Be(UseCaseResponseStatus.ResourceNotFountError);
        }
    }
}