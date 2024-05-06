namespace Forum.Application.Exceptions
{
    public class PageNotFoundException : Exception
    {
        public readonly string Code = "PageNotFound";

        public PageNotFoundException(string message = "Page not found.") : base(message)
        {
        }
    }
}
