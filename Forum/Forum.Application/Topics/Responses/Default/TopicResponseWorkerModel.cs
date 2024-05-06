using Forum.Application.Comments.Responses;

namespace Forum.Application.Topics.Responses.Default
{
    public class TopicResponseWorkerModel
    {
        public int TopicId { get; set; }
        public DateTime ModifiedAt { get; set; }
        public CommentResponseModel? LatestComment { get; set; }
    }
}
