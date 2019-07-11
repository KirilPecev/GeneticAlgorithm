namespace GeneticAlgorithm.Entities.StringImplementation
{
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

        public IIndividual<char> GetFittestIndividual()
        {
            int maxFit = int.MinValue;
            int maxFitIndex = 0;

            for (int i = 0; i < PopulationSize; i++)
            {
                if (maxFit <= Individuals[i].Fitness)
                {
                    maxFit = Individuals[i].Fitness;
                    maxFitIndex = i;
                }
            }

            FittestIndividual = Individuals[maxFitIndex].Fitness;

            return Individuals[maxFitIndex];
        }

        public IIndividual<char> GetSecondFittestIndividual()
        {
            int currentIndex = 0;
            int maxFitIndex = 0;

            for (int i = 0; i < PopulationSize; i++)
            {
                if (Individuals[i].Fitness > Individuals[currentIndex].Fitness)
                {
                    maxFitIndex = currentIndex;
                    currentIndex = i;
                }
                else if (Individuals[i].Fitness > Individuals[maxFitIndex].Fitness)
                {
                    maxFitIndex = i;
                }
            }

            return Individuals[maxFitIndex];
        }

        public int GetIndexOfWeakestIndividual()
        {
            int minFitVal = int.MaxValue;
            int minFitIndex = 0;

            for (int i = 0; i < PopulationSize; i++)
            {
                if (minFitVal >= Individuals[i].Fitness)
                {
                    minFitVal = Individuals[i].Fitness;
                    minFitIndex = i;
                }
            }

            return minFitIndex;
        }

        public void CalculateFitness()
        {
            for (int i = 0; i < PopulationSize; i++)
            {
                Individuals[i].CalculateFitness();
            }

            GetFittestIndividual();
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
