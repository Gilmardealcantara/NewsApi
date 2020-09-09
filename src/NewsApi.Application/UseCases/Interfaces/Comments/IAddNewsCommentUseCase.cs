using System;
using NewsApi.Application.Dtos;
using NewsApi.Application.Shared;

namespace NewsApi.Application.UseCases.Interfaces.Comments
{
    public interface IAddNewsCommentUseCase : IUseCase<CreateCommentRequest, Guid>
    {
    }
}