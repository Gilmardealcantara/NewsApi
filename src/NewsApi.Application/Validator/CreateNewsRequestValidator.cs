using FluentValidation;
using NewsApi.Application.Dtos;

namespace NewsApi.Application.Validator
{
    public class CreateNewsRequestValidator : AbstractValidator<CreateNewsRequest>
    {
        public CreateNewsRequestValidator()
        {
            RuleFor(x => x).NotNull();
        }
    }
}