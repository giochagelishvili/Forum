using Forum.Domain.BaseEntities;
using Forum.Domain.Comments;
using Forum.Domain.Images;
using Forum.Domain.Topics;
using Microsoft.AspNetCore.Identity;

namespace Forum.Domain.Users
{
    public class User : IdentityUser<int>, IEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public bool? Gender { get; set; }
        public string? Bio { get; set; }
        public bool IsBanned { get; set; }
        public DateTime? BannedUntil { get; set; }

        // Navigation properties
        public ICollection<Topic>? Topics { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public Image? Image { get; set; }
    }
}
