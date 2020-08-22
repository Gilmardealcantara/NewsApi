using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NewsApi.Domain.Entities;
using NewsApi.Domain.Services.Repositories;
using NewsApi.Domain.Shared;
using NewsApi.Domain.UseCases;
using Xunit;

namespace NewsApi.Domain.Tests.UseCases
{
    public class IListNewsUseCaseTest
    {
        [Fact]
        public async Task UseCase_WhenOk_ReturnListOfNews()
        {
            //Given
            var newsList = new Faker<News>()
                .CustomInstantiator(f => new News(f.Lorem.Sentence(3)))
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
                .And.NotContain(x => x.Title == null);

            repositoryMock.Verify(r => r.GetAll(), Times.Once);

        }
    }
}