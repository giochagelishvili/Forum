using Forum.Application.Topics.Interfaces.Services;
using Forum.Application.Topics.Requests;
using Forum.Application.Topics.Responses.Default;
using Forum.Domain;

namespace Forum.Workers.Archives
{
    public class ArchiveService
    {
        private readonly IAdminTopicService _adminTopicService;
        private readonly ITopicService _topicService;

        public ArchiveService(IAdminTopicService adminTopicService, ITopicService topicService)
        {
            _adminTopicService = adminTopicService;
            _topicService = topicService;
        }

        public async Task<List<TopicResponseWorkerModel>> GetTopicWorkerAsync(CancellationToken cancellationToken)
        {
            return await _topicService.GetAllArchiveWorkerTopicsAsync(cancellationToken);
        }

        public async Task UpdateStatusAsync(int topicId, CancellationToken cancellationToken)
        {
            var updateModel = new TopicStatusPutModel
            {
                Id = topicId,
                Status = Status.Inactive
            };

            await _adminTopicService.UpdateStatusAsync(updateModel, cancellationToken);
        }
    }
}
