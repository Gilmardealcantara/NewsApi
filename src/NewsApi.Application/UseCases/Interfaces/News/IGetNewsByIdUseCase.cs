using System;
using NewsApi.Application.Dtos;
using NewsApi.Application.Shared;

namespace NewsApi.Application.UseCases.Interfaces.News
{
    public interface IGetNewsByIdUseCase : IUseCase<Guid, NewsResponse>
    {

    }
}