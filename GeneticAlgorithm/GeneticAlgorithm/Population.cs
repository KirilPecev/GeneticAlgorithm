namespace GeneticAlgorithm
{
    public class Population
    {
        private readonly int PopulationSize;

        public Population(int size)
        {
            this.PopulationSize = size;
            this.Individuals = new Individual[PopulationSize];
            this.Fittest = 0;
        }

        public Individual[] Individuals { get; set; }

        public int Fittest { get; private set; }

        public void InitializePopulation()
        {
            for (int i = 0; i < PopulationSize; i++)
            {
                this.Individuals[i] = new Individual();
            }
        }

        public Individual GetFittest()
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

        public Individual GetSecondFittest()
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
    }
}
