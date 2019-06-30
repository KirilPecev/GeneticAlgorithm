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

            this.Fittest = new Individual(this.population.GeneLength);
            this.SecondFittest = new Individual(this.population.GeneLength);
            this.FittestForAllTime = new Individual(this.population.GeneLength);
        }

        public IIndividual Fittest { get; private set; }

        public IIndividual SecondFittest { get; private set; }

        public IIndividual FittestForAllTime { get; set; }

        public int BestGeneration { get; set; }

        public void Generate()
        {
            int generationCount = 0;

            this.writer.WriteLine($"Generation: {generationCount} Fittest: {this.population.Fittest}");

            while (this.population.Fittest < this.population.GeneLength)
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

                string genes = GetGenes(this.population.GetFittest().Genes);
                this.writer.WriteLine($"Generation: {generationCount} Fittest: {this.population.Fittest} Genes: {genes}");
            }

            PrintResult(generationCount);
        }

        private void PrintResult(int generationCount)
        {
            this.writer.WriteLine(GlobalConstants.Dashes);

            this.writer.WriteLine($"Solution found in generation {generationCount}");
            this.writer.WriteLine($"Fitness: {this.population.GetFittest().Fitness}");

            string fittestGenes = GetGenes(this.population.GetFittest().Genes);
            this.writer.WriteLine($"Genes: {fittestGenes}");

            this.writer.WriteLine(GlobalConstants.Dashes);
        }

        private void MutateUnderSomeProbability()
        {
            Random rn = new Random();

            if (rn.Next() % GlobalConstants.ProbabilityNumber < this.population.GeneLength)
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
            this.Fittest.CalculateFitness();
            this.SecondFittest.CalculateFitness();

            //Get index of least fit individual
            int leastFittestIndex = this.population.GetLeastFittestIndex();

            //Replace the weakest individual with fittest of offspring
            this.population.Individuals[leastFittestIndex] = this.GetFittestFromOffspring();
        }

        public IIndividual GetFittestFromOffspring()
        {
            if (this.Fittest.Fitness > this.SecondFittest.Fitness)
            {
                return Fittest;
            }

            return this.SecondFittest;
        }

        public void Selection()
        {
            this.Fittest = this.population.GetFittest();
            this.SecondFittest = this.population.GetSecondFittest();
        }

        public void Crossover()
        {
            Random rn = new Random();

            //Select a random crossover point
            int crossOverPoint = rn.Next(population.Individuals[0].GeneLength);

            //Swap values among parents
            for (int i = 0; i < crossOverPoint; i++)
            {
                int temp = this.Fittest.Genes[i];
                this.Fittest.Genes[i] = this.SecondFittest.Genes[i];
                this.SecondFittest.Genes[i] = temp;
            }
        }

        public void Mutation()
        {
            Random rn = new Random();

            //Select a random mutation point
            int mutationPoint = rn.Next(this.population.Individuals[0].GeneLength);
            FlipValuesAtRandomMutationPoint(this.Fittest, mutationPoint);

            mutationPoint = rn.Next(this.population.Individuals[0].GeneLength);
            FlipValuesAtRandomMutationPoint(this.SecondFittest, mutationPoint);
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
            if (this.FittestForAllTime.Fitness <= this.Fittest.Fitness)
            {
                this.FittestForAllTime = new Individual
                {
                    GeneLength = this.Fittest.GeneLength,
                    Genes = this.Fittest.Genes.ToArray(),
                    Fitness = this.Fittest.Fitness
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
                this.writer.WriteLine($"Fitness: {this.FittestForAllTime.Fitness}");

                string genesOfFittestForAllTime = GetGenes(this.FittestForAllTime.Genes);
                this.writer.WriteLine($"Genes: {genesOfFittestForAllTime}");
                this.writer.WriteLine(GlobalConstants.Dashes);

                Environment.Exit(0);
            }
        }
    }
}
