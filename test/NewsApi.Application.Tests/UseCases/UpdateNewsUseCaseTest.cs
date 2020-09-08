using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NewsApi.Application.Dtos;
using NewsApi.Application.Entities;
using NewsApi.Application.Services.ExternalApis;
using NewsApi.Application.Services.Repositories;
using NewsApi.Application.Shared;
using NewsApi.Application.Tests.Validadors;
using NewsApi.Application.UseCases;
using Xunit;

namespace NewsApi.Application.Tests.UseCases
{
    public class UpdateNewsUseCaseTest
    {

        [Fact]
        public async Task UseCase_WhenNewsNotExists_ReturnNotFoundError()
        {
            var fakeRequest = new UpdateNewsRequest { Id = Guid.NewGuid(), Title = "New Title" };

            var logger = new Mock<ILogger<UpdateNewsUseCase>>().Object;
            var validator = ValidatorsFactory.GetValidValidator<UpdateNewsRequest>();
            var imageStorage = new Mock<IImageStorageService>().Object;

            var repositorymock = new Mock<INewsRepository>();
            repositorymock.Setup(r => r.GetById(fakeRequest.Id)).ReturnsAsync((News)null);

            var useCase = new UpdateNewsUseCase(logger, validator, repositorymock.Object, imageStorage);
            var response = await useCase.Execute(fakeRequest);

            response.Status.Should().Be(UseCaseResponseStatus.ResourceNotFountError);
            response.Success().Should().BeFalse();

            repositorymock.Verify(r => r.GetById(fakeRequest.Id), Times.Once);
            repositorymock.Verify(r => r.Update(It.IsAny<News>()), Times.Never);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData("/app/uploads/image.png", null)]
        [InlineData("/app/uploads/image.png", "https://s3.amazonaws.com/bucketname/keyName.png")]
        [InlineData(null, "https://s3.amazonaws.com/bucketname/keyName.png")]
        public async Task UseCase_WhenNewsReplaceThumbnail_ReturnSuccess(string requestThumbnailLocalURL, string dbNewsThumbnailURL)
        {
            var fakeRequest = new UpdateNewsRequest
            {
                Id = Guid.NewGuid(),
                Title = "New Title",
                ThumbnailLocalURL = requestThumbnailLocalURL
            };
            var dbNews = new News("Teste", "OPA", new Author("gilmar.alcantara@gmail.com"));
            dbNews.ThumbnailURL = dbNewsThumbnailURL;

            var logger = new Mock<ILogger<UpdateNewsUseCase>>().Object;
            var validator = ValidatorsFactory.GetValidValidator<UpdateNewsRequest>();

            var repositorymock = new Mock<INewsRepository>();
            repositorymock.Setup(r => r.GetById(fakeRequest.Id))
                .ReturnsAsync(dbNews);

            var imageStorageMock = new Mock<IImageStorageService>();
            imageStorageMock.Setup(s => s.Upload(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync((string keyName, string path) => $"https://s3.amazonaws.com/bucketname/{keyName}.png");

            imageStorageMock.Setup(s => s.Update(It.IsAny<string>(), It.IsAny<string>()))
              .ReturnsAsync((string keyName, string path) => $"https://s3.amazonaws.com/bucketname/{keyName}.png");

            var useCase = new UpdateNewsUseCase(logger, validator, repositorymock.Object, imageStorageMock.Object);
            var response = await useCase.Execute(fakeRequest);

            response.Status.Should().Be(UseCaseResponseStatus.Success);
            response.Success().Should().BeTrue();
            response.Result.Id.Should().Be(fakeRequest.Id);
            response.Result.Title.Should().Be(fakeRequest.Title);
            response.Result.Content.Should().Be(dbNews.Content);

            if (dbNewsThumbnailURL is null && requestThumbnailLocalURL is null)
                response.Result.ThumbnailURL.Should().BeNullOrEmpty();
            else
                response.Result.ThumbnailURL.Should()
                    .NotBeNullOrEmpty()
                    .And.MatchRegex("https://s3.amazonaws.com/bucketname/.*.png");

            repositorymock.Verify(r => r.GetById(fakeRequest.Id), Times.Once);
            repositorymock.Verify(r => r.Update(It.IsAny<News>()), Times.Once);
        }
    }
}