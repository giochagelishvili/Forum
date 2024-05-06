namespace Forum.Application.Users.Responses
{
    public class UserResponseAdminModel
    {
        public int Id { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public bool? Gender { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsBanned { get; set; }
        public DateTime BannedUntil { get; set; }
        public string ImageUrl { get; set; } = default!;
    }
}
