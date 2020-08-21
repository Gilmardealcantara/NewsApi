namespace NewsApi.Domain.Shared
{
    public class ErrorMessage
    {
        public ErrorMessage(string code, string message)
        {
            Code = code;
            Message = message;
        }

        public string Code { get; }
        public string Message { get; }
    }
}