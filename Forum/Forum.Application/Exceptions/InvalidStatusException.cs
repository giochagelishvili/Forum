namespace Forum.Application.Exceptions
{
    public class InvalidStatusException : Exception
    {
        public readonly string Code = "InvalidStatus";

        public InvalidStatusException(string message = "Such status doesn't exist.") : base(message)
        {
        }
    }
}
