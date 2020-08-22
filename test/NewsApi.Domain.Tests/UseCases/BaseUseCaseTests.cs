using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using NewsApi.Domain.Shared;
using Xunit;

namespace NewsApi.Domain.Tests.UseCases
{
    public class BaseUseCaseTests
    {
        [Fact]
        public async Task UseCaseBase_WhenRequestIsInvalid_ReturnValidateError()
        {
            var logger = new Mock<ILogger<SpecificUseCase>>().Object;
            var validatorMock = new Mock<IValidator<int>>();
            validatorMock.Setup(v => v.ValidateAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult(new ValidationFailure[] {
                    new ValidationFailure("mockPropName", "mockMessage") {ErrorCode = "mockCode"}
                }));

            var useCase = new SpecificUseCase(logger, validatorMock.Object);
            var response = await useCase.Execute(5);

            response.Status.Should().Be(UseCaseResponseStatus.ValidateError);
            response.Success().Should().BeFalse();
            response.Errors
                .Should().NotBeNullOrEmpty()
                .And.ContainSingle(x => x.Code == "mockCode");
        }

        [Fact]
        public async Task UseCaseBase_WhenThrowsAnException_Returnerror()
        {
            //Given
            var logger = new Mock<ILogger<SpecificUseCase>>().Object;

            var validatorMock = new Mock<IValidator<int>>();
            validatorMock.Setup(v => v.Validate(It.IsAny<int>())).Returns(new ValidationResult());

            //When
            var useCase = new SpecificUseCase(logger, validatorMock.Object);
            var response = await useCase.Execute(0);

            //Then
            response.Status.Should().Be(UseCaseResponseStatus.GenericExceptionError);
            response.Success().Should().BeFalse();
            response.Errors
                .Should().NotBeNullOrEmpty()
                .And.ContainSingle(x => x.Code == "errorCode");
        }

        [Fact]
        public async Task UseCaseBaseOnlyResponse_WhenThrowsAnException_Returnerror()
        {
            //Given
            var logger = new Mock<ILogger<SpecificOnlyResponseUseCase>>().Object;

            //When
            var useCase = new SpecificOnlyResponseUseCase(logger);
            var response = await useCase.Execute();

            //Then
            response.Status.Should().Be(UseCaseResponseStatus.GenericExceptionError);
            response.Success().Should().BeFalse();
            response.Errors
                .Should().NotBeNullOrEmpty()
                .And.ContainSingle(x => x.Code == "errorCode");
        }
    }

    public class SpecificUseCase : UseCaseBase<int, int>
    {
        public SpecificUseCase(
            ILogger<SpecificUseCase> logger,
            IValidator<int> validator) : base(logger, validator, ("errorCode", "errorMsg")) { }

        protected override Task<UseCaseResponse<int>> OnExecute(int request)
        {
            return Task.Run(() => this._response.SetResult(10 / request));
        }
    }

    public class SpecificOnlyResponseUseCase : UseCaseBase<int>
    {
        public SpecificOnlyResponseUseCase(
            ILogger<SpecificOnlyResponseUseCase> logger) : base(logger, ("errorCode", "errorMsg")) { }

        protected override Task<UseCaseResponse<int>> OnExecute()
        {
            throw new Exception();
        }
    }
}