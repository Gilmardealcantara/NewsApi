using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using FluentValidation;
using NewsApi.Application.Dtos;
using NewsApi.Application.Services.Repositories;
using NewsApi.Application.Shared;
using NewsApi.Application.UseCases.Interfaces.News;

namespace NewsApi.Application.UseCases.News
{
    public class CreateNewsUseCase : UseCaseBase<CreateNewsRequest, Entities.News>, ICreateNewsUseCase
    {
        private readonly INewsRepository _newsRepository;
        private readonly IAuthorRepository _authorRepository;
        public CreateNewsUseCase(
            ILogger<CreateNewsUseCase> logger,
            IValidator<CreateNewsRequest> validator,
            INewsRepository newsRepository,
            IAuthorRepository authorRepository)
            : base(logger, validator, ("01", "Error unexpected when create news"))
           => (_newsRepository, _authorRepository) = (newsRepository, authorRepository);

        protected override async Task<UseCaseResponse<Entities.News>> OnExecute(CreateNewsRequest request)
        {
            var author = await _authorRepository.GeyByUserName(request.Author.UserName);
            if (author is null)
            {
                author = request.Author.ToAuthor();
                await _authorRepository.Save(author);
            };

            var news = request.ToNews(author);

            await _newsRepository.Save(news);
            return _response.SetResult(news);
        }
    }
}