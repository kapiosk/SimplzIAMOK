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

        public BackgroundPrinter(ILogger<BackgroundPrinter> logger)//IOptions<OrderingBackgroundSettings> settings
        {
            this.logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                Interlocked.Increment(ref number);
                logger.LogInformation($"Worker printing number: {number}");
                Task.Delay(System.TimeSpan.FromSeconds(5), cancellationToken)
                    .Wait(cancellationToken);
            }
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Printing worker stopping");
            return Task.CompletedTask;
        }
    }
}
