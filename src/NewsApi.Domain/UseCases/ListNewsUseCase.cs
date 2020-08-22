using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NewsApi.Domain.Entities;
using NewsApi.Domain.Services.Repositories;
using NewsApi.Domain.Shared;

namespace NewsApi.Domain.UseCases
{
    public class ListNewsUseCase : UseCaseBase<IEnumerable<News>>
    {
        INewsRepository _repository;

        public ListNewsUseCase(
            ILogger<ListNewsUseCase> logger,
            INewsRepository repository)
            : base(logger, ("02", "Error unexpected when listing news"))
            => _repository = repository;

        protected override async Task<UseCaseResponse<IEnumerable<News>>> OnExecute()
        {
            var all = await _repository.GetAll();
            return _response.SetResult(all);
        }
    }
}