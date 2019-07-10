namespace GeneticAlgorithm.Entities.Contracts
{
    public interface IIndividual<T>
    {
        int Fitness { get; }

        int GeneLength { get; }

        T[] Genes { get; }

        void CalculateFitness();

        void SetGenes();
    }
}