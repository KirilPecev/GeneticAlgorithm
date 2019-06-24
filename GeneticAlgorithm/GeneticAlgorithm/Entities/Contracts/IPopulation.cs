namespace GeneticAlgorithm.Entities.Contracts
{
    public interface IPopulation
    {
        int Fittest { get; }

        int GeneLength { get; }

        IIndividual[] Individuals { get; set; }

        void CalculateFitness();

        IIndividual GetFittest();

        int GetLeastFittestIndex();

        IIndividual GetSecondFittest();

        void InitializePopulation();
    }
}