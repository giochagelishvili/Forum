namespace Forum.Application.Comments.Requests
{
    public class CommentRequestPostModel
    {
        public int AuthorId { get; set; }
        public int TopicId { get; set; }
        public string Body { get; set; } = default!;
    }
}
