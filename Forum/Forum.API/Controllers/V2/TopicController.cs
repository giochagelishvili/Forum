using Asp.Versioning;
using Forum.API.Controllers.V1.Admin;
using Forum.Application.Topics.Interfaces.Services;
using Forum.Application.Topics.Responses.Default;
using Microsoft.AspNetCore.Mvc;

namespace Forum.API.Controllers.V2
{
    [ApiVersion("2.0")]
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

        [HttpGet]
        public async Task<List<TopicResponseNewsFeedModel>> GetAllTopicsAsync(CancellationToken cancellationToken, int page = 1)
        {
            var itemsPerPage = _config.GetValue<int>("Constants:ItemsPerPage");

            return await _topicService.GetAllTopicsAsync(page, itemsPerPage, cancellationToken);
        }
    }
}
