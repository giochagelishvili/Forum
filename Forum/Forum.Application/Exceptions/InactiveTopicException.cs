namespace Forum.Application.Exceptions
{
    public class InactiveTopicException : Exception
    {
        public readonly string Code = "InactiveTopic";

        public InactiveTopicException(string message = "This action is forbidden on inactive topic.") : base(message)
        {
        }
    }
}
