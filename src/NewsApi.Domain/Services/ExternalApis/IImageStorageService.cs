using System.Threading.Tasks;

namespace NewsApi.Domain.Services.ExternalApis
{
    public interface IImageStorageService
    {
        Task<string> Upload(string keyName, string localPath);
        Task<string> Update(string keyName, string localPath);
        Task<string> Delete(string keyName);
    }
}