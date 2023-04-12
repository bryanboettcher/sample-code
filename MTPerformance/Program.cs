using System.Reflection;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MTPerformance;
using MTPerformance.Contracts;
using StackExchange.Redis;

public class Program
{
    public const bool UseInMemory = true;

    public static async Task Main(string[] args) =>
        await CreateHostBuilder(args).Build().RunAsync();

    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostBuilderContext, services) =>
            {
                services.AddOptions<MassTransitHostOptions>()
                    .Configure(options =>
                {
                    options.WaitUntilStarted = true;
                    options.StartTimeout = TimeSpan.FromMinutes(1);
                    options.StopTimeout = TimeSpan.FromMinutes(1);
                });
                services.AddOptions<HostOptions>()
                .Configure(options =>
                {
                    options.ShutdownTimeout = TimeSpan.FromMinutes(2);
                });
                services.AddMassTransit(conf =>
                {
                    conf.SetKebabCaseEndpointNameFormatter();
                    
                    var assembly = Assembly.GetExecutingAssembly();

                    conf.AddConfigureEndpointsCallback(ConfigureEndpointDetails);
                    conf.AddConsumers(assembly);
                    conf.AddActivities(assembly);
                    conf.AddSagas(assembly);
                    conf.AddSagaStateMachines(assembly);

                    if (UseInMemory)
                    {
                        services.AddSingleton(typeof(IEndpointAddressProvider), typeof(MemoryEndpointAddressProvider));

                        conf.SetInMemorySagaRepositoryProvider();
                        conf.UsingInMemory((ctx, cfg) =>
                        {
                            cfg.ConfigureEndpoints(ctx);
                        });
                    }
                    else
                    {
                        services.AddSingleton(typeof(IEndpointAddressProvider), typeof(RabbitMqEndpointAddressProvider));

                        conf.SetRedisSagaRepositoryProvider(cfg =>
                        {
                            cfg.DatabaseConfiguration("localhost");
                            cfg.ConcurrencyMode = ConcurrencyMode.Optimistic;
                            cfg.KeyPrefix = "dev-";
                            cfg.LockTimeout = TimeSpan.FromSeconds(8);
                        });
                        conf.UsingRabbitMq((ctx, cfg) =>
                        {
                            cfg.Host("localhost", "/", h =>
                            {
                                h.Username("guest");
                                h.Password("guest");
                            });

                            cfg.UseNewtonsoftJsonSerializer();

                            // always needs to be the last thing called
                            cfg.ConfigureEndpoints(ctx);
                        });
                    }
                });
                services.AddHostedService<DataGenerationService>();
            });

    private static void ConfigureEndpointDetails(string queuename, IReceiveEndpointConfigurator configurator)
    {
        configurator.ConcurrentMessageLimit = 32;
        configurator.PrefetchCount = 16;
    }
}