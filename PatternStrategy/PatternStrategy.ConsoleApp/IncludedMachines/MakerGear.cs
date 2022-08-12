using System.Drawing;
using PatternStrategy.ConsoleApp.Extensions;
using PatternStrategy.Core;

namespace PatternStrategy.ConsoleApp.IncludedMachines;

public sealed class MakerGear : I3DPrinter
{
    public string Name => "MakerGear M2";

    public string Description => "Suitable for small desktop prints";

    public int Priority => 50;

    public bool CanPrint(PrintablePart part) =>
        part.Envelope.FitsIn(205, 250) && part.MaxHeight <= 205;

    public string Print(PrintablePart part) => 
        "Printed on the MakerGear M2";
}