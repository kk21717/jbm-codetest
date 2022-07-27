using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        IOptions<WorkerOptions> _options;

        public Worker(ILogger<Worker> logger,
            IOptions<WorkerOptions> options)
        {
            _logger = logger;
            _options = options;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }

            _logger.LogDebug($"WorkerService is starting.");

            stoppingToken.Register(() =>
                _logger.LogDebug($" WorkerService background task is stopping."));

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogDebug($"WorkerService task doing background work at: {DateTimeOffset.Now}.");

                // do the task that should be done regulalry
                // since we have used rabbitMQ mass transit and
                // we will be notified when a new event is published
                // we do not need to mannually do regular checking 
                // doSomeRegularTask();

                await Task.Delay(_options.Value.RegularActionIntervalMilliSeconds, stoppingToken);
            }

            _logger.LogDebug($"GracePeriod background task is stopping.");
        }
    }
}
