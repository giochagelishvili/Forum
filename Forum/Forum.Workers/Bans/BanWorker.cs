using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NCrontab;

namespace Forum.Workers.Bans
{
    public class BanWorker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly CrontabSchedule _schedule;
        private DateTime _nextRun;

        public BanWorker(IServiceProvider serviceProvider, IConfiguration config)
        {
            var cronExpression = config.GetValue<string>("Constants:BanWorkerCronExpression");

            _serviceProvider = serviceProvider;
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
                    await CheckBannedUsers(stoppingToken);
                    _nextRun = _schedule.GetNextOccurrence(DateTime.UtcNow);
                }
            }
        }

        private async Task CheckBannedUsers(CancellationToken stoppingToken)
        {
            using var service = _serviceProvider.CreateAsyncScope();

            BanService banService = service.ServiceProvider.GetService<BanService>();

            var bannedUsers = await banService.GetBannedUsersAsync(stoppingToken);

            foreach (var user in bannedUsers)
                await banService.UnbanUser(user.Id.ToString());
        }
    }
}