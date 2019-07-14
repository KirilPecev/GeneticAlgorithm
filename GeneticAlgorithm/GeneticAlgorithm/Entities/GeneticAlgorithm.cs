namespace GeneticAlgorithm.Entities
{
    using Contracts;
    using Core.IO.Contracts;
    using System;

    public abstract class GeneticAlgorithm<T> : IStop<T>
    {
        private const int MaxGenerationCount = 1000;
        private const int ProbabilityNumber = 7;

        private readonly string Dashes = new string('-', 80);
        private readonly string JoinSeparator = string.Empty;

        protected readonly IPopulation<T> population;
        private readonly IWriter writer;

        protected GeneticAlgorithm(IPopulation<T> population, IWriter writer)
        {
            this.population = population;
            this.writer = writer;
        }

        public IIndividual<T> FittestIndividual { get; protected set; }

        public IIndividual<T> SecondFittestIndividual { get; protected set; }

        public IIndividual<T> FittestIndividualForAllTime { get; protected set; }

        public int BestGeneration { get; protected set; }

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

        protected virtual void PrintStatus(int generationCount)
        {
            string genes = GetGenes(population.GetFittestIndividual().Genes);
            writer.WriteLine($"Generation: {generationCount} Fittest: {population.FittestIndividual} Genes: {genes}");
        }

        public virtual bool CheckForStop(int generationCount)
        {
            if (generationCount == MaxGenerationCount)
            {
                return true;
            }

            return false;
        }

        protected virtual void MutateUnderSomeProbability()
        {
            int probability = GetRandomProbability();

            if (probability < population.GeneLength)
            {
                //Mutate the fittests 2 of population
                Mutation();
            }
        }

        protected virtual int GetRandomProbability()
        {
            Random rn = new Random();
            return rn.Next() % ProbabilityNumber;
        }

        protected virtual string GetGenes(T[] genes)
        {
            return string.Join(JoinSeparator, genes);
        }

        protected virtual void ReplaceLeastFittestFromOffspring()
        {
            //Get index of weakest individual
            int indexOfWeakestIndividual = population.GetIndexOfWeakestIndividual();

            //Replace the weakest individual with fittest from offspring
            population.Individuals[indexOfWeakestIndividual] = GetFittestFromOffspring();
        }

        protected virtual void UpdateFitnessValuesFromOffspring()
        {
            FittestIndividual.CalculateFitness();
            SecondFittestIndividual.CalculateFitness();
        }

        protected virtual IIndividual<T> GetFittestFromOffspring()
        {
            if (FittestIndividual.Fitness > SecondFittestIndividual.Fitness)
            {
                return FittestIndividual;
            }

            return SecondFittestIndividual;
        }

        protected virtual void Selection()
        {
            FittestIndividual = population.GetFittestIndividual();
            SecondFittestIndividual = population.GetSecondFittestIndividual();
        }

        protected virtual void Crossover()
        {
            //Select a random crossover point
            int crossoverPoint = GetRandomPoint();

            //Swap values among parents
            SwapValuesAmongParents(crossoverPoint);
        }

        protected virtual void SwapValuesAmongParents(int crossoverPoint)
        {
            for (int i = 0; i < crossoverPoint; i++)
            {
                T temp = FittestIndividual.Genes[i];
                FittestIndividual.Genes[i] = SecondFittestIndividual.Genes[i];
                SecondFittestIndividual.Genes[i] = temp;
            }
        }

        protected virtual int GetRandomPoint()
        {
            Random rn = new Random();

            return rn.Next(population.Individuals[0].GeneLength);
        }

        protected virtual void Mutation()
        {
            //Select a random mutation point
            int mutationPoint = GetRandomPoint();
            FlipValues(FittestIndividual, mutationPoint);

            mutationPoint = GetRandomPoint();
            FlipValues(SecondFittestIndividual, mutationPoint);
        }

        protected abstract void FlipValues(IIndividual<T> individual, int mutationPoint);

        public abstract void CreateTheFittestForAllTimeIndividual(int generationCount);

        protected virtual void PrintResult(int generationCount, bool isStopped)
        {
            T[] genes = population.GetFittestIndividual().Genes;
            int fitness = population.GetFittestIndividual().Fitness;

            PrintGenerationNumber(generationCount, isStopped, ref genes, ref fitness);
            PrintFitnessAndGenes(genes, fitness);
        }

        protected virtual void PrintGenerationNumber(int generationCount, bool isStopped, ref T[] genes, ref int fitness)
        {
            writer.WriteLine(Dashes);
            if (isStopped)
            {
                genes = FittestIndividualForAllTime.Genes;
                fitness = FittestIndividualForAllTime.Fitness;
                writer.WriteLine($"The best solution is found in generation {BestGeneration}");
            }
            else
            {
                writer.WriteLine($"Solution found in generation {generationCount}");
            }
        }

        protected virtual void PrintFitnessAndGenes(T[] genes, int fitness)
        {
            string fittestGenes = GetGenes(genes);

            writer.WriteLine($"Fitness: {fitness}");
            writer.WriteLine($"Genes: {fittestGenes}");
            writer.WriteLine(Dashes);
        }
    }
}
