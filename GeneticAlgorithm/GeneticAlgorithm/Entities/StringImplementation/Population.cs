namespace GeneticAlgorithm.Entities.StringImplementation
{
    using System;
    using Contracts;
    using Core.IO.Contracts;

    public class Population : IPopulation<char>
    {
        private readonly IReader reader;
        private readonly IWriter writer;

        public Population(IReader reader, IWriter writer)
        {
            this.GetPopulationSizeAndDesiredChromosome();
            this.Fittest = 0;
            
            this.Individuals = new Individual[PopulationSize];
        }

        public int Fittest { get; private set; }

        public string Chromosome { get; private set; }

        public int FittestIndividual { get; private set; }

        public int GeneLength => this.Chromosome.Length;

        public int PopulationSize { get; private set; }

        public IIndividual<char>[] Individuals { get; private set; }

        public void InitializePopulation()
        {
            for (int i = 0; i < this.PopulationSize; i++)
            {
                this.Individuals[i] = new Individual(Chromosome);
            }
        }

        public Individual GetFittest()
        {
            int maxFit = int.MinValue;
            int maxFitIndex = 0;

            for (int i = 0; i < this.PopulationSize; i++)
            {
                if (maxFit <= this.Individuals[i].Fitness)
                {
                    maxFit = this.Individuals[i].Fitness;
                    maxFitIndex = i;
                }
            }

            Fittest = this.Individuals[maxFitIndex].Fitness;

            return this.Individuals[maxFitIndex];
        }

        public Individual GetSecondFittest()
        {
            int currentIndex = 0;
            int maxFitIndex = 0;

            for (int i = 0; i < this.PopulationSize; i++)
            {
                if (this.Individuals[i].Fitness >this.Individuals[currentIndex].Fitness)
                {
                    maxFitIndex = currentIndex;
                    currentIndex = i;
                }
                else if (this.Individuals[i].Fitness > this.Individuals[maxFitIndex].Fitness)
                {
                    maxFitIndex = i;
                }
            }

            return this.Individuals[maxFitIndex];
        }

        public int GetLeastFittestIndex()
        {
            int minFitVal = int.MaxValue;
            int minFitIndex = 0;

            for (int i = 0; i < this.PopulationSize; i++)
            {
                if (minFitVal >= this.Individuals[i].Fitness)
                {
                    minFitVal = this.Individuals[i].Fitness;
                    minFitIndex = i;
                }
            }

            return minFitIndex;
        }

        public void CalculateFitness()
        {
            for (int i = 0; i < this.PopulationSize; i++)
            {
                this.Individuals[i].CalculateFitness();
            }

            this.GetFittest();
        }

        public IIndividual<int> GetFittestIndividual()
        {
            throw new System.NotImplementedException();
        }

        public int GetIndexOfWeakestIndividual()
        {
            throw new System.NotImplementedException();
        }

        public IIndividual<int> GetSecondFittestIndividual()
        {
            throw new System.NotImplementedException();
        }

        IIndividual<char> IPopulation<char>.GetFittestIndividual()
        {
            throw new System.NotImplementedException();
        }

        IIndividual<char> IPopulation<char>.GetSecondFittestIndividual()
        {
            throw new System.NotImplementedException();
        }

        private void GetPopulationSizeAndDesiredChromosome()
        {
            int size = 0;
            string desiredChromosome = string.Empty;
            while (true)
            {
                writer.Write("Please enter population size: ");
                var sizeResult = int.TryParse(reader.ReadLine(), result: out size);

                writer.Write("Please enter the string, which I must find: ");
                var chromosome = reader.ReadLine();

                if (sizeResult && !string.IsNullOrEmpty(chromosome))
                {
                    this.PopulationSize = size;
                    this.Chromosome = chromosome;
                    break;
                }
            }
        }
    }
}
