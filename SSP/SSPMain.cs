using ExecutionTimeMeasurement;
using System;

namespace SSP
{
    class SSPMain
    {

        static void Main(string[] args)
        {
            Backtracking();

            //NeighborhoodSearch();

            //TabuSearch();

        }

        private static void Backtracking()
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

        private static void TabuSearch()
        {
            Console.WriteLine(" *** Tabu search solution *** ");
            var tabuCalculator = new SSTabuSearchCalculator();

            Console.WriteLine("\n IA8 set ");
            MeasureResult measureResultforIA8 = Measurement.Exec(tabuCalculator.Calculate, Dataset.IA8);
            Console.WriteLine($"\t{measureResultforIA8.CalculateAverage().TotalMilliseconds} miliseconds");
        }

        private static void NeighborhoodSearch()
        {
            Console.WriteLine(" *** Neighborhood search solution *** ");
            var neighborhoodCalculator = new SSNeighborhoodSearchCalculator();

            Console.WriteLine("\n IA8 set ");
            MeasureResult measureResultforIA8 = Measurement.Exec(neighborhoodCalculator.Calculate, Dataset.IA8);
            Console.WriteLine($"\t{measureResultforIA8.CalculateAverage().TotalMilliseconds} miliseconds");

            //Console.WriteLine("\n IA10 set ");
            //MeasureResult measureResultforIA10 = Measurement.Exec(neighborhoodCalculator.Calculate, Dataset.IA10);
            //Console.WriteLine($"\t{measureResultforIA8.CalculateAverage().TotalMilliseconds} miliseconds");

            //Console.WriteLine("\n IA50 set ");
            //MeasureResult measureResultforIA50 = Measurement.Exec(neighborhoodCalculator.Calculate, Dataset.IA50);
            //Console.WriteLine($"\t{measureResultforIA8.CalculateAverage().TotalMilliseconds} miliseconds");

            //Console.WriteLine("\n IA100 set ");
            //MeasureResult measureResultforIA100 = Measurement.Exec(neighborhoodCalculator.Calculate, Dataset.IA100);
            //Console.WriteLine($"\t{measureResultforIA8.CalculateAverage().TotalMilliseconds} miliseconds");
        }
    }
}
