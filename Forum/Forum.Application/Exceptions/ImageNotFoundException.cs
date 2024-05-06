namespace Forum.Application.Exceptions
{
    public class ImageNotFoundException : Exception
    {
        public readonly string Code = "ImageNotFound";

        public ImageNotFoundException(string message = "Image not found.") : base(message)
        {
        }
    }
}
