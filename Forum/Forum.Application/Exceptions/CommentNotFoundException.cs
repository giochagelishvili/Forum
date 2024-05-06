namespace Forum.Application.Exceptions
{
    public class CommentNotFoundException : Exception
    {
        public readonly string Code = "CommentNotFound";

        public CommentNotFoundException(string message = "Comment not found.") : base(message)
        {
        }
    }
}
