using Forum.Application.Topics.Requests;
using Forum.Application.Topics.Responses.Default;

namespace Forum.Application.Topics.Interfaces.Services
{
    public interface ITopicService
    {
        Task<List<TopicResponseNewsFeedModel>> GetAllTopicsAsync(int page, int itemsPerPage, CancellationToken cancellationToken);
        Task<List<TopicResponseNewsFeedModel>> GetAllArchivedTopicsAsync(int page, int itemsPerPage, CancellationToken cancellationToken);
        Task<List<TopicResponseNewsFeedModel>> GetAllUserTopicsAsync(int userId, int page, int itemsPerPage, CancellationToken cancellationToken);
        Task<List<TopicResponseWorkerModel>> GetAllArchiveWorkerTopicsAsync(CancellationToken cancellationToken);
        Task<TopicResponseModel> GetTopicAsync(int topicId, CancellationToken cancellationToken);
        Task CreateTopicAsync(TopicRequestPostModel topic, CancellationToken cancellationToken);
        Task<int> GetTopicsCountAsync(CancellationToken cancellationToken);
        Task<int> GetArchivedTopicsCountAsync(CancellationToken cancellationToken);
        Task<int> GetUserTopicsCountAsync(int userId, CancellationToken cancellationToken);
        Task<bool> ExistsAsync(int topicId, CancellationToken cancellationToken);
        Task<bool> IsActiveAsync(int topicId, CancellationToken cancellationToken);
    }
}
