namespace GeneticAlgorithm
{
    using Core.IO.Contracts;
    using Entities;
    using Entities.Contracts;
    using System;
    using System.Linq;

    public class Generator : IGenerator, IStop
    {
        private readonly IPopulation population;
        private readonly IWriter writer;

        public Generator(IPopulation population, IWriter writer)
        {
            this.population = population;
            this.writer = writer;

            this.FittestIndividual = new Individual(this.population.GeneLength);
            this.SecondFittestIndividual = new Individual(this.population.GeneLength);
            this.FittestIndividualForAllTime = new Individual(this.population.GeneLength);
        }

        public IIndividual FittestIndividual { get; private set; }

        public IIndividual SecondFittestIndividual { get; private set; }

        public IIndividual FittestIndividualForAllTime { get; private set; }

        public int BestGeneration { get; private set; }

        public void Generate()
        {
            int generationCount = 0;

            this.writer.WriteLine($"Generation: {generationCount} Fittest: {this.population.FittestIndividual}");

            while (this.population.FittestIndividual < this.population.GeneLength)
            {
                generationCount++;

                //Get the fittests 2 from population
                this.Selection();

                //Crossover among parents 
                this.Crossover();

                //Do mutation under a some probability
                MutateUnderSomeProbability();

                //Replace least fittest from offspring
                this.ReplaceLeastFittestFromOffspring();

                this.population.CalculateFitness();

                this.Stop(generationCount);

                string genes = GetGenes(this.population.GetFittestIndividual().Genes);
                this.writer.WriteLine($"Generation: {generationCount} Fittest: {this.population.FittestIndividual} Genes: {genes}");
            }

            PrintResult(generationCount);
        }

        private void PrintResult(int generationCount)
        {
            this.writer.WriteLine(GlobalConstants.Dashes);

            this.writer.WriteLine($"Solution found in generation {generationCount}");
            this.writer.WriteLine($"Fitness: {this.population.GetFittestIndividual().Fitness}");

            string fittestGenes = GetGenes(this.population.GetFittestIndividual().Genes);
            this.writer.WriteLine($"Genes: {fittestGenes}");

            this.writer.WriteLine(GlobalConstants.Dashes);
        }

        private void MutateUnderSomeProbability()
        {
            Random rn = new Random();

            int probability = rn.Next() % GlobalConstants.ProbabilityNumber;

            if (probability < this.population.GeneLength)
            {
                //Mutate the fittests 2 of population
                Mutation();
            }
        }

        private string GetGenes(int[] genes)
        {
            return string.Join(GlobalConstants.JoinSeparator, genes);
        }

        public void ReplaceLeastFittestFromOffspring()
        {
            //Update fitness values of offspring
            this.FittestIndividual.CalculateFitness();
            this.SecondFittestIndividual.CalculateFitness();

            //Get index of least fit individual
            int indexOfWeakestIndividual = this.population.GetIndexOfWeakestIndividual();

            //Replace the weakest individual with fittest of offspring
            this.population.Individuals[indexOfWeakestIndividual] = this.GetFittestFromOffspring();
        }

        public IIndividual GetFittestFromOffspring()
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
            Random rn = new Random();

            //Select a random crossover point
            int crossOverPoint = rn.Next(population.Individuals[0].GeneLength);

            //Swap values among parents
            for (int i = 0; i < crossOverPoint; i++)
            {
                int temp = this.FittestIndividual.Genes[i];
                this.FittestIndividual.Genes[i] = this.SecondFittestIndividual.Genes[i];
                this.SecondFittestIndividual.Genes[i] = temp;
            }
        }

        public void Mutation()
        {
            Random rn = new Random();

            //Select a random mutation point
            int mutationPoint = rn.Next(this.population.Individuals[0].GeneLength);
            FlipValuesAtRandomMutationPoint(this.FittestIndividual, mutationPoint);

            mutationPoint = rn.Next(this.population.Individuals[0].GeneLength);
            FlipValuesAtRandomMutationPoint(this.SecondFittestIndividual, mutationPoint);
        }

        private void FlipValuesAtRandomMutationPoint(IIndividual individual, int mutationPoint)
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

        public void Stop(int generationCount)
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

            PrintTheBestFindResult(generationCount);
        }

        private void PrintTheBestFindResult(int generationCount)
        {
            if (generationCount >= GlobalConstants.MaxGenerationCount)
            {
                this.writer.WriteLine(GlobalConstants.Dashes);
                this.writer.WriteLine($"The best solution is found in generation {this.BestGeneration}");
                this.writer.WriteLine($"Fitness: {this.FittestIndividualForAllTime.Fitness}");

                string genesOfFittestForAllTime = GetGenes(this.FittestIndividualForAllTime.Genes);
                this.writer.WriteLine($"Genes: {genesOfFittestForAllTime}");
                this.writer.WriteLine(GlobalConstants.Dashes);

                Environment.Exit(0);
            }
        }
    }
}
