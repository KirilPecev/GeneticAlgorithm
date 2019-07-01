namespace GeneticAlgorithm.Entities.Contracts
{
    public interface IPopulation
    {
        int FittestIndividual { get; }

        int GeneLength { get; }

        int PopulationSize { get;}

        IIndividual[] Individuals { get; }

        void CalculateFitness();

        IIndividual GetFittestIndividual();

        int GetIndexOfWeakestIndividual();

        IIndividual GetSecondFittestIndividual();

        void InitializePopulation();
    }
}