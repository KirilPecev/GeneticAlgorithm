namespace GeneticAlgorithm.Entities.Contracts
{
    public interface IGenerator
    {
        IIndividual FittestIndividual { get; }

        IIndividual SecondFittestIndividual { get; }

        void Generate();

        void Mutation();

        void Crossover();

        void Selection();

        IIndividual GetFittestFromOffspring();

        void ReplaceLeastFittestFromOffspring();
    }
}