using ExecutionTimeMeasurement;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SSP
{

    //1. produce an initial population of individuals
    //2. evaluate the fitness of all individuals
    //  while termination condition not met do
    //      3. select fitter individuals for reproduction
    //      4. recombine between individuals
    //      5. mutate individuals
    //      6. evaluate the fitness of the modified individuals
    //      7. generate a new population
    //  end while

    public class SSGeneticCalculator : ISSCalculator
    {
        [Measure(nameof(Calculate), "Calculate method", 1)]
        public void Calculate(List<int> dataset)
        {
            var datasetLength = dataset.Count;
            // how many numbers in a possible solution
            var populationLength = new Random().Next(2, datasetLength - 1);
            // how many initial solutions 
            var numberOfPopulations = 1000;

            List<List<int>> initialPopulations = GenerateInitialPopulations(dataset, numberOfPopulations, populationLength);
            Console.WriteLine("Initial Populations");
            PrintIntegerListOfLists(initialPopulations);



            for (int iterations = 0; iterations < 50; iterations++)
            {
                ReplaceWeakestSolution(initialPopulations);
                Crossover(initialPopulations);
                Mutate(initialPopulations, dataset);
            }

            ExtractBestSolution(initialPopulations);

        }

        private void ExtractBestSolution(List<List<int>> initialPopulations)
        {
            var best = initialPopulations.OrderBy(s => Math.Abs(s.Sum())).Take(3).ToList();
            foreach (var list in best)
            {
                var sum = list.Sum();
                PrintSolution(list, sum);
            }
        }

        private void Mutate(List<List<int>> initialPopulations, List<int> dataset)
        {
            foreach (var population in initialPopulations)
            {
                for (int index = 0; index < population.Count; index++)
                {
                    if (new Random().Next(1, 100) < 15)
                    {
                        var intersection = dataset.Except(population).ToList();
                        population[index] = intersection
                                            .OrderBy(s => new Random().Next())
                                            .First();
                    }
                }

            }
        }

        private void Crossover(List<List<int>> initialPopulations)
        {
            for (int i = 0; i < initialPopulations.Count; i += 2)
            {
                // check the probability of crossover (80%)
                if (new Random().Next(1, 100) < 80)
                {
                    var crossoverPoint = new Random().Next(0, initialPopulations[0].Count);
                    SwapElements(initialPopulations[i], initialPopulations[i + 1], crossoverPoint);
                }
            }
        }

        private void SwapElements(List<int> mother, List<int> father, int crossoverPoint)
        {
            var length = mother.Count;
            for (int i = crossoverPoint; i < length; i++)
            {
                //cu tractoru pe campie
                var temp = mother[i];
                mother[i] = father[i];
                father[i] = temp;
            }

        }

        private void ReplaceWeakestSolution(List<List<int>> initialPopulations)
        {
            int minPos = 0;
            int maxPos = 0;
            int minSum = Math.Abs(initialPopulations[0].Sum());
            int maxSum = minSum;

            for (int i = 0; i < initialPopulations.Count; i++)
            {
                int currentSum = Math.Abs(initialPopulations[i].Sum());
                if (currentSum > maxSum)
                {
                    maxSum = currentSum;
                    maxPos = i;
                }

                if (currentSum < minSum)
                {
                    minSum = currentSum;
                    minPos = i;
                }
            }

            initialPopulations[maxPos] = initialPopulations[minPos];
        }

        private List<List<int>> GenerateInitialPopulations(List<int> dataset, int numberOfPopulations, int populationLength)
        {
            List<List<int>> initialPopulations = new List<List<int>>();
            for (int i = 0; i < numberOfPopulations; i++)
            {
                initialPopulations.Add(GenerateRandomSolution(dataset, populationLength));
            }

            return initialPopulations;
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
