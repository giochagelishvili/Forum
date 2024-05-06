using Forum.Application.Topics.Requests;
using Forum.Domain.Topics;

namespace Forum.Application.Topics.Interfaces.Interfaces
{
    public interface IAdminTopicRepository
    {
        Task<List<TopicCommentsCount>> GetAllTopicsAsync(int itemsToSkip, int itemsToTake, CancellationToken cancellationToken);
        Task<List<TopicCommentsCount>> GetAllArchivedTopicsAsync(int itemsToSkip, int itemsToTake, CancellationToken cancellationToken);
        Task<List<TopicCommentsCount>> GetAllUserTopicsAsync(int userId, int itemsToSkip, int itemsToTake, CancellationToken cancellationToken);
        Task<Topic?> GetTopicAsync(int topicId, CancellationToken cancellationToken);
        Task CreateTopicAsync(Topic topic, CancellationToken cancellationToken);
        Task UpdateStateAsync(TopicStatePutModel putModel, CancellationToken cancellationToken);
        Task UpdateStatusAsync(TopicStatusPutModel putModel, CancellationToken cancellationToken);
        Task<bool> ExistsAsync(int topicId, CancellationToken cancellationToken);
        Task<int> GetTopicsCountAsync(CancellationToken cancellationToken);
        Task<int> GetArchivedTopicsCountAsync(CancellationToken cancellationToken);
        Task<int> GetUserTopicsCountAsync(int userId, CancellationToken cancellationToken);
    }
}
