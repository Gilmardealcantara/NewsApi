using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using FluentAssertions;
using Moq;
using NewsApi.Domain.Dtos;
using NewsApi.Domain.Entities;
using NewsApi.Domain.Services.Repositories;
using NewsApi.Domain.UseCases;
using Xunit;
using Bogus;
using NewsApi.Domain.Tests.Validadors;

namespace NewsApi.Domain.Tests.UseCases
{
    public class CreateNewsUseCaseTests
    {
        [Fact]
        public async Task UseCase_WhenOk_ReturnSuccess()
        {
            //Given
            var request = new Faker<CreateNewsRequest>()
                .RuleFor(x => x.Title, f => f.Lorem.Sentence(3))
                .RuleFor(x => x.Content, f => f.Lorem.Paragraphs())
                .Generate();

            var logger = new Mock<ILogger<CreateNewsUseCase>>().Object;
            var validator = ValidatorsFactory.GetValidValidator<CreateNewsRequest>();

            var repositoryMock = new Mock<INewsRepository>();
            repositoryMock.Setup(r => r.Save(It.IsAny<News>()));

            //When
            var useCase = new CreateNewsUseCase(logger, validator, repositoryMock.Object);
            var response = await useCase.Execute(request);

            //Then
            response.Success().Should().BeTrue();
            response.Result.Title.Should().Be(request.Title);
            response.Result.Content.Should().Be(request.Content);
            response.Result.Id.Should().NotBeEmpty();
            repositoryMock.Verify(r => r.Save(It.IsAny<News>()), Times.Once);
        }
    }
}