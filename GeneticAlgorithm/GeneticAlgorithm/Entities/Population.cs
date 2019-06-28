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
            this.Fittest = 0;

            this.GetPopulationAndGeneLength();
            this.Individuals = new Individual[PopulationSize];
            this.InitializePopulation();
            this.CalculateFitness();
        }

        public int PopulationSize { get; set; }

        public IIndividual[] Individuals { get; set; }

        public int Fittest { get; private set; }

        public int GeneLength { get; private set; }

        public void InitializePopulation()
        {
            for (int i = 0; i < PopulationSize; i++)
            {
                this.Individuals[i] = new Individual(this.GeneLength);
            }
        }

        public IIndividual GetFittest()
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

            this.Fittest = Individuals[maxFitIndex].Fitness;

            return Individuals[maxFitIndex];
        }

        public IIndividual GetSecondFittest()
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

        public int GetLeastFittestIndex()
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

            this.GetFittest();
        }

        private void GetPopulationAndGeneLength()
        {
            this.writer.Write("Please enter population size: ");
            this.PopulationSize = int.Parse(this.reader.ReadLine());

            this.writer.Write("Please enter length of genes: ");
            this.GeneLength = int.Parse(this.reader.ReadLine());
        }
    }
}
