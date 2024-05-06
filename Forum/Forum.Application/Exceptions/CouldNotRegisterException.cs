namespace Forum.Application.Exceptions
{
    public class CouldNotRegisterException : Exception
    {
        public readonly string Code = "CouldNotRegister";

        public CouldNotRegisterException(string message = "An error occured while registering.") : base(message)
        {
        }
    }
}
