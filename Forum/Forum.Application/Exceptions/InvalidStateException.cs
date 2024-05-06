namespace Forum.Application.Exceptions
{
    public class InvalidStateException : Exception
    {
        public readonly string Code = "InvalidState";

        public InvalidStateException(string message = "Such state doesn't exist.") : base(message)
        {
        }
    }
}
