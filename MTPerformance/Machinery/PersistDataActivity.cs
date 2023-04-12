using MassTransit;
using Newtonsoft.Json;

namespace MTPerformance.Machinery;

public class PersistDataActivity : IExecuteActivity<PersistDataActivityArguments>
{
    public async Task<ExecutionResult> Execute(ExecuteContext<PersistDataActivityArguments> context)
    {
        var filename = $@"X:\JSON\{context.Arguments.Zipcode}.json";
        await File.WriteAllTextAsync(filename, JsonConvert.SerializeObject(context.Arguments));

        return context.Completed();
    }
}