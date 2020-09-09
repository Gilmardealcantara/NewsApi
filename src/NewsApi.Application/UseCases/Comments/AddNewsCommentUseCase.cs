using System;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Extensions.Logging;
using NewsApi.Application.Dtos;
using NewsApi.Application.Shared;
using NewsApi.Application.UseCases.Interfaces.Comments;

namespace NewsApi.Application.UseCases.Comments
{
    public class AddNewsCommentUseCase : UseCaseBase<CreateCommentRequest, Guid>, IAddNewsCommentUseCase
    {
        public AddNewsCommentUseCase(
            ILogger<UseCaseBase<CreateCommentRequest, Guid>> logger,
            IValidator<CreateCommentRequest> validator) : base(logger, validator, ("05", "Error unexpected when add comment int news"))
        {
        }

        protected override Task<UseCaseResponse<Guid>> OnExecute(CreateCommentRequest request)
        {
            throw new NotImplementedException();
        }
    }
}