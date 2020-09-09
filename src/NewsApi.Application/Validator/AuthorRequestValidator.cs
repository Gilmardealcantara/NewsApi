using FluentValidation;
using NewsApi.Application.Dtos;
using NewsApi.Application.Entities;

namespace NewsApi.Application.Validator
{
    public class AuthorRequestValidator : AbstractValidator<AuthorRequest>
    {
        public AuthorRequestValidator()
        {
            RuleFor(x => x).NotNull();
            RuleFor(x => x.UserName)
                .NotEmpty()
                .MaximumLength(255)
                .EmailAddress();

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(255);
        }
    }
}