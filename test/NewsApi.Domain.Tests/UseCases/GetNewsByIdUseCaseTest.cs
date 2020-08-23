using System;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NewsApi.Domain.Entities;
using NewsApi.Domain.Services.Repositories;
using NewsApi.Domain.Shared;
using NewsApi.Domain.Tests.Validadors;
using NewsApi.Domain.UseCases;
using Xunit;

namespace NewsApi.Domain.Tests.UseCases
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
                    new Author(f.Person.UserName)
                )).Generate();

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
                    new Author(f.Person.UserName)))
                .RuleFor(n => n.Comments, f =>
                    f.Make<Comment>(numCommentByNews, () => new Comment(f.Lorem.Paragraph(), new Author(f.Person.UserName)))
                ).Generate();


            var fakeComments = new Faker<Comment>()
                .CustomInstantiator(f => new Comment(f.Lorem.Paragraph(), new Author(f.Person.UserName)))
                .Generate(10);

            var logger = new Mock<ILogger<GetNewsByIdUseCase>>().Object;
            var validator = ValidatorsFactory.GetValidValidator<Guid>();

            var repositoryMock = new Mock<INewsRepository>();
            repositoryMock.Setup(r => r.GetById(fakeNews.Id)).ReturnsAsync(fakeNews);

            var useCase = new GetNewsByIdUseCase(logger, validator, repositoryMock.Object);
            var response = await useCase.Execute(fakeNews.Id);

            response.Result.NumComments.Should().Be(numCommentByNews);
            response.Result.Comments.Should()
                .NotContain(x => x.Text == null || x.Id == Guid.Empty || x.Author == null || x.Author.UserName == null);
            repositoryMock.Verify(r => r.GetById(fakeNews.Id), Times.Once);
        }
    }
}