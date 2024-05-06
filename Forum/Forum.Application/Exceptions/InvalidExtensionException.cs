namespace Forum.Application.Exceptions
{
    public class InvalidExtensionException : Exception
    {
        public readonly string Code = "InvalidFileExtension";

        public InvalidExtensionException(string message = "Invalid file extension.") : base(message)
        {
        }
    }
}
