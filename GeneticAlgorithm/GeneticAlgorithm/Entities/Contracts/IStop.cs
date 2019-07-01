namespace GeneticAlgorithm.Entities.Contracts
{
    public interface IStop
    {
        IIndividual FittestIndividualForAllTime { get; }

        int BestGeneration { get; }

        void Stop(int generationCount);
    }
}
