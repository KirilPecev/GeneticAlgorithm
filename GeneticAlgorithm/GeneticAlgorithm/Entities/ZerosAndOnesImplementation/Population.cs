namespace GeneticAlgorithm.Entities.ZerosAndOnesImplementation
{
    using Core.IO.Contracts;
    using Entities.Contracts;

    public class Population : IPopulation<int>
    {
        private readonly IReader reader;
        private readonly IWriter writer;

        public Population(IReader reader, IWriter writer)
        {
            this.reader = reader;
            this.writer = writer;
            FittestIndividual = 0;

            GetPopulationAndGeneLength();
            Individuals = new Individual[PopulationSize];
            InitializePopulation();
            CalculateFitness();
        }

        public int PopulationSize { get; private set; }

        public IIndividual<int>[] Individuals { get; private set; }

        public int FittestIndividual { get; private set; }

        public int GeneLength { get; private set; }

        public void InitializePopulation()
        {
            for (int i = 0; i < PopulationSize; i++)
            {
                Individuals[i] = new Individual(GeneLength);
            }
        }

        public IIndividual<int> GetFittestIndividual()
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

        public IIndividual<int> GetSecondFittestIndividual()
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

        private void GetPopulationAndGeneLength()
        {
            int size = 0;
            int geneLength = 0;
            while (true)
            {
                writer.Write("Please enter population size: ");
                var sizeResult = int.TryParse(reader.ReadLine(), result: out size);

                writer.Write("Please enter length of genes: ");
                var genesResult = int.TryParse(reader.ReadLine(), result: out geneLength);

                if (sizeResult && genesResult)
                {
                    PopulationSize = size;
                    GeneLength = geneLength;
                    break;
                }
            }
        }
    }
}
