using FluentValidation;
using NewsApi.Application.Dtos;

namespace NewsApi.Application.Validator
{
    public class UpdateNewsRequestValidator : AbstractValidator<UpdateNewsRequest>
    {
        public UpdateNewsRequestValidator()
        {
            RuleFor(x => x).NotNull();
            RuleFor(x => x.Id).SetValidator(new GuidValidator());
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