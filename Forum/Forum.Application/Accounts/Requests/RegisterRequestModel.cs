namespace Forum.Application.Accounts.Requests
{
    public class RegisterRequestModel
    {
        public string? Email { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public bool? Gender { get; set; }
    }
}