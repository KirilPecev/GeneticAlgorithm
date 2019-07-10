namespace GeneticAlgorithm.Entities.Contracts
{
    public interface IPopulation<T>
    {
        int FittestIndividual { get; }

        int GeneLength { get; }

        int PopulationSize { get;}

        IIndividual<T>[] Individuals { get; }

        void CalculateFitness();

        IIndividual<T> GetFittestIndividual();

        int GetIndexOfWeakestIndividual();

        IIndividual<T> GetSecondFittestIndividual();

        void InitializePopulation();
    }
}