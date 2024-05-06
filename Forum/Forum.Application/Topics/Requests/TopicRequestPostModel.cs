namespace Forum.Application.Topics.Requests
{
    public class TopicRequestPostModel
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int AuthorId { get; set; }
    }
}