using System.Drawing;
using Lamar;
using PatternStrategy.ConsoleApp.Extensions;
using PatternStrategy.ConsoleApp.IncludedMachines;
using PatternStrategy.Core;

namespace PatternStrategy.ConsoleApp;

internal class Program
{
    static void Main(string[] args)
    {
        using var container = new Container(conf =>
        {
            conf.Scan(a =>
            {
                a.AssembliesFromApplicationBaseDirectory();
                a.RegisterConcreteTypesAgainstTheFirstInterface();
            });
        });

        var knownPrinters = container
            .GetAllInstances<I3DPrinter>()
            .OrderBy(p => p.Priority)
            .ToList();

        Console.Clear();
        Console.WriteLine("The following printers are loaded and ready for jobs:");
        foreach (var printer in knownPrinters)
        {
            Console.WriteLine($"  - {printer.Name}, {printer.Description}   (priority: {printer.Priority})");
        }

        var jobs = CreateBusywork().ToList();
        Console.WriteLine();
        Console.WriteLine("The following jobs are ready to be printed:");
        foreach(var job in jobs)
            Console.WriteLine($"  {job.Filename}\t{job.Envelope.Width} x {job.Envelope.Height} x {job.MaxHeight}");

        Console.WriteLine();
        Console.WriteLine("Checking the backlog of work against the machines");
        var results = PrintAllJobs(jobs, knownPrinters)
            .GroupBy(k => k.Item1.Id)
            .ToDictionary(j => j.Key, g => g.First());

        results.Expect<MakerGear>(1, true);
        results.Expect<MakerGear>(2, true);
        results.Expect<VoronV0>(3, true);
        results.Expect<PaidProvider>(4, true);
        results.Expect<PaidProvider>(5, false);
        results.Expect<VoronV0>(6, true);
    }

    private static IEnumerable<(PrintablePart, I3DPrinter)> PrintAllJobs(IEnumerable<PrintablePart> jobs, List<I3DPrinter> knownPrinters) 
        =>  from job in jobs 
            from printer in knownPrinters 
            where printer.CanPrint(job) 
            select (job, printer);

    private static IEnumerable<PrintablePart> CreateBusywork()
    {
        yield return new PrintablePart(1, new Size(35, 160), 101,     "air_magazine.stl");
        yield return new PrintablePart(2, new Size(140, 220), 12,     "custom_wrench.stl");
        yield return new PrintablePart(3, new Size(50, 25), 30,       "little_boaty.stl");
        yield return new PrintablePart(4, new Size(1180, 2635), 3180, "actual_boaty.stl");
        yield return new PrintablePart(5, new Size(220, 240), 101,    "drying_rack.stl");
        yield return new PrintablePart(6, new Size(115, 115), 101,    "corner_bumper.stl");
    }
}