using PatternStrategy.Core;

namespace PatternStrategy.ConsoleApp.IncludedMachines;

public sealed class PaidProvider : I3DPrinter
{
    public string Name => "Paid Provider";
        
    public string Description => "Submits the file to a paid service";

    public int Priority => 1000;

    public bool CanPrint(PrintablePart part)
        => true;        // paid provider will figure out how to make it happen

    public string Print(PrintablePart part) 
        => "Money evaporated on paid provider";
}