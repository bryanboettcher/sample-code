using MassTransit;
using Microsoft.Extensions.Logging;
using MTPerformance.Contracts;

namespace MTPerformance.Activities
{
    public class CalculatePriceArguments
    {
        public decimal Price { get; set; }
    }

    public class CalculatePriceActivity : IExecuteActivity<CalculatePriceArguments>
    {
        private readonly ILogger<CalculatePriceActivity> _logger;

        public CalculatePriceActivity(ILogger<CalculatePriceActivity> logger)
        {
            _logger = logger;
        }

        public async Task<ExecutionResult> Execute(ExecuteContext<CalculatePriceArguments> context)
        {
            var price = context.Arguments.Price;

            _logger.LogInformation("Calculating final price based on 5% tax rate for initial price of {price}");

            return context.CompletedWithVariables(new
            {
                FinalPrice = price * 1.05m
            });
        }
    }

    public class CalculatePriceActivityDefinition : ExecuteActivityDefinition<CalculatePriceActivity, CalculatePriceArguments>
    {
        public CalculatePriceActivityDefinition(IEndpointAddressProvider address)
        {
            ExecuteEndpointName = address.GetEndpoint<CalculatePriceActivity>().AbsolutePath;
        }
    }
}
