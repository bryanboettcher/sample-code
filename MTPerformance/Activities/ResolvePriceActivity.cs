using MassTransit;
using Microsoft.Extensions.Logging;
using MTPerformance.Contracts;

namespace MTPerformance.Activities
{
    public class ResolvePriceArguments
    {
        public int ProductId { get; set; }
    }

    public class ResolvePriceActivity : IExecuteActivity<ResolvePriceArguments>
    {
        private readonly ILogger<ResolvePriceActivity> _logger;

        public ResolvePriceActivity(ILogger<ResolvePriceActivity> logger)
        {
            _logger = logger;
        }

        public async Task<ExecutionResult> Execute(ExecuteContext<ResolvePriceArguments> context)
        {
            var productId = context.Arguments.ProductId;
            var price = productId switch
            {
                781 => 3.99,
                240 => 0.80
            };

            _logger.LogInformation("Resolving {productId} to {price}", productId, price);

            return context.CompletedWithVariables(new
            {
                Price = price
            });
        }
    }
    public class ResolvePriceActivityDefinition : ExecuteActivityDefinition<ResolvePriceActivity, ResolvePriceArguments>
    {
        public ResolvePriceActivityDefinition(IEndpointAddressProvider address)
        {
            ExecuteEndpointName = address.GetEndpoint<ResolvePriceActivity>().AbsolutePath;
        }
    }
}
