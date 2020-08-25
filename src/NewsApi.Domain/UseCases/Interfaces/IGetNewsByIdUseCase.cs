using System;
using NewsApi.Domain.Dtos;
using NewsApi.Domain.Shared;

namespace NewsApi.Domain.UseCases.Interfaces
{
    public interface IGetNewsByIdUseCase : IUseCase<Guid, NewsResponse>
    {

    }
}