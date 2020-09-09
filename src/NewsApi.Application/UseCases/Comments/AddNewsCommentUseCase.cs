using System;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Extensions.Logging;
using NewsApi.Application.Dtos;
using NewsApi.Application.Services.Repositories;
using NewsApi.Application.Shared;
using NewsApi.Application.UseCases.Interfaces.Comments;

namespace NewsApi.Application.UseCases.Comments
{
    public class AddNewsCommentUseCase
        : UseCaseBase<CreateCommentRequest, Guid>, IAddNewsCommentUseCase
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly INewsRepository _newsRepository;

        public AddNewsCommentUseCase(
            ILogger<UseCaseBase<CreateCommentRequest, Guid>> logger,
            IValidator<CreateCommentRequest> validator,
            IAuthorRepository authorRepository,
            INewsRepository newsRepository)
            : base(logger, validator, ("05", "Error unexpected when add comment int news"))
        {
            _authorRepository = authorRepository;
            _newsRepository = newsRepository;
        }

        protected override async Task<UseCaseResponse<Guid>> OnExecute(CreateCommentRequest request)
        {
            var author = await _authorRepository.GeyByUserName(request.Author.UserName);
            if (author is null)
            {
                author = request.Author.ToAuthor();
                await _authorRepository.Save(author);
            };

            var news = await _newsRepository.GetById(request.NewsId);
            if (news is null)
                return this._response.SetResourceNotFountError();

            var comment = request.ToComment(author);
            await _newsRepository.AddComment(news.Id, comment);

            return _response.SetResult(comment.Id);
        }
    }
}