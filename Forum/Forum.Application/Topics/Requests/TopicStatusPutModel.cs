using Forum.Domain;

namespace Forum.Application.Topics.Requests
{
    public class TopicStatusPutModel
    {
        public int Id { get; set; }
        public Status Status { get; set; }
    }
}
