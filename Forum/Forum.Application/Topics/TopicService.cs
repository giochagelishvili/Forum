using Forum.Application.Exceptions;
using Forum.Application.Topics.Interfaces.Interfaces;
using Forum.Application.Topics.Interfaces.Services;
using Forum.Application.Topics.Requests;
using Forum.Application.Topics.Responses.Default;
using Forum.Application.Users.Interfaces.Services;
using Forum.Domain.Topics;
using Mapster;
using Microsoft.Extensions.Configuration;

namespace Forum.Application.Topics
{
    public class TopicService : ITopicService
    {
        private readonly ITopicRepository _topicRepository;
        private readonly IUserService _userService;
        private readonly IConfiguration _config;

        public TopicService(ITopicRepository topicRepository, IUserService userService, IConfiguration config)
        {
            _topicRepository = topicRepository;
            _userService = userService;
            _config = config;
        }

        public async Task<List<TopicResponseNewsFeedModel>> GetAllTopicsAsync(int page, int itemsPerPage, CancellationToken cancellationToken)
        {
            if (page <= 0)
                throw new PageNotFoundException();

            var itemsToSkip = (page - 1) * itemsPerPage;

            var result = await _topicRepository.GetAllTopicsAsync(itemsToSkip, itemsPerPage, cancellationToken);

            if (page > 1 && result.Count == 0)
                throw new PageNotFoundException();

            return result.Adapt<List<TopicResponseNewsFeedModel>>();
        }

        public async Task<List<TopicResponseNewsFeedModel>> GetAllArchivedTopicsAsync(int page, int itemsPerPage, CancellationToken cancellationToken)
        {
            if (page <= 0)
                throw new PageNotFoundException();

            var itemsToSkip = (page - 1) * itemsPerPage;

            var result = await _topicRepository.GetAllArchivedTopicsAsync(itemsToSkip, itemsPerPage, cancellationToken);

            if (page > 1 && result.Count == 0)
                throw new PageNotFoundException();

            return result.Adapt<List<TopicResponseNewsFeedModel>>();
        }

        public async Task<List<TopicResponseNewsFeedModel>> GetAllUserTopicsAsync(int userId, int page, int itemsPerPage, CancellationToken cancellationToken)
        {
            if (page <= 0)
                throw new PageNotFoundException();

            if (!await _userService.ExistsAsync(userId.ToString()))
                throw new UserNotFoundException();

            var itemsToSkip = (page - 1) * itemsPerPage;

            var result = await _topicRepository.GetAllUserTopicsAsync(userId, itemsToSkip, itemsPerPage, cancellationToken);

            if (page > 1 && result.Count == 0)
                throw new PageNotFoundException();

            return result.Adapt<List<TopicResponseNewsFeedModel>>();
        }

        public async Task<List<TopicResponseWorkerModel>> GetAllArchiveWorkerTopicsAsync(CancellationToken cancellationToken)
        {
            var result = await _topicRepository.GetAllArchiveWorkerTopicsAsync(cancellationToken);

            return result.Adapt<List<TopicResponseWorkerModel>>();
        }

        public async Task<TopicResponseModel> GetTopicAsync(int topicId, CancellationToken cancellationToken)
        {
            var result = await _topicRepository.GetTopicAsync(topicId, cancellationToken);

            if (result == null)
                throw new TopicNotFoundException();

            return result.Adapt<TopicResponseModel>();
        }

        public async Task CreateTopicAsync(TopicRequestPostModel topic, CancellationToken cancellationToken)
        {
            var userCommentCount = await _userService.GetUserCommentCountAsync(topic.AuthorId, cancellationToken);
            var minCommentsRequired = int.Parse(_config["Constants:MinimumCommentsRequired"]);

            if (userCommentCount < minCommentsRequired)
                throw new NotEnoughCommentsException();

            var entity = topic.Adapt<Topic>();

            await _topicRepository.CreateAsync(entity, cancellationToken);
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

        public async Task<bool> ExistsAsync(int topicId, CancellationToken cancellationToken)
        {
            return await _topicRepository.ExistsAsync(topicId, cancellationToken);
        }

        public async Task<bool> IsActiveAsync(int topicId, CancellationToken cancellationToken)
        {
            return await _topicRepository.IsActiveAsync(topicId, cancellationToken);
        }
    }
}
