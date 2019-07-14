namespace GeneticAlgorithm.Core.IO
{
    using Contracts;
    using System;

    public class ConsoleCleaner : ICleaner
    {
        public void Clean()
        {
            Console.Clear();
        }
    }
}
