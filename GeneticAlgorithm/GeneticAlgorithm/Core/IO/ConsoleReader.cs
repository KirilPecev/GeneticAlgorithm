namespace GeneticAlgorithm.Core.IO
{
    using GeneticAlgorithm.Core.IO.Contracts;
    using System;

    public class ConsoleReader : IReader
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
