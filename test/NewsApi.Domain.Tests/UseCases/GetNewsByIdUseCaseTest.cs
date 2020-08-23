using System;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NewsApi.Domain.Entities;
using NewsApi.Domain.Services.Repositories;
using NewsApi.Domain.Shared;
using NewsApi.Domain.Tests.Validadors;
using NewsApi.Domain.UseCases;
using Xunit;

namespace NewsApi.Domain.Tests.UseCases
{
    public class GetNewsByIdUseCaseTest
    {
        [Fact]
        public async Task UseCase_WhenOk_ResultSuccess()
        {
            var fakeNews = new Faker<News>()
                .CustomInstantiator(f => new News(f.Lorem.Sentence(3)))
                .RuleFor(x => x.Id, f => Guid.NewGuid())
                .RuleFor(x => x.Content, f => f.Lorem.Paragraphs(3))
                .Generate();

            var logger = new Mock<ILogger<GetNewsByIdUseCase>>().Object;
            var validator = ValidatorsFactory.GetValidValidator<Guid>();

            var repositoryMock = new Mock<INewsRepository>();
            repositoryMock.Setup(r => r.GetById(fakeNews.Id))
                .ReturnsAsync(fakeNews);

            var useCase = new GetNewsByIdUseCase(logger, validator, repositoryMock.Object);
            var response = await useCase.Execute(fakeNews.Id);

            response.Status.Should().Be(UseCaseResponseStatus.Success);
            response.Result.Id.Should().Be(fakeNews.Id);
            response.Result.Title.Should().Be(fakeNews.Title);
            response.Result.Content.Should().Be(fakeNews.Content);
            repositoryMock.Verify(r => r.GetById(fakeNews.Id), Times.Once);

        }
    }
}