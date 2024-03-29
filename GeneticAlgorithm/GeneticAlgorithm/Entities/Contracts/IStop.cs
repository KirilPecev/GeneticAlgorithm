﻿namespace GeneticAlgorithm.Entities.Contracts
{
    public interface IStop<T>
    {
        IIndividual<T> FittestIndividualForAllTime { get; }

        int BestGeneration { get; }

        void CreateTheFittestForAllTimeIndividual(int generationCount);

        bool CheckForStop(int generationCount);
    }
}
