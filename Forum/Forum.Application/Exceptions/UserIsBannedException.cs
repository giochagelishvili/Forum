namespace Forum.Application.Exceptions
{
    public class UserIsBannedException : Exception
    {
        public readonly string Code = "UserIsBanned";

        public UserIsBannedException(string message = "This user is banned.") : base(message)
        {
        }
    }
}
