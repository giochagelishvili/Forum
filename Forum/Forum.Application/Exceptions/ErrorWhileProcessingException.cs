namespace Forum.Application.Exceptions
{
    public class ErrorWhileProcessingException : Exception
    {
        public readonly string Code = "ErrorWhileProcessing";
        public ErrorWhileProcessingException(string message = "An error occurred while trying to process this request.") : base(message)
        {
        }
    }
}
