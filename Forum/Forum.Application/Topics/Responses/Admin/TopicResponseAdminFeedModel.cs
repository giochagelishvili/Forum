using Forum.Application.Users.Responses;
using Forum.Domain;

namespace Forum.Application.Topics.Responses.Admin
{
    public class TopicResponseAdminFeedModel
    {
        public int TopicId { get; set; }
        public string TopicTitle { get; set; } = default!;
        public string TopicDescription { get; set; } = default!;
        public int CommentCount { get; set; }
        public Status TopicStatus { get; set; }
        public State TopicState { get; set; }
        public DateTime TopicCreatedAt { get; set; }
        public UserResponseModel TopicAuthor { get; set; } = default!;
    }
}
