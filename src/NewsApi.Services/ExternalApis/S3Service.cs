using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NewsApi.Application.Services.External;

namespace NewsApi.Services.ExternalApis
{
    public class S3Service : IImageStorageService
    {
        ILogger<S3Service> _logger;

        public async Task Delete(string keyName)
        {
            _logger.LogInformation($"S3Services -> Delete {keyName}");
        }

        public async Task<string> Update(string keyName, string localPath)
        {
            _logger.LogInformation($"S3Services -> Update {keyName}");
            return $"http://s3.com/bucketName/{keyName}";
        }

        public async Task<string> Upload(string keyName, string localPath)
        {
            _logger.LogInformation($"S3Services -> Upload {keyName}");
            return $"http://s3.com/bucketName/{keyName}";
        }
    }
}