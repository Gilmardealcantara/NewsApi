using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using FluentAssertions;
using Moq;
using NewsApi.Application.Dtos;
using NewsApi.Application.Entities;
using NewsApi.Application.Services.Repositories;
using NewsApi.Application.UseCases;
using Xunit;
using Bogus;
using NewsApi.Application.Tests.Validadors;
using NewsApi.Application.UseCases.News;
using NewsApi.Application.Tests.Builders;

namespace NewsApi.Application.Tests.UseCases.News
{
    public class CreateNewsUseCaseTests
    {
        [Fact]
        public async Task UseCase_WhenAuthorExists_ReturnSuccess()
        {
            //Given
            var request = new NewsRequestBuilder().BuildCreate();

            var logger = new Mock<ILogger<CreateNewsUseCase>>().Object;
            var validator = ValidatorFactory.GetValidValidator<CreateNewsRequest>();

            var repositoryMock = new Mock<INewsRepository>();

            var authorRepositoryMock = new Mock<IAuthorRepository>();
            authorRepositoryMock.Setup(r => r.GeyByUserName(request.Author.UserName))
                .ReturnsAsync(request.Author.ToAuthor());

            //When
            var useCase = new CreateNewsUseCase(logger, validator, repositoryMock.Object, authorRepositoryMock.Object);
            var response = await useCase.Execute(request);

            //Then
            response.Success().Should().BeTrue();
            response.Result.Title.Should().Be(request.Title);
            response.Result.Content.Should().Be(request.Content);
            response.Result.Id.Should().NotBeEmpty();
            response.Result.Author.Should().NotBeNull();
            response.Result.Author.Id.Should().NotBeEmpty();

            repositoryMock.Verify(r => r.Save(It.IsAny<Entities.News>()), Times.Once);
            authorRepositoryMock.Verify(r => r.GeyByUserName(request.Author.UserName), Times.Once);
            authorRepositoryMock.Verify(r => r.Save(It.IsAny<Author>()), Times.Never);

        }

        [Fact]
        public async Task UseCase_WhenAuthorNotExists_ReturnSuccessAndCreateAuthor()
        {
            //Given
            var request = new NewsRequestBuilder().BuildCreate();

            var logger = new Mock<ILogger<CreateNewsUseCase>>().Object;
            var validator = ValidatorFactory.GetValidValidator<CreateNewsRequest>();

            var repositoryMock = new Mock<INewsRepository>();
            var authorRepositoryMock = new Mock<IAuthorRepository>();

            //When
            var useCase = new CreateNewsUseCase(logger, validator, repositoryMock.Object, authorRepositoryMock.Object);
            var response = await useCase.Execute(request);

            //Then
            response.Success().Should().BeTrue();
            response.Result.Title.Should().Be(request.Title);
            response.Result.Content.Should().Be(request.Content);
            response.Result.Id.Should().NotBeEmpty();
            response.Result.Author.Should().NotBeNull();
            response.Result.Author.Id.Should().NotBeEmpty();

            repositoryMock.Verify(r => r.Save(It.IsAny<Entities.News>()), Times.Once);
            authorRepositoryMock.Verify(r => r.GeyByUserName(request.Author.UserName), Times.Once);
            authorRepositoryMock.Verify(r => r.Save(It.IsAny<Author>()), Times.Once);
        }
    }
}