using MassTransit;
using Microsoft.Extensions.Logging;
using MTPerformance.Contracts;

namespace MTPerformance.Activities
{
    public class ResolveProductArguments
    {
        public string? ProductName { get; set; }
    }

    public class ResolveProductActivity : IExecuteActivity<ResolveProductArguments>
    {
        private readonly ILogger<ResolveProductActivity> _logger;

        public ResolveProductActivity(ILogger<ResolveProductActivity> logger)
        {
            _logger = logger;
        }
        public async Task<ExecutionResult> Execute(ExecuteContext<ResolveProductArguments> context)
        {
            var productName = context.Arguments.ProductName;
            var productId = productName switch
            {
                "Conference" => 781,
                "Connection" => 240,
                _ => 999
            };

            _logger.LogInformation("Resolving {productName} to ID {productId}", productName, productId);

            return context.CompletedWithVariables(new
            {
                ProductId = productId
            });
        }
    }

    public class ResolveProductActivityDefinition : ExecuteActivityDefinition<ResolveProductActivity, ResolveProductArguments>
    {
        public ResolveProductActivityDefinition(IEndpointAddressProvider address)
        {
            ExecuteEndpointName = address.GetEndpoint<ResolveProductActivity>().AbsolutePath;
        }
    }
}
