using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using FluentAssertions;
using FluentValidation;
using Moq;
using NewsApi.Domain.Shared;
using Xunit;
using NewsApi.Domain.Tests.Validadors;

namespace NewsApi.Domain.Tests.UseCases
{
    public class BaseUseCaseTests
    {
        [Fact]
        public async Task UseCaseBase_WhenRequestIsInvalid_ReturnValidateError()
        {
            var logger = new Mock<ILogger<SpecificUseCase>>().Object;
            var validator = ValidatorsFactory.GetInValidValidator<int>("mockCode");

            var useCase = new SpecificUseCase(logger, validator);
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
            var validator = ValidatorsFactory.GetValidValidator<int>();

            //When
            var useCase = new SpecificUseCase(logger, validator);
            var response = await useCase.Execute(0);

            //Then
            response.Status.Should().Be(UseCaseResponseStatus.ExceptionError);
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
            response.Status.Should().Be(UseCaseResponseStatus.ExceptionError);
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