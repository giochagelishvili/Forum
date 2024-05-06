using Forum.Application.Users.Responses;
using Forum.Domain;

namespace Forum.Application.Topics.Responses.Default
{
    public class TopicResponseNewsFeedModel
    {
        public int TopicId { get; set; }
        public string TopicTitle { get; set; } = default!;
        public string TopicDescription { get; set; } = default!;
        public int CommentCount { get; set; }
        public Status TopicStatus { get; set; }
        public UserResponseModel TopicAuthor { get; set; } = default!;
    }
}
