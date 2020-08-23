using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NewsApi.Domain.Dtos;
using NewsApi.Domain.Entities;
using NewsApi.Domain.Services.ExternalApis;
using NewsApi.Domain.Services.Repositories;
using NewsApi.Domain.Shared;
using NewsApi.Domain.Tests.Validadors;
using NewsApi.Domain.UseCases;
using Xunit;

namespace NewsApi.Domain.Tests.UseCases
{
    public class UpdateNewsUseCaseTest
    {
        [Fact]
        public async Task UseCase_WhenOk_ReturnSuccess()
        {
            var fakeRequest = new UpdateNewsRequest { Id = Guid.NewGuid(), Title = "New Title" };

            var logger = new Mock<ILogger<UpdateNewsUseCase>>().Object;
            var validator = ValidatorsFactory.GetValidValidator<UpdateNewsRequest>();
            var imageStorage = new Mock<IImageStorageService>().Object;

            var repositorymock = new Mock<INewsRepository>();
            repositorymock.Setup(r => r.GetById(fakeRequest.Id)).ReturnsAsync(new News("Teste", "OPA", new Author("gilmar.alcantara@gmail.com")));

            var useCase = new UpdateNewsUseCase(logger, validator, repositorymock.Object, imageStorage);
            var response = await useCase.Execute(fakeRequest);

            response.Status.Should().Be(UseCaseResponseStatus.Success);
            response.Success().Should().BeTrue();
            response.Result.Should().Be(fakeRequest.Id);

            repositorymock.Verify(r => r.GetById(fakeRequest.Id), Times.Once);
            repositorymock.Verify(r => r.Update(It.IsAny<News>()), Times.Once);
        }

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
    }
}