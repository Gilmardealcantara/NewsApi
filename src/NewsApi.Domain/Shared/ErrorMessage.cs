namespace NewsApi.Domain.Shared
{
    public class ErrorMessage
    {
        public ErrorMessage(string code, string message)
        {
            Code = code;
            Message = message;
        }

        string Code { get; }
        string Message { get; }
    }
}