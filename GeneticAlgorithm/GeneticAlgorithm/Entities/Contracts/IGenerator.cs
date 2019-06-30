namespace GeneticAlgorithm.Entities.Contracts
{
    public interface IGenerator
    {
        IIndividual Fittest { get; }

        IIndividual SecondFittest { get; }

        void Generate();

        void Mutation();

        void Crossover();

        void Selection();

        IIndividual GetFittestFromOffspring();

        void ReplaceLeastFittestFromOffspring();
    }
}