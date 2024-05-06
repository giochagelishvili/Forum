namespace Forum.Application.Users.Requests.Updates
{
    public class UserRequestPutModel
    {
        public string? Email { get; set; }
        public string? UpdatedUsername { get; set; }
        public bool? Gender { get; set; }
        public string? Bio { get; set; }
    }
}
