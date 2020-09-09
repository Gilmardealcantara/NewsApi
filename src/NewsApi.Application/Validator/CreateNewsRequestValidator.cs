using FluentValidation;
using NewsApi.Application.Dtos;

namespace NewsApi.Application.Validator
{
    public class CreateNewsRequestValidator : AbstractValidator<CreateNewsRequest>
    {
        public CreateNewsRequestValidator()
        {
            RuleFor(x => x).NotNull();
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(255);

            RuleFor(x => x.Content)
                .NotEmpty();

            RuleFor(x => x.Author)
                .SetValidator(new AuthorRequestValidator());
        }
    }
}