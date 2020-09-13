using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NewsApi.Application.Dtos;
using NewsApi.Application.Services.ExternalApis;
using NewsApi.Application.Services.Repositories;
using NewsApi.Application.Shared;
using NewsApi.Application.Tests.Builders;
using NewsApi.Application.Tests.Validadors;
using NewsApi.Application.UseCases.Thumbnail;
using Xunit;

namespace NewsApi.Application.Tests.UseCases.Thumbnail
{
    public class CrudNewsThumbnailUseCaseTests
    {
        private readonly ThumbnailRequest _request;
        public CrudNewsThumbnailUseCaseTests()
        {
            _request = new ThumbnailRequest()
            {
                NewsId = Guid.NewGuid(),
                Type = ThumbnailRequestType.Create,
                FileName = "image.jpg",
                FileLocalPath = "app/uploads/image.jpg",
                FileLength = 3 * 1024 * 104
            };
        }

        [Fact]
        public async Task UseCase_WhenCrateThumbnail_ReturnRemoteUrl()
        {

            var repo = new Mock<INewsRepository>();
            var imgService = new Mock<IImageStorageService>();
            var validator = ValidatorFactory.GetValidValidator<ThumbnailRequest>();
            var logger = new Mock<ILogger<CrudNewsThumbnailUseCase>>().Object;

            repo.Setup(r => r.GetById(_request.NewsId))
                .ReturnsAsync(new NewsBuilder(_request.NewsId).Build());
            imgService.Setup(s => s.Upload(It.IsAny<string>(), _request.FileLocalPath))
                .ReturnsAsync((string keyName, string path) => $"http://s3.com/bucketName/{keyName}");

            var useCase = new CrudNewsThumbnailUseCase(repo.Object, imgService.Object, logger, validator);
            var response = await useCase.Execute(_request);

            response.Status.Should().Be(UseCaseResponseStatus.Success);
            response.Result
                .Should()
                .NotBeNullOrEmpty()
                .And.Contain("http");
        }
    }
}