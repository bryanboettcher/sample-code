using MassTransit;
using MassTransit.Courier.Contracts;
using Microsoft.Extensions.Logging;
using MTPerformance.Activities;
using MTPerformance.Contracts;
using MTPerformance.States;

namespace MTPerformance.Machinery
{
    public class PrepareRoutingConsumer : IConsumer<PrepareRouting>
    {
        private readonly ILogger<PrepareRoutingConsumer> _logger;
        private readonly IEndpointAddressProvider _addressProvider;

        public PrepareRoutingConsumer(ILogger<PrepareRoutingConsumer> logger, IEndpointAddressProvider addressProvider)
        {
            _logger = logger;
            _addressProvider = addressProvider;
        }

        public async Task Consume(ConsumeContext<PrepareRouting> context)
        {
            // categorize the message
            
            // real-world application would be much more involved with
            // checking the "required" fields to see what stage we're
            // at in the global pipeline, as well as what data we already
            // do or don't have

            await context.Execute(BuildRoutingSlip(context));
        }

        private RoutingSlip BuildRoutingSlip(ConsumeContext<PrepareRouting> context)
        {
            var msg = context.Message;
            var builder = new RoutingSlipBuilder(Guid.NewGuid());

            // completely universal activities would go here
            // 
            // builder.AddActivity("ResolveProductId");
            // builder.AddActivity("PersistSvcData");

            _logger.LogInformation(
@"Building routing slip for {correlationId}
    -- ParentId {parentId}
    -- RemainingChildren {remainingChildren}", context.Message.CorrelationId, context.Message.ParentId, context.Message.RemainingChildren);

            builder.AddActivity(
                "SetProductName",
                _addressProvider.GetEndpoint<ResolveProductActivity>(),
                new { msg.ProductName }
            );

            builder.AddActivity(
                "ResolvePrice",
                _addressProvider.GetEndpoint<ResolvePriceActivity>());

            if (msg.ParentId is null)
            {
                // we are a parent, so this is hardcoded to have a reduce step
                // which means we must split the routing slip
                

                // if we're nonzero here, this is a first-pass and needs to be terminated with a "suspend" activity
                if (msg.RemainingChildren > 0)
                {
                    builder.AddSubscription(
                        context.SourceAddress,
                        RoutingSlipEvents.ActivityCompleted,
                        RoutingSlipEventContents.All,
                        "SetProductName",       // this activity name is just the last one we added, it is not *specifically* product name
                        x => x.Send<SuspendItem>(new { context.CorrelationId })
                    );
                }
                else
                {
                    builder.AddActivity(
                        "CalculatePrice",
                        _addressProvider.GetEndpoint<CalculatePriceActivity>());

                    builder.AddSubscription(
                        context.SourceAddress,
                        RoutingSlipEvents.ActivityCompleted,
                        RoutingSlipEventContents.Variables,
                        "CalculatePrice",
                        x => x.Send<CapturePrice>(new { context.CorrelationId }));
                }
            }
            else
            {
                builder.AddActivity(
                    "MapField",
                    _addressProvider.GetEndpoint<MapFieldActivity>(),
                    new
                    {
                        Source = "Price",
                        Destination = "Map"
                    });
                builder.AddSubscription(
                    context.SourceAddress,
                    RoutingSlipEvents.ActivityCompleted,
                    RoutingSlipEventContents.Variables,
                    "MapField",
                    x => x.Send<NestedUpdate>(new
                    {
                        ChildId = context.CorrelationId,
                        CorrelationId = context.Message.ParentId
                    }));
            }

            return builder.Build();
        }
    }
}
