using System;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Extensions.Logging;
using NewsApi.Application.Dtos;
using NewsApi.Application.Entities;
using NewsApi.Application.Services.Repositories;
using NewsApi.Application.Shared;
using NewsApi.Application.UseCases.Interfaces;

namespace NewsApi.Application.UseCases
{
    public class UpdateNewsUseCase : UseCaseBase<UpdateNewsRequest, News>, IUpdateNewsUseCase
    {
        private readonly INewsRepository _repository;
        private readonly IAuthorRepository _authorRepository;

        public UpdateNewsUseCase(
            ILogger<UpdateNewsUseCase> logger,
            IValidator<UpdateNewsRequest> validator,
            INewsRepository repository,
             IAuthorRepository authorRepository)
            : base(logger, validator, ("03", "Error unexpected when update news"))
           => (_repository, _authorRepository) = (repository, authorRepository);

        protected override async Task<UseCaseResponse<News>> OnExecute(UpdateNewsRequest request)
        {
            var oldNews = await _repository.GetById(request.Id);
            if (oldNews is null)
                return _response.SetResourceNotFountError();

            Author author = oldNews.Author;

            if (oldNews.Author.UserName != request.Author.UserName)
            {
                author = await _authorRepository.GeyByUserName(request.Author.UserName);
                if (author is null)
                {
                    author = request.Author.ToAuthor();
                    await _authorRepository.Save(author);
                };
            }

            var updatedNews = request.ToNews(author);

            await _repository.Update(updatedNews);
            return _response.SetResult(updatedNews);
        }
    }
}