using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using NewsApi.Api.Extensions;
using NewsApi.Application.Dtos;
using NewsApi.Application.Shared;
using NewsApi.Application.UseCases.Interfaces.Thumbnail;

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
            [FromServices] ICreateOrUpdateThumbnailUseCase useCase,
            [FromRoute] Guid newsId,
            [FromForm] IFormFile file)
        {

            var fileLocalPath = await FormFileExtensions.GetLocalPath(file, _environment);
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

            System.IO.File.Delete(request.FileLocalPath);

            return UseCaseResponseToActionResult.Converter(result);
        }
    }
}