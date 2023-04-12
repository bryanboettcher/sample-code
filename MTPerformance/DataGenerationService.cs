using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MTPerformance.Machinery;

namespace MTPerformance
{
    public class DataGenerationService : BackgroundService
    {
        private readonly ILogger<DataGenerationService> _logger;
        private readonly IPublishEndpoint _publishEndpoint;
        
        public DataGenerationService(ILogger<DataGenerationService> logger,
            IPublishEndpoint publishEndpoint)
        {
            _logger = logger;
            _publishEndpoint = publishEndpoint;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Waiting 5 seconds for everything to start up...");
            await Task.Delay(TimeSpan.FromSeconds(3), stoppingToken);

            _logger.LogInformation("Emitting parent record");
            await _publishEndpoint.Publish(new IngestFileRowMessage { Data = GetParent() }, stoppingToken);
            await Task.Delay(TimeSpan.FromMilliseconds(250), stoppingToken);

            _logger.LogInformation("Emitting child records");
            await _publishEndpoint.Publish(new IngestFileRowMessage { Data = GetChild1() }, stoppingToken);
            await Task.Delay(TimeSpan.FromMilliseconds(250), stoppingToken);

            await _publishEndpoint.Publish(new IngestFileRowMessage { Data = GetChild2() }, stoppingToken);
            await Task.Delay(TimeSpan.FromMilliseconds(250), stoppingToken);

            await _publishEndpoint.Publish(new IngestFileRowMessage { Data = GetChild3() }, stoppingToken);
            await Task.Delay(TimeSpan.FromMilliseconds(250), stoppingToken);

            _logger.LogInformation("Done with child records");

            await Task.Delay(-1, stoppingToken);
        }

        // recordId, parentId?, childCount?, productName
        private static string GetParent() => "1,,3,Conference";
        private static string GetChild1() => "2,1,,Connection";
        private static string GetChild2() => "3,1,,Connection";
        private static string GetChild3() => "4,1,,Connection";
    }
}