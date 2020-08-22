using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using NewsApi.Domain.Dtos;
using NewsApi.Domain.Entities;
using NewsApi.Domain.Services.Repositories;
using NewsApi.Domain.UseCases;
using Xunit;

namespace NewsApi.Domain.Tests.UseCases
{
    public class CreateNewsUseCaseTests
    {
        [Fact]
        public async Task UseCase_WhenOk_ReturnSuccess()
        {
            //Given
            var request = new CreateNewsRequest { Title = "My News" };
            var logger = new Mock<ILogger<CreateNewsUseCase>>().Object;

            var validatorMock = new Mock<IValidator<CreateNewsRequest>>();
            validatorMock.Setup(v => v.ValidateAsync(request, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());

            var repositoryMock = new Mock<INewsRepository>();
            repositoryMock.Setup(r => r.Save(It.IsAny<News>()));

            //When
            var useCase = new CreateNewsUseCase(logger, validatorMock.Object, repositoryMock.Object);
            var response = await useCase.Execute(request);

            //Then
            response.Success().Should().BeTrue();
            response.Result?.Title.Should().Be("My News");
            response.Result?.Id.Should().NotBeEmpty();
            repositoryMock.Verify(r => r.Save(It.IsAny<News>()), Times.Once);
        }
    }
}