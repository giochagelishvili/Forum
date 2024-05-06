using Forum.Domain;

namespace Forum.Application.Topics.Requests
{
    public class TopicStatePutModel
    {
        public int Id { get; set; }
        public State State { get; set; } = default!;
    }
}
