using System;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NewsApi.Application.Entities;
using NewsApi.Application.Services.Repositories;
using NewsApi.Application.Shared;
using NewsApi.Application.UseCases.News;
using Xunit;

namespace NewsApi.Application.Tests.UseCases.News
{
    public class IListNewsUseCaseTest
    {
        [Theory]
        [InlineData(100)]
        [InlineData(99)]
        [InlineData(101)]
        public async Task UseCase_WhenOk_ReturnListOfNews(int contentSize)
        {
            //Given
            int contentPreviewSizeLimit = 100;
            var newsList = new Faker<Entities.News>()
                .CustomInstantiator(f => new Entities.News(
                    Guid.NewGuid(),
                    f.Lorem.Sentence(3),
                    f.Lorem.Letter(contentSize),
                    new Author(f.Person.UserName, f.Person.FullName)
                ))
                .RuleFor(n => n.ThumbnailURL, (f, n) => $"https://s3.amazonaws.com/bucketname/{n.Id}.png")
                .Generate(10);

            var logger = new Mock<ILogger<ListNewsUseCase>>().Object;
            var repositoryMock = new Mock<INewsRepository>();
            repositoryMock.Setup(r => r.GetAll()).ReturnsAsync(newsList);

            //When
            var useCase = new ListNewsUseCase(logger, repositoryMock.Object);
            var response = await useCase.Execute();

            //Then
            response.Status.Should().Be(UseCaseResponseStatus.Success);
            response.Success().Should().BeTrue();
            response.Result.Should()
                .NotBeNullOrEmpty()
                .And.HaveCount(10)
                .And.NotContain(x =>
                    x.Title == null
                    || x.ContentPreview == null
                    || x.ContentPreview.Length > contentPreviewSizeLimit
                    || x.Id == Guid.Empty
                    || x.ThumbnailURL == null
                );

            repositoryMock.Verify(r => r.GetAll(), Times.Once);

        }
    }
}