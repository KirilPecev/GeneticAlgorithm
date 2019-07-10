namespace GeneticAlgorithm.Entities.Contracts
{
    public interface IGenerator<T>
    {
        IIndividual<T> FittestIndividual { get; }

        IIndividual<T> SecondFittestIndividual { get; }

        void Generate();

        void Mutation();

        void Crossover();

        void Selection();

        IIndividual<T> GetFittestFromOffspring();

        void ReplaceLeastFittestFromOffspring();
    }
}