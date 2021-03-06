﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewsApi.Application.Dtos;
using NewsApi.Application.UseCases.Interfaces.News;

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

        [HttpPost, Authorize]
        public async Task<IActionResult> Post([FromServices] ICreateNewsUseCase useCase, CreateNewsRequest request)
        {
            var author = this.GetAuthorFromClaims();
            var response = await useCase.Execute((CreateNewsRequest)request.WithAuthor(author));
            return UseCaseResponseToActionResult.Converter(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromServices] IUpdateNewsUseCase useCase, Guid id, UpdateNewsRequest request)
        {
            var author = this.GetAuthorFromClaims();
            var response = await useCase.Execute((UpdateNewsRequest)request.WithId(id).WithAuthor(author));
            return UseCaseResponseToActionResult.Converter(response);
        }

        private AuthorRequest GetAuthorFromClaims()
        {
            var userName = this.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            var name = this.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
            return new AuthorRequest
            {
                UserName = userName,
                Name = name
            };
        }
    }
}
