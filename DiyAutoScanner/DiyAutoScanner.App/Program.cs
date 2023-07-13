using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DiyAutoScanner.App;

internal class Program
{
    public static async Task Main(string[] args)
    {
        var builder = Host.CreateDefaultBuilder(args)
            .ConfigureServices(ctx =>
            {
                ctx.ScanAssembly<ScanAnchor>();
                ctx.AddHostedService<TestServiceHost>();
            });

        var app = builder.Build();
        await app.RunAsync();
    }
}