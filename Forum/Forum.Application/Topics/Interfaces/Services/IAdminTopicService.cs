using Forum.Application.Topics.Requests;
using Forum.Application.Topics.Responses.Admin;

namespace Forum.Application.Topics.Interfaces.Services
{
    public interface IAdminTopicService
    {
        Task<List<TopicResponseAdminFeedModel>> GetAllTopicsAsync(int page, int itemsPerPage, CancellationToken cancellationToken);
        Task<List<TopicResponseAdminFeedModel>> GetAllArchivedTopicsAsync(int page, int itemsPerPage, CancellationToken cancellationToken);
        Task<List<TopicResponseAdminFeedModel>> GetAllUserTopicsAsync(int userId, int page, int itemsPerPage, CancellationToken cancellationToken);
        Task<TopicResponseAdminModel> GetTopicAsync(int topicId, CancellationToken cancellationToken);
        Task CreateTopicAsync(TopicRequestPostModel postModel, CancellationToken cancellationToken);
        Task UpdateStateAsync(TopicStatePutModel putModel, CancellationToken cancellationToken);
        Task UpdateStatusAsync(TopicStatusPutModel putModel, CancellationToken cancellationToken);
        Task<int> GetTopicsCountAsync(CancellationToken cancellationToken);
        Task<int> GetArchivedTopicsCountAsync(CancellationToken cancellationToken);
        Task<int> GetUserTopicsCountAsync(int userId, CancellationToken cancellationToken);
    }
}
