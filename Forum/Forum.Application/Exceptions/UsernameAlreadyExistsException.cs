namespace Forum.Application.Exceptions
{
    public class UsernameAlreadyExistsException : Exception
    {
        public readonly string Code = "UsernameAlreadyExists";

        public UsernameAlreadyExistsException(string message = "Username already exists in the database.") : base(message)
        {
        }
    }
}
