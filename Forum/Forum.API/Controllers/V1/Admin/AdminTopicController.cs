using Forum.Application.Topics.Interfaces.Services;
using Forum.Application.Topics.Requests;
using Forum.Application.Topics.Responses.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Forum.API.Controllers.V1.Admin
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class AdminTopicController : CustomControllerBase
    {
        private readonly IAdminTopicService _adminTopicService;
        private readonly IConfiguration _config;

        public AdminTopicController(IAdminTopicService adminTopicService, IConfiguration config)
        {
            _adminTopicService = adminTopicService;
            _config = config;
        }

        [HttpGet]
        public async Task<List<TopicResponseAdminFeedModel>> GetAllTopicsAsync(CancellationToken cancellationToken, int page = 1)
        {
            var itemsPerPage = _config.GetValue<int>("Constants:ItemsPerPage");

            return await _adminTopicService.GetAllTopicsAsync(page, itemsPerPage, cancellationToken);
        }

        [HttpGet("archive")]
        public async Task<List<TopicResponseAdminFeedModel>> GetAllArchivedTopicsAsync(CancellationToken cancellationToken, int page = 1)
        {
            var itemsPerPage = _config.GetValue<int>("Constants:ItemsPerPage");

            return await _adminTopicService.GetAllArchivedTopicsAsync(page, itemsPerPage, cancellationToken);
        }

        [HttpGet("userTopics")]
        public async Task<List<TopicResponseAdminFeedModel>> GetAllUserTopicsAsync(int userId, CancellationToken cancellationToken, int page = 1)
        {
            var itemsPerPage = _config.GetValue<int>("Constants:ItemsPerPage");

            return await _adminTopicService.GetAllUserTopicsAsync(userId, page, itemsPerPage, cancellationToken);
        }

        [HttpGet("{topicId}")]
        public async Task<TopicResponseAdminModel> GetTopicAsync(int topicId, CancellationToken cancellationToken)
        {
            return await _adminTopicService.GetTopicAsync(topicId, cancellationToken);
        }

        [HttpPost("create")]
        public async Task CreateTopic(TopicRequestPostModel postModel, CancellationToken cancellationToken)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            postModel.AuthorId = userId;

            await _adminTopicService.CreateTopicAsync(postModel, cancellationToken);
        }

        [HttpPost("updateState")]
        public async Task UpdateStateAsync(TopicStatePutModel putModel, CancellationToken cancellationToken)
        {
            await _adminTopicService.UpdateStateAsync(putModel, cancellationToken);
        }

        [HttpPost("updateStatus")]
        public async Task UpdateStatusAsync(TopicStatusPutModel putModel, CancellationToken cancellationToken)
        {
            await _adminTopicService.UpdateStatusAsync(putModel, cancellationToken);
        }

        [HttpGet("topicsCount")]
        public async Task<int> GetTopicsCountAsync(CancellationToken cancellationToken)
        {
            return await _adminTopicService.GetTopicsCountAsync(cancellationToken);
        }

        [HttpGet("archivedTopicsCount")]
        public async Task<int> GetArchivedTopicsCountAsync(CancellationToken cancellationToken)
        {
            return await _adminTopicService.GetArchivedTopicsCountAsync(cancellationToken);
        }

        [HttpGet("userTopicsCount")]
        public async Task<int> GetUserTopicsCountAsync(int userId, CancellationToken cancellationToken)
        {
            return await _adminTopicService.GetUserTopicsCountAsync(userId, cancellationToken);
        }
    }
}
