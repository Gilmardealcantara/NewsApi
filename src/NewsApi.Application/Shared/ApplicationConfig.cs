namespace NewsApi.Application.Shared
{
    public class ApplicationConfig
    {
        public ApplicationConfig()
        {
            Database = new Database();
            Authorization = new Authorization();
        }

        public Database Database { get; set; }
        public Authorization Authorization { get; set; }
        public S3 S3 { get; set; }
    }

    public class Database
    {
        public string ConnectionString { get; set; }
    }

    public class Authorization
    {
        public string SecretKey { get; set; }
    }

    public class S3
    {
        public string AwsAccessKeyId { get; set; }
        public string AwsSecretAccessKey { get; set; }
        public string AwsBucketName { get; set; }
        public string AwsRegionEndpoint { get; set; }
        public string AwsEndpointUrl { get; set; }
    }
}