namespace Forum.Domain.Topics
{
    public class TopicCommentsCount
    {
        public Topic Topic { get; set; } = default!;
        public int CommentCount { get; set; }
    }
}
