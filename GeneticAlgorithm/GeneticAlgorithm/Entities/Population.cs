namespace GeneticAlgorithm
{
    using Core.IO.Contracts;
    using Entities.Contracts;

    public class Population : IPopulation
    {
        private readonly IReader reader;
        private readonly IWriter writer;

        public Population(IReader reader, IWriter writer)
        {
            this.reader = reader;
            this.writer = writer;
            this.FittestIndividual = 0;

            this.GetPopulationAndGeneLength();
            this.Individuals = new Individual[PopulationSize];
            this.InitializePopulation();
            this.CalculateFitness();
        }

        public int PopulationSize { get; private set; }

        public IIndividual[] Individuals { get; private set; }

        public int FittestIndividual { get; private set; }

        public int GeneLength { get; private set; }

        public void InitializePopulation()
        {
            for (int i = 0; i < PopulationSize; i++)
            {
                this.Individuals[i] = new Individual(this.GeneLength);
            }
        }

        public IIndividual GetFittestIndividual()
        {
            int maxFit = int.MinValue;
            int maxFitIndex = 0;

            for (int i = 0; i < PopulationSize; i++)
            {
                if (maxFit <= this.Individuals[i].Fitness)
                {
                    maxFit = this.Individuals[i].Fitness;
                    maxFitIndex = i;
                }
            }

            this.FittestIndividual = Individuals[maxFitIndex].Fitness;

            return Individuals[maxFitIndex];
        }

        public IIndividual GetSecondFittestIndividual()
        {
            int currentIndex = 0;
            int maxFitIndex = 0;

            for (int i = 0; i < PopulationSize; i++)
            {
                if (this.Individuals[i].Fitness > this.Individuals[currentIndex].Fitness)
                {
                    maxFitIndex = currentIndex;
                    currentIndex = i;
                }
                else if (this.Individuals[i].Fitness > Individuals[maxFitIndex].Fitness)
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
            for (int i = 0; i < PopulationSize; i++)
            {
                this.Individuals[i].CalculateFitness();
            }

            this.GetFittestIndividual();
        }

        private void GetPopulationAndGeneLength()
        {
            int size = 0;
            int geneLength = 0;
            while (true)
            {
                this.writer.Write("Please enter population size: ");
                var sizeResult = int.TryParse(this.reader.ReadLine(), result: out size);

                this.writer.Write("Please enter length of genes: ");
                var genesResult = int.TryParse(this.reader.ReadLine(), result: out geneLength);

                if (sizeResult && genesResult)
                {
                    this.PopulationSize = size;
                    this.GeneLength = geneLength;
                    break;
                }
            }
        }
    }
}
