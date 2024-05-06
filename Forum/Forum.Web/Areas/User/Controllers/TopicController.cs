using Forum.Application.Topics.Interfaces;
using Forum.Application.Topics.Interfaces.Services;
using Forum.Application.Topics.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Forum.Web.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles = "Member")]
    public class TopicController : Controller
    {
        private readonly ITopicService _topicService;

        public TopicController(ITopicService topicService)
        {
            _topicService = topicService;
        }

        [HttpGet]
        public async Task<IActionResult> Topic(int topicId, CancellationToken cancellationToken)
        {
            var topic = await _topicService.GetTopicAsync(topicId, cancellationToken);

            return View(topic);
        }

        [HttpGet]
        public IActionResult CreateTopic()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTopic([FromForm] TopicRequestPostModel topic, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return View();

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            topic.AuthorId = userId;

            await _topicService.CreateTopicAsync(topic, cancellationToken);

            return RedirectToAction("Topics", "Topic", new { area = "" });
        }
    }
}
