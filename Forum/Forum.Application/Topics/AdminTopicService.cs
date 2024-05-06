using Forum.Application.Exceptions;
using Forum.Application.Topics.Interfaces.Interfaces;
using Forum.Application.Topics.Interfaces.Services;
using Forum.Application.Topics.Requests;
using Forum.Application.Topics.Responses.Admin;
using Forum.Application.Users.Interfaces.Services;
using Forum.Domain;
using Forum.Domain.Topics;
using Mapster;

namespace Forum.Application.Topics
{
    public class AdminTopicService : IAdminTopicService
    {
        private readonly IAdminTopicRepository _topicRepository;
        private readonly IAdminUserService _userService;

        public AdminTopicService(IAdminTopicRepository topicRepository, IAdminUserService userService)
        {
            _topicRepository = topicRepository;
            _userService = userService;
        }

        public async Task<List<TopicResponseAdminFeedModel>> GetAllTopicsAsync(int page, int itemsPerPage, CancellationToken cancellationToken)
        {
            if (page <= 0)
                throw new PageNotFoundException();

            var itemsToSkip = (page - 1) * itemsPerPage;

            var result = await _topicRepository.GetAllTopicsAsync(itemsToSkip, itemsPerPage, cancellationToken);

            if (page > 1 && result.Count == 0)
                throw new PageNotFoundException();

            return result.Adapt<List<TopicResponseAdminFeedModel>>();
        }

        public async Task<List<TopicResponseAdminFeedModel>> GetAllArchivedTopicsAsync(int page, int itemsPerPage, CancellationToken cancellationToken)
        {
            if (page <= 0)
                throw new PageNotFoundException();

            var itemsToSkip = (page - 1) * itemsPerPage;

            var result = await _topicRepository.GetAllArchivedTopicsAsync(itemsToSkip, itemsPerPage, cancellationToken);

            if (page > 1 && result.Count == 0)
                throw new PageNotFoundException();

            return result.Adapt<List<TopicResponseAdminFeedModel>>();
        }

        public async Task<List<TopicResponseAdminFeedModel>> GetAllUserTopicsAsync(int userId, int page, int itemsPerPage, CancellationToken cancellationToken)
        {
            if (page <= 0)
                throw new PageNotFoundException();

            if (!await _userService.ExistsAsync(userId.ToString()))
                throw new UserNotFoundException();

            var itemsToSkip = (page - 1) * itemsPerPage;

            var result = await _topicRepository.GetAllUserTopicsAsync(userId, itemsToSkip, itemsPerPage, cancellationToken);

            if (page > 1 && result.Count == 0)
                throw new PageNotFoundException();

            return result.Adapt<List<TopicResponseAdminFeedModel>>();
        }

        public async Task<TopicResponseAdminModel> GetTopicAsync(int topicId, CancellationToken cancellationToken)
        {
            var result = await _topicRepository.GetTopicAsync(topicId, cancellationToken);

            if (result == null)
                throw new TopicNotFoundException();

            return result.Adapt<TopicResponseAdminModel>();
        }

        public async Task CreateTopicAsync(TopicRequestPostModel postModel, CancellationToken cancellationToken)
        {
            var entity = postModel.Adapt<Topic>();

            await _topicRepository.CreateTopicAsync(entity, cancellationToken);
        }

        public async Task UpdateStateAsync(TopicStatePutModel putModel, CancellationToken cancellationToken)
        {
            if (putModel.State != State.Show && putModel.State != State.Hide)
                throw new InvalidStateException();

            if (!await _topicRepository.ExistsAsync(putModel.Id, cancellationToken))
                throw new TopicNotFoundException();

            await _topicRepository.UpdateStateAsync(putModel, cancellationToken);
        }

        public async Task UpdateStatusAsync(TopicStatusPutModel putModel, CancellationToken cancellationToken)
        {
            if (putModel.Status != Status.Active && putModel.Status != Status.Inactive)
                throw new InvalidStatusException();

            if (!await _topicRepository.ExistsAsync(putModel.Id, cancellationToken))
                throw new TopicNotFoundException();

            await _topicRepository.UpdateStatusAsync(putModel, cancellationToken);
        }

        public async Task<int> GetTopicsCountAsync(CancellationToken cancellationToken)
        {
            return await _topicRepository.GetTopicsCountAsync(cancellationToken);
        }

        public async Task<int> GetArchivedTopicsCountAsync(CancellationToken cancellationToken)
        {
            return await _topicRepository.GetArchivedTopicsCountAsync(cancellationToken);
        }

        public async Task<int> GetUserTopicsCountAsync(int userId, CancellationToken cancellationToken)
        {
            if (!await _userService.ExistsAsync(userId.ToString()))
                throw new UserNotFoundException();

            return await _topicRepository.GetUserTopicsCountAsync(userId, cancellationToken);
        }
    }
}
