using System;
using FluentValidation;

namespace NewsApi.Application.Validator
{
    public class GuidValidator : AbstractValidator<Guid>
    {
        public GuidValidator()
        {
            RuleFor(x => x)
                .NotNull()
                .NotEmpty();
        }
    }
}