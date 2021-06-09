using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace SimplzIAMOK.HostedServices
{
    public class BackgroundPrinter : IHostedService
    {
        private readonly ILogger<BackgroundPrinter> logger;
        private int number = 0;
        private Timer timer;

        public BackgroundPrinter(ILogger<BackgroundPrinter> logger)
        {
            this.logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            timer = new Timer(o =>
            {
                Interlocked.Increment(ref number);
                logger.LogInformation($"Worker printing number: {number}");
            }, null, System.TimeSpan.Zero, System.TimeSpan.FromSeconds(5));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Printing worker stopping");
            timer?.Dispose();
            return Task.CompletedTask;
        }
    }
}
