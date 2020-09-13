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
    }

    public class Database
    {
        public string ConnectionString { get; set; }
    }

    public class Authorization
    {
        public string SecretKey { get; set; }
    }
}