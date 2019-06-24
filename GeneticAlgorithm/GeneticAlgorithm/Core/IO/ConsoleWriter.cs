namespace GeneticAlgorithm.Core.IO
{
    using GeneticAlgorithm.Core.IO.Contracts;
    using System;

    public class ConsoleWriter : IWriter
    {
        public void Write(string content)
        {
            Console.Write(content);
        }

        public void WriteLine(string content)
        {
            Console.WriteLine(content);
        }
    }
}
