namespace GeneticAlgorithm.Entities.Contracts
{
    public interface IIndividual
    {
        int Fitness { get; }

        int GeneLength { get; }

        int[] Genes { get; }

        void CalculateFitness();

        void SetGenes();
    }
}