using MassTransit;
using Microsoft.Extensions.Logging;
using MTPerformance.Contracts;

namespace MTPerformance.Activities
{
    public class MapFieldArguments
    {
        public string Source { get; set; }
        public string Destination { get; set; }
    }

    public class MapFieldActivity : IExecuteActivity<MapFieldArguments>
    {
        private readonly ILogger<MapFieldActivity> _logger;

        public MapFieldActivity(ILogger<MapFieldActivity> logger)
        {
            _logger = logger;
        }
        public async Task<ExecutionResult> Execute(ExecuteContext<MapFieldArguments> context)
        {
            var source = context.Arguments.Source;
            var destination = context.Arguments.Destination;

            _logger.LogInformation("Mapping {source} to {destination}", source, destination);

            var value = context.GetVariable<object>(source);
            var pair = new KeyValuePair<string, object?>(destination, value);

            return context.CompletedWithVariables(new[] { pair }!);
        }
    }

    public class MapFieldActivityDefinition : ExecuteActivityDefinition<MapFieldActivity, MapFieldArguments>
    {
        public MapFieldActivityDefinition(IEndpointAddressProvider address)
        {
            ExecuteEndpointName = address.GetEndpoint<MapFieldActivity>().AbsolutePath;
        }
    }
}
