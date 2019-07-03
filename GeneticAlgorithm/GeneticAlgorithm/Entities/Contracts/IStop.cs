namespace GeneticAlgorithm.Entities.Contracts
{
    public interface IStop
    {
        IIndividual FittestIndividualForAllTime { get; }

        int BestGeneration { get; }

        void CreateTheFittestForAllTimeIndividual(int generationCount);

        bool CheckForStop(int generationCount);
    }
}
