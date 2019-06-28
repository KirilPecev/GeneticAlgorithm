namespace GeneticAlgorithm.Entities.Contracts
{
    public interface IStop
    {
        IIndividual FittestForAllTime { get; set; }

        int BestGeneration { get; set; }

        void Stop(int generationCount);
    }
}
