using FluentValidation;
using NewsApi.Application.Dtos;

namespace NewsApi.Application.Validator
{

    public class NewsRequestValidator<T> : AbstractValidator<T> where T : NewsRequest
    {
        public NewsRequestValidator()
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

    public class CreateNewsRequestValidator : NewsRequestValidator<CreateNewsRequest>
    {
        public CreateNewsRequestValidator()
        {
        }
    }

    public class UpdateNewsRequestValidator : NewsRequestValidator<UpdateNewsRequest>
    {
        public UpdateNewsRequestValidator()
        {
            RuleFor(x => x.Id).SetValidator(new GuidValidator());
        }
    }
}