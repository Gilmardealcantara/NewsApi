using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using NewsApi.Application.Shared;

namespace NewsApi.Api
{
    public class UseCaseResponseToActionResult
    {
        public static IActionResult Converter<T>(UseCaseResponse<T> response)
            => (response.Status) switch
            {
                UseCaseResponseStatus.Success => new OkObjectResult(response.Result),
                UseCaseResponseStatus.ValidateError => new BadRequestObjectResult(response.Errors),
                UseCaseResponseStatus.ResourceNotFountError => new NotFoundObjectResult(response.Errors),
                _ => new ObjectResult(response.Errors) { StatusCode = (int)HttpStatusCode.InternalServerError }
            };
    }
}
