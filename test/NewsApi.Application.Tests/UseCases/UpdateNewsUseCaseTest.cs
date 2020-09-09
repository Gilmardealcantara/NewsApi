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

            var repositorymock = new Mock<INewsRepository>();
            repositorymock.Setup(r => r.GetById(fakeRequest.Id)).ReturnsAsync((News)null);

            var useCase = new UpdateNewsUseCase(logger, validator, repositorymock.Object);
            var response = await useCase.Execute(fakeRequest);

            response.Status.Should().Be(UseCaseResponseStatus.ResourceNotFountError);
            response.Success().Should().BeFalse();

            repositorymock.Verify(r => r.GetById(fakeRequest.Id), Times.Once);
            repositorymock.Verify(r => r.Update(It.IsAny<News>()), Times.Never);
        }
    }
}