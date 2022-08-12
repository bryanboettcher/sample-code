using PatternStrategy.Core;

namespace PatternStrategy.ConsoleApp.Extensions
{
    public static class DictionaryExtensions
    {
        public static void Expect<T>(this Dictionary<int, (PrintablePart, I3DPrinter)> input, int id, bool wasPrinted) where T : I3DPrinter
        {
            var outputTuple = input[id];
            var shouldPrint = wasPrinted ? "was" : "was NOT";
            var passed = wasPrinted ^ outputTuple.Item2 is T ? "FAIL" : "PASS";

            Console.WriteLine($"  [{passed}] {outputTuple.Item1.Filename} {shouldPrint} supposed to be printed with the {outputTuple.Item2.Name}");
        }
    }
}