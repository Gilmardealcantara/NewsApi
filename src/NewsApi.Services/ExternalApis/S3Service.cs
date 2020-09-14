using System;
using System.Threading.Tasks;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.Extensions.Logging;
using NewsApi.Application.Services.External;
using NewsApi.Application.Shared;

namespace NewsApi.Services.ExternalApis
{
    public class S3Service : IImageStorageService
    {
        private static IAmazonS3 _s3Client;
        private readonly ApplicationConfig _config;

        private readonly ILogger<S3Service> _logger;

        public S3Service(ApplicationConfig config, ILogger<S3Service> logger)
        {
            _config = config;
            _logger = logger;
            // var region = RegionEndpoint.GetBySystemName(_config.S3.AwsRegionEndpoint);
            // _s3Client = new AmazonS3Client(_config.S3.AwsAccessKeyId, _config.S3.AwsSecretAccessKey, region);
            AmazonS3Config s3Config = new AmazonS3Config
            {
                ServiceURL = _config.S3.AwsEndpointUrl,
                UseHttp = true,
                ForcePathStyle = true,
                AuthenticationRegion = _config.S3.AwsRegionEndpoint,
            };
            AWSCredentials creds = new AnonymousAWSCredentials();

            _s3Client = new AmazonS3Client(creds, s3Config);
        }

        public string GetFullUrl(string fileName)
        {
            return $"{_config.S3.AwsEndpointUrl}/{_config.S3.AwsBucketName}/{fileName}";
            // return $"https://{_config.S3.AwsBucketName}.s3.amazonaws.com/{fileName}";
        }


        public async Task<string> Save(string keyName, string localPath)
        {
            _logger.LogInformation($"Executing S3Service.Save for image {keyName}");
            try
            {
                var fileTransferUtility =
                            new TransferUtility(_s3Client);

                await fileTransferUtility.UploadAsync(localPath, _config.S3.AwsBucketName, keyName);

                _logger.LogInformation($"Executed S3Service.Save for image {keyName}");
                return this.GetFullUrl(keyName);
            }
            catch (AmazonS3Exception e)
            {
                _logger.LogError("Error encountered on server. Message:'{0}' when writing an object", e.Message);
                throw new Exception("Fail Upload File", e);
            }
            catch (Exception e)
            {
                _logger.LogError("Unknown encountered on server. Message:'{0}' when writing an object", e.Message);
                throw;
            }
        }

        public async Task Delete(string keyName)
        {
            _logger.LogInformation($"Executing S3Service.Delete for image {keyName}");
            try
            {
                var deleteObjectRequest = new DeleteObjectRequest
                {
                    BucketName = _config.S3.AwsBucketName,
                    Key = keyName
                };

                await _s3Client.DeleteObjectAsync(deleteObjectRequest);
                _logger.LogInformation($"Executed S3Service.Delete for image {keyName}");
            }
            catch (AmazonS3Exception e)
            {
                _logger.LogError("Error encountered on server. Message:'{0}' when deleting an object", e.Message);
                throw new Exception("Fail Delete File", e);
            }
            catch (Exception e)
            {
                _logger.LogError("Unknown encountered on server. Message:'{0}' when deleting an object", e.Message);
                throw;
            }
        }

        public async Task<string> Update(string keyName, string localPath)
        {
            _logger.LogInformation($"Executing S3Service.Update for image {localPath}");
            try
            {
                var putObjectRequest = new PutObjectRequest
                {
                    BucketName = _config.S3.AwsBucketName,
                    Key = keyName,
                    FilePath = localPath
                };

                await _s3Client.PutObjectAsync(putObjectRequest);

                _logger.LogInformation($"Executed S3Service.Update for image {keyName}");
                return this.GetFullUrl(keyName);
            }
            catch (AmazonS3Exception e)
            {
                _logger.LogError("Error encountered on server. Message:'{0}' when deleting an object", e.Message);
                throw new Exception("Fail Update File", e);
            }
            catch (Exception e)
            {
                _logger.LogError("Unknown encountered on server. Message:'{0}' when deleting an object", e.Message);
                throw;
            }
        }
    }
}