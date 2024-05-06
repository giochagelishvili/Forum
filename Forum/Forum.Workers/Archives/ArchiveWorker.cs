using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using NCrontab;

namespace Forum.Workers.Archives
{
    public class ArchiveWorker : BackgroundService
    {
        private readonly ArchiveService _archiveService;
        private readonly CrontabSchedule _schedule;
        private readonly IConfiguration _config;
        private DateTime _nextRun;

        public ArchiveWorker(ArchiveService archiveService, IConfiguration config)
        {
            _archiveService = archiveService;
            _config = config;
            var cronExpression = config.GetValue<string>("Constants:ArchiveWorkerCronExpression");
            _schedule = CrontabSchedule.Parse(cronExpression, new CrontabSchedule.ParseOptions { IncludingSeconds = true });
            _nextRun = _schedule.GetNextOccurrence(DateTime.UtcNow);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var now = DateTime.UtcNow;

                if (now > _nextRun)
                {
                    await CheckTopics(stoppingToken);
                    _nextRun = _schedule.GetNextOccurrence(DateTime.UtcNow);
                }
            }
        }

        private async Task CheckTopics(CancellationToken stoppingToken)
        {
            var topics = await _archiveService.GetTopicWorkerAsync(stoppingToken);

            var daysToMoveToArchive = _config.GetValue<int>("Constants:DaysToMoveToArchive");

            foreach (var topic in topics)
            {
                if (topic.LatestComment == null && topic.ModifiedAt.AddDays(daysToMoveToArchive) <= DateTime.UtcNow || topic.LatestComment?.CreatedAt.AddDays(daysToMoveToArchive) <= DateTime.UtcNow)
                {
                    await _archiveService.UpdateStatusAsync(topic.TopicId, stoppingToken);
                }
            }
        }
    }
}
