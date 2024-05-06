using Asp.Versioning;
using Forum.API.Controllers.V1.Admin;
using Forum.Application.Topics.Interfaces.Services;
using Forum.Application.Topics.Requests;
using Forum.Application.Topics.Responses.Default;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Forum.API.Controllers.V1
{
    [Authorize(Roles = "Member")]
    [ApiController]
    public class TopicController : CustomControllerBase
    {
        private readonly ITopicService _topicService;
        private readonly IConfiguration _config;

        public TopicController(ITopicService topicService, IConfiguration config)
        {
            _topicService = topicService;
            _config = config;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<List<TopicResponseNewsFeedModel>> GetAllTopicsAsync(CancellationToken cancellationToken, int page = 1)
        {
            var itemsPerPage = _config.GetValue<int>("Constants:ItemsPerPage");

            return await _topicService.GetAllTopicsAsync(page, itemsPerPage, cancellationToken);
        }

        [AllowAnonymous]
        [HttpGet("archive")]
        public async Task<List<TopicResponseNewsFeedModel>> GetAllArchivedTopicsAsync(CancellationToken cancellationToken, int page = 1)
        {
            var itemsPerPage = _config.GetValue<int>("Constants:ItemsPerPage");

            return await _topicService.GetAllArchivedTopicsAsync(page, itemsPerPage, cancellationToken);
        }

        [AllowAnonymous]
        [HttpGet("userTopics/{userId}")]
        public async Task<List<TopicResponseNewsFeedModel>> GetAllUserTopicsAsync(int userId, CancellationToken cancellationToken, int page = 1)
        {
            var itemsPerPage = _config.GetValue<int>("Constants:ItemsPerPage");

            return await _topicService.GetAllUserTopicsAsync(userId, page, itemsPerPage, cancellationToken);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<TopicResponseModel> GetTopicAsync(int topicId, CancellationToken cancellationToken)
        {
            return await _topicService.GetTopicAsync(topicId, cancellationToken);
        }

        [HttpPost]
        public async Task CreateTopicAsync(TopicRequestPostModel postModel, CancellationToken cancellationToken)
        {
            await _topicService.CreateTopicAsync(postModel, cancellationToken);
        }

        [HttpGet("topicsCount")]
        public async Task<int> GetTopicsCountAsync(CancellationToken cancellationToken)
        {
            return await _topicService.GetTopicsCountAsync(cancellationToken);
        }

        [HttpGet("archivedTopicsCount")]
        public async Task<int> GetArchivedTopicsCountAsync(CancellationToken cancellationToken)
        {
            return await _topicService.GetArchivedTopicsCountAsync(cancellationToken);
        }

        [HttpGet("userTopicsCount")]
        public async Task<int> GetUserTopicsCountAsync(int userId, CancellationToken cancellationToken)
        {
            return await _topicService.GetUserTopicsCountAsync(userId, cancellationToken);
        }
    }
}
