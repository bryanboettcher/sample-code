using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

BenchmarkRunner.Run<FormatTest>();

[MemoryDiagnoser]
public class FormatTest
{
    private readonly ILogger _logger;
    private readonly Random _rand;

    public FormatTest()
    {
        _logger = NullLogger.Instance;
        _rand = new Random();
    }

    [Benchmark()]
    public void TestRuntimeFormat() 
        => _logger.Log(LogLevel.Information, string.Format("The client ID {0} logged a information at {1}.", GetClientId(), GetCurrentTime()));

    [Benchmark()]
    public void TestCompileFormat() 
        => _logger.Log(LogLevel.Information, $"The client ID {GetClientId()} logged a information at {GetCurrentTime()}.");

    private string GetClientId() => _rand.Next(10000).ToString();

    private string GetCurrentTime() => (_rand.Next(1, 5) switch
    {
        1 => "today, early",
        2 => "yesterday, late",
        3 => "tomorrow, maybe?",
        4 => "right now",
        _ => "noooo idea."
    }).ToLowerInvariant();
}