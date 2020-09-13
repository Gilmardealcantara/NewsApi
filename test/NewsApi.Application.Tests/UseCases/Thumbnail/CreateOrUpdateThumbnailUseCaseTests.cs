using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NewsApi.Application.Dtos;
using NewsApi.Application.Services.External;
using NewsApi.Application.Services.Repositories;
using NewsApi.Application.Shared;
using NewsApi.Application.Tests.Builders;
using NewsApi.Application.Tests.Validadors;
using NewsApi.Application.UseCases.Thumbnail;
using Xunit;

namespace NewsApi.Application.Tests.UseCases.Thumbnail
{
    public class CreateOrUpdateThumbnailUseCaseTests
    {
        private readonly ThumbnailRequest _request;
        public CreateOrUpdateThumbnailUseCaseTests()
        {
            _request = new ThumbnailRequest()
            {
                NewsId = Guid.NewGuid(),
                FileName = "image.jpg",
                FileLocalPath = "app/uploads/image.jpg",
                FileLength = 3 * 1024 * 104
            };
        }

        [Fact]
        public async Task UseCase_WhenPersonNewsExists_ReturnNotFound()
        {

            var repo = new Mock<INewsRepository>();
            var imgService = new Mock<IImageStorageService>();
            var validator = ValidatorFactory.GetValidValidator<ThumbnailRequest>();
            var logger = new Mock<ILogger<CreateOrUpdateThumbnailUseCase>>().Object;

            var useCase = new CreateOrUpdateThumbnailUseCase(repo.Object, imgService.Object, logger, validator);
            var response = await useCase.Execute(_request);

            response.Status.Should().Be(UseCaseResponseStatus.ResourceNotFountError);
            response.Errors
                .Should()
                .NotBeNullOrEmpty()
                .And.Contain(x => x.Code == "0.10");
        }

        [Fact]
        public async Task UseCase_WhenCreateThumbnail_ReturnRemoteUrl()
        {

            var repo = new Mock<INewsRepository>();
            var imgService = new Mock<IImageStorageService>();
            var validator = ValidatorFactory.GetValidValidator<ThumbnailRequest>();
            var logger = new Mock<ILogger<CreateOrUpdateThumbnailUseCase>>().Object;

            repo.Setup(r => r.GetById(_request.NewsId))
                .ReturnsAsync(new NewsBuilder(_request.NewsId).Build());
            imgService.Setup(s => s.Save(It.IsAny<string>(), _request.FileLocalPath))
                .ReturnsAsync((string keyName, string path) => $"http://s3.com/bucketName/{keyName}");

            var useCase = new CreateOrUpdateThumbnailUseCase(repo.Object, imgService.Object, logger, validator);
            var response = await useCase.Execute(_request);

            response.Status.Should().Be(UseCaseResponseStatus.Success);
            response.Result
                .Should()
                .NotBeNullOrEmpty()
                .And.Contain("http");
        }

        [Fact]
        public async Task UseCase_WhenUpdateThumbnail_ReturnRemoteUrl()
        {

            var repo = new Mock<INewsRepository>();
            var imgService = new Mock<IImageStorageService>();
            var validator = ValidatorFactory.GetValidValidator<ThumbnailRequest>();
            var logger = new Mock<ILogger<CreateOrUpdateThumbnailUseCase>>().Object;

            repo.Setup(r => r.GetById(_request.NewsId))
                .ReturnsAsync(new NewsBuilder(_request.NewsId).WithThumbnailURL("http://s3.com/bucketName/ab123cdf3243.jpg").Build());
            imgService.Setup(s => s.Update(It.IsAny<string>(), _request.FileLocalPath))
                .ReturnsAsync((string keyName, string path) => $"http://s3.com/bucketName/{keyName}");

            var useCase = new CreateOrUpdateThumbnailUseCase(repo.Object, imgService.Object, logger, validator);
            var response = await useCase.Execute(_request);

            response.Status.Should().Be(UseCaseResponseStatus.Success);
            response.Result
                .Should()
                .NotBeNullOrEmpty()
                .And.Contain("http");
        }
    }
}