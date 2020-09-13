using System.Threading.Tasks;

namespace NewsApi.Application.Services.External
{
    public interface IImageStorageService
    {
        Task<string> Upload(string keyName, string localPath);
        Task<string> Update(string keyName, string localPath);
        Task Delete(string keyName);
    }
}