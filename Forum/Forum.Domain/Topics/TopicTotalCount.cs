namespace Forum.Domain.Topics
{
    public class TopicTotalCount
    {
        public List<TopicCommentsCount> Topics { get; set; } = default!;
        public int TotalCount { get; set; }
    }
}
