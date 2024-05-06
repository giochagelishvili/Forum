using Forum.Domain.BaseEntities;
using Forum.Domain.Topics;
using Forum.Domain.Users;

namespace Forum.Domain.Comments
{
    public class Comment : BaseEntity
    {
        public int AuthorId { get; set; }
        public int TopicId { get; set; }
        public string Body { get; set; } = default!;
        public bool IsDeleted { get; set; }

        // Navigation property
        public Topic Topic { get; set; } = default!;
        public User Author { get; set; } = default!;
    }
}
