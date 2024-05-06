namespace Forum.Application.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public readonly string Code = "Unauthorized";

        public UnauthorizedException(string message = "Current user is unauthorized for this action.") : base(message)
        {
        }
    }
}
