namespace GeneticAlgorithm
{
    using GeneticAlgorithm.Core.IO.Contracts;
    using GeneticAlgorithm.Entities.Contracts;
    using System;

    public class Generator : IGenerator
    {
        private readonly IPopulation population;
        private readonly IWriter writer;

        public Generator(IPopulation population, IWriter writer)
        {
            this.population = population;
            this.writer = writer;
        }

        public IIndividual Fittest { get; private set; }

        public IIndividual SecondFittest { get; private set; }

        public void Generate()
        {
            Random rn = new Random();

            this.population.InitializePopulation();
            this.population.CalculateFitness();

            int generationCount = 0;

            this.writer.WriteLine($"Generation: {generationCount} Fittest: {this.population.Fittest}");

            while (population.Fittest < population.GeneLength)
            {
                generationCount++;

                this.Selection();

                this.Crossover();

                //Do mutation under a random probability
                if (rn.Next() % 7 < this.population.GeneLength)
                {
                    Mutation();
                }

                this.AddFittestOffspring();

                this.population.CalculateFitness();

                this.writer.WriteLine($"Generation: {generationCount} Fittest: {this.population.Fittest}");
            }

            this.writer.WriteLine($"Solution found in generation {generationCount}");
            this.writer.WriteLine($"Fitness: {this.population.GetFittest().Fitness}");
            this.writer.WriteLine("Genes: ");

            for (int i = 0; i < population.GeneLength; i++)
            {
                this.writer.Write(this.population.GetFittest().Genes[i].ToString());
            }
        }

        private void AddFittestOffspring()
        {
            //Update fitness values of offspring
            this.Fittest.CalculateFitness();
            this.SecondFittest.CalculateFitness();

            //Get index of least fit individual
            int leastFittestIndex = this.population.GetLeastFittestIndex();

            //Replace least fittest individual from most fittest offspring
            this.population.Individuals[leastFittestIndex] = this.GetFittestOffspring();
        }

        private IIndividual GetFittestOffspring()
        {
            if (this.Fittest.Fitness > this.SecondFittest.Fitness)
            {
                return Fittest;
            }

            return this.SecondFittest;
        }

        private void Selection()
        {
            this.Fittest = this.population.GetFittest();
            this.SecondFittest = this.population.GetSecondFittest();
        }

        private void Crossover()
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

        private void Mutation()
        {
            Random rn = new Random();

            //Select a random mutation point
            int mutationPoint = rn.Next(this.population.Individuals[0].GeneLength);

            //Flip values at the mutation point
            if (this.Fittest.Genes[mutationPoint] == 0)
            {
                this.Fittest.Genes[mutationPoint] = 1;
            }
            else
            {
                this.Fittest.Genes[mutationPoint] = 0;
            }

            mutationPoint = rn.Next(this.population.Individuals[0].GeneLength);

            if (this.SecondFittest.Genes[mutationPoint] == 0)
            {
                this.SecondFittest.Genes[mutationPoint] = 1;
            }
            else
            {
                this.SecondFittest.Genes[mutationPoint] = 0;
            }
        }
    }
}
