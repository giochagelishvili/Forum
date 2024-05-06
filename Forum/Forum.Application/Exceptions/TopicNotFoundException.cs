namespace Forum.Application.Exceptions
{
    public class TopicNotFoundException : Exception
    {
        public readonly string Code = "TopicNotFound";

        public TopicNotFoundException(string message = "Topic not found.") : base(message)
        {
        }
    }
}
