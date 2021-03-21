using ExecutionTimeMeasurement;
using System;

namespace SSP
{
    class SSPMain
    {

        static void Main(string[] args)
        {
            Console.WriteLine(" *** Backtracking solution *** ");
            var backtrackingCalculator = new SSBacktrackingCalculator();

            Console.WriteLine("\n IA8 set ");
            MeasureResult measureResultforIA8 = Measurement.Exec(backtrackingCalculator.Calculate, Dataset.IA8);
            Console.WriteLine($"\t{measureResultforIA8.CalculateAverage().TotalMilliseconds} miliseconds");

            Console.WriteLine("\n IA10 set ");
            MeasureResult measureResultforIA10 = Measurement.Exec(backtrackingCalculator.Calculate, Dataset.IA10);
            Console.WriteLine($"\t{measureResultforIA10.CalculateAverage().TotalMilliseconds} miliseconds");
        }
    }
}
