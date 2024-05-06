using Forum.Domain.BaseEntities;
using Forum.Domain.Users;

namespace Forum.Domain.Images
{
    public class Image : BaseEntity
    {
        public string Url { get; set; } = default!;
        public int UserId { get; set; }

        // Navigation property
        public User User { get; set; } = default!;
    }
}
