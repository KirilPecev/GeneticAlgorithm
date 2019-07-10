namespace GeneticAlgorithm.Entities.ZerosAndOnesImplementation
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

            this.FittestIndividual = new Individual(this.population.GeneLength);
            this.SecondFittestIndividual = new Individual(this.population.GeneLength);
            this.FittestIndividualForAllTime = new Individual(this.population.GeneLength);
        }

        public IIndividual<int> FittestIndividual { get; private set; }

        public IIndividual<int> SecondFittestIndividual { get; private set; }

        public IIndividual<int> FittestIndividualForAllTime { get; private set; }

        public int BestGeneration { get; private set; }

        public void Generate()
        {
            bool isStopped = false;
            int generationCount = 0;

            while (this.population.FittestIndividual < this.population.GeneLength)
            {
                generationCount++;

                //Get the fittests 2 individuals from population
                this.Selection();

                //Crossover among parents 
                this.Crossover();

                //Do mutation under a some probability
                MutateUnderSomeProbability();

                //Update fitness values of offspring
                UpdateFitnessValuesFromOffspring();

                //Replace weakest individual from population with fittest from offspring
                this.ReplaceLeastFittestFromOffspring();

                //Calculate fitness foreach individual in population
                this.population.CalculateFitness();

                //Print the current generation with his fittest individual and his genes
                string genes = GetGenes(this.population.GetFittestIndividual().Genes);
                this.writer.WriteLine($"Generation: {generationCount} Fittest: {this.population.FittestIndividual} Genes: {genes}");

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

            if (probability < this.population.GeneLength)
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
            int indexOfWeakestIndividual = this.population.GetIndexOfWeakestIndividual();

            //Replace the weakest individual with fittest from offspring
            this.population.Individuals[indexOfWeakestIndividual] = this.GetFittestFromOffspring();
        }

        private void UpdateFitnessValuesFromOffspring()
        {
            this.FittestIndividual.CalculateFitness();
            this.SecondFittestIndividual.CalculateFitness();
        }

        public IIndividual<int> GetFittestFromOffspring()
        {
            if (this.FittestIndividual.Fitness > this.SecondFittestIndividual.Fitness)
            {
                return FittestIndividual;
            }

            return this.SecondFittestIndividual;
        }

        public void Selection()
        {
            this.FittestIndividual = this.population.GetFittestIndividual();
            this.SecondFittestIndividual = this.population.GetSecondFittestIndividual();
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
                int temp = this.FittestIndividual.Genes[i];
                this.FittestIndividual.Genes[i] = this.SecondFittestIndividual.Genes[i];
                this.SecondFittestIndividual.Genes[i] = temp;
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
            FlipValues(this.FittestIndividual, mutationPoint);

            mutationPoint = GetRandomPoint();
            FlipValues(this.SecondFittestIndividual, mutationPoint);
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
            if (this.FittestIndividualForAllTime.Fitness <= this.FittestIndividual.Fitness)
            {
                this.FittestIndividualForAllTime = new Individual
                {
                    GeneLength = this.FittestIndividual.GeneLength,
                    Genes = this.FittestIndividual.Genes.ToArray(),
                    Fitness = this.FittestIndividual.Fitness
                };

                this.BestGeneration = generationCount;
            }
        }

        private void PrintResult(int generationCount, bool isStopped)
        {
            if (isStopped)
            {
                PrintTheBestFindResult();
                return;
            }

            this.writer.WriteLine(Dashes);
            this.writer.WriteLine($"Solution found in generation {generationCount}");
            this.writer.WriteLine($"Fitness: {this.population.GetFittestIndividual().Fitness}");
            string fittestGenes = GetGenes(this.population.GetFittestIndividual().Genes);
            this.writer.WriteLine($"Genes: {fittestGenes}");
            this.writer.WriteLine(Dashes);
        }

        private void PrintTheBestFindResult()
        {
            this.writer.WriteLine(Dashes);
            this.writer.WriteLine($"The best solution is found in generation {this.BestGeneration}");
            this.writer.WriteLine($"Fitness: {this.FittestIndividualForAllTime.Fitness}");

            string genesOfFittestForAllTime = GetGenes(this.FittestIndividualForAllTime.Genes);
            this.writer.WriteLine($"Genes: {genesOfFittestForAllTime}");
            this.writer.WriteLine(Dashes);
        }
    }
}
