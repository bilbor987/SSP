using ExecutionTimeMeasurement;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SSP
{
    //1. Generate a random solution for the problem and call it S
    //2. Compute the neighborhood of S and choose S' as the best solution in the neighborhood
    //3. If S' is better than S then go to step 4, else go to step 6
    //4. S = S'
    //5. Go to step 2
    //6. Return S as the best solution encountered

    public class SSNeighborhoodSearchCalculator : ISSCalculator
    {
        [Measure(nameof(Calculate), "Calculate method", 1)]
        public void Calculate(List<int> dataset)
        {
            int sizeOfTheSolution = new Random().Next(2, dataset.Count - 1);
            var startingSolution = GenerateRandomSolution(dataset, sizeOfTheSolution);
            int startingSum = startingSolution.Sum();
            if (startingSum == 0)
            {
                Console.WriteLine("Initial random solution was luckily the winner: ");
                PrintIntegerList(startingSolution);
                return;
            }
            //Console.WriteLine("Initial random solution: ");
            //PrintIntegerList(startingSolution);
            Search(dataset, startingSolution, startingSum);
        }

        private void Search(List<int> dataset, List<int> startingSolution, int startingSum)
        {
            var neighborhoods = GenerateNeighborhoods(dataset, startingSolution);
            //Console.WriteLine("\nNeighborhoods: ");
            //PrintIntegerListOfLists(neighborhoods);

            List<int> bestSolution = GetBestSolutionFromNeighborhood(neighborhoods);
            int bestSum = bestSolution.Sum();
            //Console.WriteLine("\nBest solution: ");
            //PrintSolution(bestSolution, bestSum);

            if (Math.Abs(bestSum) < Math.Abs(startingSum))
            {
                //Console.WriteLine("Current sum is better than the starting sum. \nGoing further.\n");
                Search(dataset, bestSolution, bestSum);
            }
            else
            {
                PrintSolution(bestSolution, bestSum);
            }
        }

        private static List<List<int>> GenerateNeighborhoods(List<int> dataset, List<int> solutionSoFar)
        {
            var neighborhoods = new List<List<int>>();
            // get the elements from the dataset that are not in the solution
            var intersection = dataset.Except(solutionSoFar).ToList();

            //Console.WriteLine("\nIntersection: ");
            //PrintIntegerList(intersection);

            List<int> neighborhood = new List<int>(solutionSoFar);
            // generate neighborhoods by adding one element more
            foreach (var item in intersection)
            {
                var tempList = new List<int>() { item };

                neighborhood.Add(item);
                neighborhoods.Add(neighborhood);
                neighborhood = new List<int>(solutionSoFar);

                intersection = intersection.Except(tempList).ToList();
            }

            neighborhood = new List<int>(solutionSoFar);
            //generate neighborhoods by substracting one element
            foreach (var item in solutionSoFar)
            {
                neighborhood.Remove(item);
                neighborhoods.Add(neighborhood);
                neighborhood = new List<int>(solutionSoFar);
            }

            return neighborhoods;
        }

        private List<int> GetBestSolutionFromNeighborhood(List<List<int>> neighborhoods)
        {
            var bestSolution = new List<int>();
            if (neighborhoods != null && neighborhoods.Any())
            {
                bestSolution = neighborhoods[0];
                var bestSum = Math.Abs(bestSolution.Sum());
                foreach (var currentNeighborhood in neighborhoods)
                {
                    var currentSum = Math.Abs(currentNeighborhood.Sum());
                    if (currentSum < bestSum)
                    {
                        bestSolution = currentNeighborhood;
                        bestSum = currentSum;
                    }
                }
            }

            return bestSolution;
        }

        private static List<int> GenerateRandomSolution(List<int> source, int sizeOfTheSolution)
        {
            var rnd = new Random();
            return source.OrderBy(s => rnd.Next()).Take(sizeOfTheSolution).ToList();
        }

        private static void PrintSolution(IList<int> combinations, int sum)
        {
            string values = String.Join(", ", combinations);
            Console.WriteLine($"The solution subset is: {values} having the sum of {sum} ]\n");
        }

        private static void PrintIntegerList(List<int> enumerable)
        {
            foreach (var element in enumerable)
            {
                Console.Write($"{element} ");
            }
            Console.WriteLine();
        }

        private static void PrintIntegerListOfLists(List<List<int>> enumerable)
        {
            foreach (var element in enumerable)
            {
                PrintIntegerList(element);
            }
        }
    }
}
