using NewsApi.Application.Dtos;
using NewsApi.Application.Shared;

namespace NewsApi.Application.UseCases.Interfaces.Thumbnail
{
    public interface ICreateOrUpdateThumbnailUseCase : IUseCase<ThumbnailRequest, string>
    {

    }
}