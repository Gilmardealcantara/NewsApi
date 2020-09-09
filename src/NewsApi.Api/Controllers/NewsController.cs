using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewsApi.Application.Dtos;
using NewsApi.Application.UseCases.Interfaces;

namespace NewsApi.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly ILogger<NewsController> _logger;

        public NewsController(ILogger<NewsController> logger)
            => _logger = logger;

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<NewsListItem>), 200)]
        public async Task<IActionResult> Get([FromServices] IListNewsUseCase useCase)
           => UseCaseResponseToActionResult.Converter(await useCase.Execute());

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(NewsResponse), 200)]
        public async Task<IActionResult> GetById([FromServices] IGetNewsByIdUseCase useCase, Guid id)
            => UseCaseResponseToActionResult.Converter(await useCase.Execute(id));

        [HttpPost]
        public async Task<IActionResult> Post([FromServices] ICreateNewsUseCase useCase, CreateNewsRequest request)
            => UseCaseResponseToActionResult.Converter(await useCase.Execute(request));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromServices] IUpdateNewsUseCase useCase, Guid id, UpdateNewsRequest request)
            => UseCaseResponseToActionResult.Converter(await useCase.Execute(request.WithId(id)));
    }
}
