using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using Moq;
using NewsApi.Domain.Dtos;
using NewsApi.Domain.Entities;
using NewsApi.Domain.Services.Repositories;
using NewsApi.Domain.Shared;
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
            var validatorMock = new Mock<IValidator<UpdateNewsRequest>>();
            validatorMock
                .Setup(v => v.ValidateAsync(fakeRequest, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());

            var repositorymock = new Mock<INewsRepository>();

            var useCase = new UpdateNewsUseCase(logger, validatorMock.Object, repositorymock.Object);
            var response = await useCase.Execute(fakeRequest);

            response.Status.Should().Be(UseCaseResponseStatus.Success);
            response.Success().Should().BeTrue();
            response.Result.Should().Be(fakeRequest.Id);

            repositorymock.Verify(r => r.Update(It.IsAny<News>()), Times.Once);
        }
    }
}