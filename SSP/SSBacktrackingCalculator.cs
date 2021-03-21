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
            int count = dataset.Count;

            foreach (var numberOfCombinationsItemToTake in Enumerable.Range(1, count + 1))
            {
                foreach (var combinationsList in new Combinations<int>(dataset, numberOfCombinationsItemToTake)) //combinations = subset of the original set containing only a number of 'numberOfCombinationsItemToTake' items
                {
                    var currentSum = Math.Abs(combinationsList.Sum());
                    //Print the intermediary results
                    //PrintSolution(combinationsList, currentSum);
                    if(currentSum < minimumSum)
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
            Console.WriteLine($"The solution subset is: {values} having the sum of {sum} ");
        }
    }
}
