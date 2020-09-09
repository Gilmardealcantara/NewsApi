using System;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using NewsApi.Application.Validator;

namespace NewsApi.Api.Configurations
{
    public static class ValidatorConfig
    {
        public static void ConfigureValidator(this IServiceCollection services)
        {
            services.AddTransient<IValidator<Guid>, GuidValidator>();
        }
    }
}