using MassTransit;
using Microsoft.Extensions.Logging;
using MTPerformance.States;

namespace MTPerformance.Machinery
{
    public class IngestFileRowMessage
    {
        public string Data { get; set; }
    }

    public class IngestFileRowConsumer : IConsumer<IngestFileRowMessage>
    {
        private readonly ILogger<IngestFileRowMessage> _logger;

        public IngestFileRowConsumer(ILogger<IngestFileRowMessage> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<IngestFileRowMessage> context)
        {
            var fields = context.Message.Data.Split(",");

            int recordId = Convert.ToInt32(fields[0]);
            int? parentId = !string.IsNullOrEmpty(fields[1]) ? Convert.ToInt32(fields[1]) : null;
            int? totalChildren = !string.IsNullOrEmpty(fields[2]) ? Convert.ToInt32(fields[2]) : null;
            var productName = fields[3];

            _logger.LogInformation("Publishing message for {productName}", productName);

            await context.Publish<ItemReady>(new
            {
                CorrelationId = Guid.NewGuid(),
                RecordId = recordId,
                ParentId = parentId,
                TotalChildren = totalChildren,
                ProductName = productName,
            }, context.CancellationToken);
        }
    }
}
