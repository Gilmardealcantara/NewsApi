using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NewsApi.Application.Dtos;
using NewsApi.Application.Entities;
using NewsApi.Application.Services.Repositories;
using NewsApi.Application.Shared;
using NewsApi.Application.Tests.Builders;
using NewsApi.Application.Tests.Validadors;
using NewsApi.Application.UseCases.News;
using Xunit;

namespace NewsApi.Application.Tests.UseCases.News
{
    public class UpdateNewsUseCaseTests
    {

        [Fact]
        public async Task UseCase_WhenNewsAuthorNotchanged_ReturnUpdatedNews()
        {

            var oldNews = new NewsBuilder().Build();
            var fakeRequest = new NewsRequestBuilder(oldNews.Id)
                .WithTitle("New Title")
                .WithContent("New Content")
                .WithAuthorUserName(oldNews.Author.UserName)
                .BuildUpdate();

            var logger = new Mock<ILogger<UpdateNewsUseCase>>().Object;
            var validator = ValidatorFactory.GetValidValidator<UpdateNewsRequest>();

            var newsRepositoryMock = new Mock<INewsRepository>();
            newsRepositoryMock.Setup(r => r.GetById(fakeRequest.Id)).ReturnsAsync(oldNews);
            var authorRepositoryMock = new Mock<IAuthorRepository>();


            var useCase = new UpdateNewsUseCase(logger, validator, newsRepositoryMock.Object, authorRepositoryMock.Object);
            var response = await useCase.Execute(fakeRequest);

            response.Status.Should().Be(UseCaseResponseStatus.Success);
            response.Success().Should().BeTrue();

            newsRepositoryMock.Verify(r => r.GetById(fakeRequest.Id), Times.Once);
            newsRepositoryMock.Verify(r => r.Update(It.IsAny<Entities.News>()), Times.Once);
            authorRepositoryMock.Verify(r => r.GeyByUserName(fakeRequest.Author.UserName), Times.Never);
            authorRepositoryMock.Verify(r => r.Save(It.IsAny<Author>()), Times.Never);
        }


        [Fact]
        public async Task UseCase_WhenNewsAuthorChangedAndExistsInDb_ReturnUpdatedNews()
        {

            var oldNews = new NewsBuilder().Build();
            var fakeRequest = new NewsRequestBuilder(oldNews.Id)
                .WithTitle("New Title")
                .WithContent("New Content")
                .WithAuthorUserName("jose@gmail.com")
                .BuildUpdate();

            var logger = new Mock<ILogger<UpdateNewsUseCase>>().Object;
            var validator = ValidatorFactory.GetValidValidator<UpdateNewsRequest>();

            var newsRepositoryMock = new Mock<INewsRepository>();
            newsRepositoryMock.Setup(r => r.GetById(fakeRequest.Id)).ReturnsAsync(oldNews);
            var authorRepositoryMock = new Mock<IAuthorRepository>();
            authorRepositoryMock.Setup(r => r.GeyByUserName(fakeRequest.Author.UserName))
                .ReturnsAsync(fakeRequest.Author.ToAuthor());

            var useCase = new UpdateNewsUseCase(logger, validator, newsRepositoryMock.Object, authorRepositoryMock.Object);
            var response = await useCase.Execute(fakeRequest);

            response.Status.Should().Be(UseCaseResponseStatus.Success);
            response.Success().Should().BeTrue();

            newsRepositoryMock.Verify(r => r.GetById(fakeRequest.Id), Times.Once);
            newsRepositoryMock.Verify(r => r.Update(It.IsAny<Entities.News>()), Times.Once);
            authorRepositoryMock.Verify(r => r.GeyByUserName(fakeRequest.Author.UserName), Times.Once);
            authorRepositoryMock.Verify(r => r.Save(It.IsAny<Author>()), Times.Never);
        }

        [Fact]
        public async Task UseCase_WhenNewsAuthorChangedAndNotExistsInDb_ReturnUpdatedNews()
        {

            var oldNews = new NewsBuilder().Build();
            var fakeRequest = new NewsRequestBuilder(oldNews.Id)
                .WithTitle("New Title")
                .WithContent("New Content")
                .WithAuthorUserName("jose@gmail.com")
                .BuildUpdate();

            var logger = new Mock<ILogger<UpdateNewsUseCase>>().Object;
            var validator = ValidatorFactory.GetValidValidator<UpdateNewsRequest>();

            var newsRepositoryMock = new Mock<INewsRepository>();
            newsRepositoryMock.Setup(r => r.GetById(fakeRequest.Id)).ReturnsAsync(oldNews);
            var authorRepositoryMock = new Mock<IAuthorRepository>();

            var useCase = new UpdateNewsUseCase(logger, validator, newsRepositoryMock.Object, authorRepositoryMock.Object);
            var response = await useCase.Execute(fakeRequest);

            response.Status.Should().Be(UseCaseResponseStatus.Success);
            response.Success().Should().BeTrue();

            newsRepositoryMock.Verify(r => r.GetById(fakeRequest.Id), Times.Once);
            newsRepositoryMock.Verify(r => r.Update(It.IsAny<Entities.News>()), Times.Once);
            authorRepositoryMock.Verify(r => r.GeyByUserName(fakeRequest.Author.UserName), Times.Once);
            authorRepositoryMock.Verify(r => r.Save(It.IsAny<Author>()), Times.Once);
        }


        [Fact]
        public async Task UseCase_WhenNewsNotExists_ReturnNotFoundError()
        {

            var oldNews = new NewsBuilder().Build();
            var fakeRequest = new NewsRequestBuilder(Guid.NewGuid())
                .WithTitle("New Title")
                .WithContent("New Content")
                .WithAuthorUserName("jose@gmail.com")
                .BuildUpdate();

            var logger = new Mock<ILogger<UpdateNewsUseCase>>().Object;
            var validator = ValidatorFactory.GetValidValidator<UpdateNewsRequest>();

            var repositorymock = new Mock<INewsRepository>();
            var authorRepositoryMock = new Mock<IAuthorRepository>();


            var useCase = new UpdateNewsUseCase(logger, validator, repositorymock.Object, authorRepositoryMock.Object);
            var response = await useCase.Execute(fakeRequest);

            response.Status.Should().Be(UseCaseResponseStatus.ResourceNotFountError);
            response.Success().Should().BeFalse();

            repositorymock.Verify(r => r.GetById(fakeRequest.Id), Times.Once);
            repositorymock.Verify(r => r.Update(It.IsAny<Entities.News>()), Times.Never);
            authorRepositoryMock.Verify(r => r.GeyByUserName(fakeRequest.Author.UserName), Times.Never);
            authorRepositoryMock.Verify(r => r.Save(It.IsAny<Author>()), Times.Never);
        }
    }
}