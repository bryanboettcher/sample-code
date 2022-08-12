using System.Drawing;
using PatternStrategy.ConsoleApp.Extensions;
using PatternStrategy.Core;

namespace PatternStrategy.ConsoleApp.IncludedMachines;

public sealed class VoronV0 : I3DPrinter
{
    public string Name => "Voron V0";
        
    public string Description => "Suitable for small parts, fast";
        
    public int Priority => 10;

    public bool CanPrint(PrintablePart part) =>
        part.Envelope.FitsIn(120, 120) && part.MaxHeight <= 120;

    public string Print(PrintablePart part) =>
        "Printed on the Voron V0";
}