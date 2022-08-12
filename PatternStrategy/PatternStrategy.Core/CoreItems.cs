using System.Drawing;

namespace PatternStrategy.Core
{
    /// <summary>
    /// Defines the features and behaviors of a 3d printer, used for illustration
    /// purposes of exploring a dynamically-loaded strategy pattern.
    /// </summary>
    public interface I3DPrinter
    {
        /// <summary>
        /// A human-readable name of this 3D printer.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// A human-readable description of the 3D printer.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// The preferential order of the printers, since some are cheaper to run than others.
        /// Lower numbers will be used earlier.
        /// </summary>
        public int Priority { get; }

        /// <summary>
        /// Used to determine if a given fake part should be printed on this machine.
        /// </summary>
        /// <returns><c>true</c> if this particular printer should print this part, <c>false</c> otherwise.</returns>
        public bool CanPrint(PrintablePart part);

        /// <summary>
        /// A more-or-less fake method to signify that the printer has been told to print this part.  There isn't much
        /// to do here.
        /// </summary>
        /// <param name="part"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public string Print(PrintablePart part);
    }

    /// <summary>
    /// Holds metadata about a printable part.
    /// </summary>
    /// <param name="Id">A unique identifier for this part.</param>
    /// <param name="Envelope">The maximum size of the part on the print bed.</param>
    /// <param name="MaxHeight">The tallest measurement on the part.</param>
    /// <param name="Filename">The filename to load the rest of the data from.</param>
    public sealed record PrintablePart(int Id, Size Envelope, float MaxHeight, string Filename);
}