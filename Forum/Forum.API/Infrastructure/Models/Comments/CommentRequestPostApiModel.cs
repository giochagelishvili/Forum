namespace Forum.API.Infrastructure.Models.Comments
{
    public class CommentRequestPostApiModel
    {
        public int TopicId { get; set; }
        public string? Body { get; set; }
    }
}
