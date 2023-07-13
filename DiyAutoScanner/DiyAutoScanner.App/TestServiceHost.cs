using DiyAutoScanner.Library;
using Microsoft.Extensions.Hosting;

namespace DiyAutoScanner.App;

public class TestServiceHost : BackgroundService
{
    public TestServiceHost(IWorker worker, ISlacker slacker, IEnumerable<IWorker> workers/*, IValidator<int> validator*/)
    {
        Console.WriteLine("Service constructed");
        Console.WriteLine();
        Console.WriteLine($"The default IWorker is {worker.GetType()}");
        Console.WriteLine($"The default ISlacker is {slacker.GetType()}");
        Console.WriteLine($"All workers are {string.Join(',', workers.Select(w => w.GetType()))}");
        //Console.WriteLine($"The validator is {validator.ToString()}");
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("Running the service, but this isn't important");
        return Task.CompletedTask;
    }
}