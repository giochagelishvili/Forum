namespace Forum.Application.Exceptions
{
    public class NotEnoughCommentsException : Exception
    {
        public readonly string Code = "NotEnoughComments";

        public NotEnoughCommentsException(string message = "The user must have at least 3 comments in order to create a topic.") : base(message)
        {
        }
    }
}
