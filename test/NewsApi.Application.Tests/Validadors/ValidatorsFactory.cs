using System.Threading;
using FluentValidation;
using FluentValidation.Results;
using Moq;

namespace NewsApi.Application.Tests.Validadors
{
    public static class ValidatorFactory
    {
        public static IValidator<T> GetValidValidator<T>()
        {
            var validatorMock = new Mock<IValidator<T>>();
            validatorMock.Setup(v => v.ValidateAsync(It.IsAny<T>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new FluentValidation.Results.ValidationResult());
            return validatorMock.Object;
        }

        public static IValidator<T> GetInValidValidator<T>(string errorCode)
        {
            var validatorMock = new Mock<IValidator<T>>();
            validatorMock.Setup(v => v.ValidateAsync(It.IsAny<T>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult(new ValidationFailure[] {
                            new ValidationFailure("mockPropName", "mockMessage") {ErrorCode = errorCode}
            }));
            return validatorMock.Object;
        }
    }
}