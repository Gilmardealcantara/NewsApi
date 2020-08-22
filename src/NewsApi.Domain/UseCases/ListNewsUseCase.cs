using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NewsApi.Domain.Dtos;
using NewsApi.Domain.Entities;
using NewsApi.Domain.Services.Repositories;
using NewsApi.Domain.Shared;

namespace NewsApi.Domain.UseCases
{
    public class ListNewsUseCase : UseCaseBase<IEnumerable<NewsListItem>>
    {
        private readonly INewsRepository _repository;
        private readonly int _contentPreviewSizeLimit = 100;
        public ListNewsUseCase(
            ILogger<ListNewsUseCase> logger,
            INewsRepository repository)
            : base(logger, ("02", "Error unexpected when listing news"))
            => _repository = repository;

        protected override async Task<UseCaseResponse<IEnumerable<NewsListItem>>> OnExecute()
        {
            var all = await _repository.GetAll();
            var listNews = all.Select(news => new NewsListItem
            {
                Id = news.Id,
                Title = news.Title,
                ContentPreview = news.Content.Length > _contentPreviewSizeLimit
                    ? news.Content.Substring(0, _contentPreviewSizeLimit)
                    : news.Content
            });
            return _response.SetResult(listNews);
        }
    }
}