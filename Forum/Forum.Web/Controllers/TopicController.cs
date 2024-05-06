using Forum.Application.Topics.Interfaces;
using Forum.Application.Topics.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Web.Controllers
{
    public class TopicController : Controller
    {
        private readonly ITopicService _topicService;
        private readonly IConfiguration _config;

        public TopicController(ITopicService topicService, IConfiguration config)
        {
            _topicService = topicService;
            _config = config;
        }

        [HttpGet]
        public async Task<IActionResult> Topics(CancellationToken cancellationToken, int page = 1)
        {
            if (User.IsInRole("Admin"))
                return RedirectToAction("Topics", "Topic", new { area = "Admin" });

            var itemsPerPage = _config.GetValue<int>("Constants:ItemsPerPage");

            var topics = await _topicService.GetAllTopicsAsync(page, itemsPerPage, cancellationToken);
            var totalCount = await _topicService.GetTopicsCountAsync(cancellationToken);

            ViewBag.CurrentPage = page;
            ViewBag.TotalCount = totalCount;
            ViewBag.ItemsPerPage = itemsPerPage;

            return View(topics);
        }

        [HttpGet]
        public async Task<IActionResult> Archive(CancellationToken cancellationToken, int page = 1)
        {
            if (User.IsInRole("Admin"))
                return RedirectToAction("Archive", "Topic", new { area = "Admin" });

            var itemsPerPage = _config.GetValue<int>("Constants:ItemsPerPage");

            var topics = await _topicService.GetAllArchivedTopicsAsync(page, itemsPerPage, cancellationToken);
            var totalCount = await _topicService.GetArchivedTopicsCountAsync(cancellationToken);

            ViewBag.CurrentPage = page;
            ViewBag.TotalCount = totalCount;
            ViewBag.ItemsPerPage = itemsPerPage;

            return View(topics);
        }

        [HttpGet]
        public async Task<IActionResult> UserTopics(int userId, CancellationToken cancellationToken, int page = 1)
        {
            if (User.IsInRole("Admin"))
                return RedirectToAction("UserTopics", "Topic", new { area = "Admin" });

            var itemsPerPage = _config.GetValue<int>("Constants:ItemsPerPage");

            var topics = await _topicService.GetAllUserTopicsAsync(userId, page, itemsPerPage, cancellationToken);
            var totalCount = await _topicService.GetUserTopicsCountAsync(userId, cancellationToken);

            ViewBag.CurrentPage = page;
            ViewBag.TotalCount = totalCount;
            ViewBag.ItemsPerPage = itemsPerPage;
            ViewBag.UserId = userId;

            return View(topics);
        }

        [HttpGet]
        public async Task<IActionResult> Topic(int topicId, CancellationToken cancellationToken)
        {
            if (TempData.ContainsKey("Error"))
                ViewBag.Error = TempData["Error"];

            var topic = await _topicService.GetTopicAsync(topicId, cancellationToken);

            return View(topic);
        }
    }
}
