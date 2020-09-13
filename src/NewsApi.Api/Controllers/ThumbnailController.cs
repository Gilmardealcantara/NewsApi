using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using NewsApi.Application.Dtos;
using NewsApi.Api.Extensions;
using NewsApi.Application.Shared;
using NewsApi.Application.UseCases.Thumbnail;

namespace NewsApi.Api.Controllers
{
    [ApiController]
    [Route("news")]
    public class ThumbnailController : ControllerBase
    {
        private readonly IHostEnvironment _environment;

        public ThumbnailController(IHostEnvironment environment)
            => _environment = environment;

        [HttpPost("{newsId}/thumbnail")]
        public async Task<IActionResult> Post(
            [FromServices] CreateOrUpdateThumbnailUseCase useCase,
            [FromRoute] Guid newsId,
            [FromForm] IFormFile file)
        {

            var fileLocalPath = await file?.GetLocalPath(_environment);
            if (fileLocalPath is null)
                return BadRequest(new[] { new ErrorMessage("09.93", "Invalid file, Expected multipart/form-data Content-Type") });

            var request = new ThumbnailRequest
            {
                NewsId = newsId,
                FileLength = file.Length,
                FileLocalPath = fileLocalPath,
                FileName = file.FileName,
            };

            var result = await useCase.Execute(request);

            return UseCaseResponseToActionResult.Converter(result);
        }
    }
}