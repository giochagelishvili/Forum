using Forum.Domain.Topics;

namespace Forum.Application.Topics.Interfaces.Interfaces
{
    public interface ITopicRepository
    {
        Task<List<TopicCommentsCount>> GetAllTopicsAsync(int itemsToSkip, int itemsToTake, CancellationToken cancellationToken);
        Task<List<TopicCommentsCount>> GetAllArchivedTopicsAsync(int itemsToSkip, int itemsToTake, CancellationToken cancellationToken);        
        Task<List<TopicCommentsCount>> GetAllUserTopicsAsync(int userId, int itemsToSkip, int itemsToTake, CancellationToken cancellationToken);
        Task<List<TopicWithLatestComment>> GetAllArchiveWorkerTopicsAsync(CancellationToken cancellationToken);
        Task<Topic?> GetTopicAsync(int topicId, CancellationToken cancellationToken);
        Task CreateAsync(Topic topic, CancellationToken cancellationToken);
        Task<int> GetTopicsCountAsync(CancellationToken cancellationToken);
        Task<int> GetArchivedTopicsCountAsync(CancellationToken cancellationToken);
        Task<int> GetUserTopicsCountAsync(int userId, CancellationToken cancellationToken);
        Task<bool> ExistsAsync(int topicId, CancellationToken cancellationToken);
        Task<bool> IsActiveAsync(int topicId, CancellationToken cancellationToken);
    }
}
