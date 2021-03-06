using System;
using System.Collections.Generic;
using System.Linq;
using Combinatorics.Collections;
using ExecutionTimeMeasurement;

namespace SSP
{
    public class SSBacktrackingCalculator : ISSCalculator
    {
        [Measure(nameof(Calculate), "Calculate method", 1)]
        public void Calculate(List<int> dataset)
        {
            var minimumSum = Math.Abs(dataset[0]);
            var minimumSubset = new List<int>() { dataset[0] };
            int numberOfElements = dataset.Count;

            var listOfNumbersOfCombinations = Enumerable.Range(1, numberOfElements + 1);
            foreach (var numberOfCombinationsItemToTake in listOfNumbersOfCombinations)
            {
                //combinations = subset of the original set containing only a number of 'numberOfCombinationsItemToTake' items
                foreach (var combinationsList in new Combinations<int>(dataset, numberOfCombinationsItemToTake)) 
                {
                    var currentSum = Math.Abs(combinationsList.Sum());
                    //if (currentSum == 0)
                    //{
                    //    Console.WriteLine("Found the first perfect solution. Stopping!\n");
                    //    PrintSolution(combinationsList, currentSum);
                    //    return;
                    //}
                    //Print the intermediary results
                    //Console.WriteLine($"[Minimum sum so far is: {minimumSum}");
                    //PrintSolution(combinationsList, currentSum);
                    if (currentSum < minimumSum)
                    {
                        minimumSum = currentSum;
                        minimumSubset = (List<int>)combinationsList;
                    }

                }
            }
            PrintSolution(minimumSubset, minimumSum);
        }

        private static void PrintSolution(IList<int> combinations, int sum)
        {
            string values = String.Join(", ", combinations);
            Console.WriteLine($"The solution subset is: {values} having the sum of {sum} ]\n");
        }
    }
}
