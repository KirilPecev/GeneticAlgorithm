namespace GeneticAlgorithm.Entities.IntegersImplementation
{
    using Core.IO.Contracts;
    using Entities.Contracts;
    using System;
    using System.Linq;

    public class Generator : IGenerator<int>, IStop<int>
    {
        private const int MaxGenerationCount = 1000;
        private const int ProbabilityNumber = 7;

        private readonly string Dashes = new string('-', 80);
        private readonly string JoinSeparator = string.Empty;

        private readonly IPopulation<int> population;
        private readonly IWriter writer;

        public Generator(IPopulation<int> population, IWriter writer)
        {
            this.population = population;
            this.writer = writer;

            FittestIndividual = new Individual(this.population.GeneLength);
            SecondFittestIndividual = new Individual(this.population.GeneLength);
            FittestIndividualForAllTime = new Individual(this.population.GeneLength);
        }

        public IIndividual<int> FittestIndividual { get; private set; }

        public IIndividual<int> SecondFittestIndividual { get; private set; }

        public IIndividual<int> FittestIndividualForAllTime { get; private set; }

        public int BestGeneration { get; private set; }

        public void Generate()
        {
            bool isStopped = false;
            int generationCount = 0;

            while (population.FittestIndividual < population.GeneLength)
            {
                generationCount++;

                //Get the fittests 2 individuals from population
                Selection();

                //Crossover among parents 
                Crossover();

                //Do mutation under a some probability
                MutateUnderSomeProbability();

                //Update fitness values of offspring
                UpdateFitnessValuesFromOffspring();

                //Replace weakest individual from population with fittest from offspring
                ReplaceLeastFittestFromOffspring();

                //Calculate fitness foreach individual in population
                population.CalculateFitness();

                //Print the current generation with his fittest individual and his genes
                PrintStatus(generationCount);

                CreateTheFittestForAllTimeIndividual(generationCount);

                //Check that if generationCount is equal to MaxGenerationCount
                isStopped = CheckForStop(generationCount);

                if (isStopped)
                {
                    break;
                }
            }

            PrintResult(generationCount, isStopped);
        }

        private void PrintStatus(int generationCount)
        {
            string genes = GetGenes(population.GetFittestIndividual().Genes);
            writer.WriteLine($"Generation: {generationCount} Fittest: {population.FittestIndividual} Genes: {genes}");
        }

        public bool CheckForStop(int generationCount)
        {
            if (generationCount == MaxGenerationCount)
            {
                return true;
            }

            return false;
        }

        private void MutateUnderSomeProbability()
        {
            int probability = GetRandomProbability();

            if (probability < population.GeneLength)
            {
                //Mutate the fittests 2 of population
                Mutation();
            }
        }

        private static int GetRandomProbability()
        {
            Random rn = new Random();
            return rn.Next() % ProbabilityNumber;
        }

        private string GetGenes(int[] genes)
        {
            return string.Join(JoinSeparator, genes);
        }

        public void ReplaceLeastFittestFromOffspring()
        {
            //Get index of weakest individual
            int indexOfWeakestIndividual = population.GetIndexOfWeakestIndividual();

            //Replace the weakest individual with fittest from offspring
            population.Individuals[indexOfWeakestIndividual] = GetFittestFromOffspring();
        }

        private void UpdateFitnessValuesFromOffspring()
        {
            FittestIndividual.CalculateFitness();
            SecondFittestIndividual.CalculateFitness();
        }

        public IIndividual<int> GetFittestFromOffspring()
        {
            if (FittestIndividual.Fitness > SecondFittestIndividual.Fitness)
            {
                return FittestIndividual;
            }

            return SecondFittestIndividual;
        }

        public void Selection()
        {
            FittestIndividual = population.GetFittestIndividual();
            SecondFittestIndividual = population.GetSecondFittestIndividual();
        }

        public void Crossover()
        {
            //Select a random crossover point
            int crossoverPoint = GetRandomPoint();

            //Swap values among parents
            SwapValuesAmongParents(crossoverPoint);
        }

        private void SwapValuesAmongParents(int crossoverPoint)
        {
            for (int i = 0; i < crossoverPoint; i++)
            {
                int temp = FittestIndividual.Genes[i];
                FittestIndividual.Genes[i] = SecondFittestIndividual.Genes[i];
                SecondFittestIndividual.Genes[i] = temp;
            }
        }

        private int GetRandomPoint()
        {
            Random rn = new Random();

            return rn.Next(population.Individuals[0].GeneLength);
        }

        public void Mutation()
        {
            //Select a random mutation point
            int mutationPoint = GetRandomPoint();
            FlipValues(FittestIndividual, mutationPoint);

            mutationPoint = GetRandomPoint();
            FlipValues(SecondFittestIndividual, mutationPoint);
        }

        private void FlipValues(IIndividual<int> individual, int mutationPoint)
        {
            if (individual.Genes[mutationPoint] == 0)
            {
                individual.Genes[mutationPoint] = 1;
            }
            else
            {
                individual.Genes[mutationPoint] = 0;
            }
        }

        public void CreateTheFittestForAllTimeIndividual(int generationCount)
        {
            if (FittestIndividualForAllTime.Fitness <= FittestIndividual.Fitness)
            {
                FittestIndividualForAllTime = new Individual
                {
                    GeneLength = FittestIndividual.GeneLength,
                    Genes = FittestIndividual.Genes.ToArray(),
                    Fitness = FittestIndividual.Fitness
                };

                BestGeneration = generationCount;
            }
        }

        private void PrintResult(int generationCount, bool isStopped)
        {
            if (isStopped)
            {
                PrintTheBestFindResult();
                return;
            }

            writer.WriteLine(Dashes);
            writer.WriteLine($"Solution found in generation {generationCount}");
            writer.WriteLine($"Fitness: {population.GetFittestIndividual().Fitness}");
            string fittestGenes = GetGenes(population.GetFittestIndividual().Genes);
            writer.WriteLine($"Genes: {fittestGenes}");
            writer.WriteLine(Dashes);
        }

        private void PrintTheBestFindResult()
        {
            writer.WriteLine(Dashes);
            writer.WriteLine($"The best solution is found in generation {BestGeneration}");
            writer.WriteLine($"Fitness: {FittestIndividualForAllTime.Fitness}");

            string genesOfFittestForAllTime = GetGenes(FittestIndividualForAllTime.Genes);
            writer.WriteLine($"Genes: {genesOfFittestForAllTime}");
            writer.WriteLine(Dashes);
        }
    }
}
