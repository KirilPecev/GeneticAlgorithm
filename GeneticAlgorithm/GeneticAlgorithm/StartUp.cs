namespace GeneticAlgorithm
{
    using GeneticAlgorithm.Core;
    using GeneticAlgorithm.Core.Contracts;
    using GeneticAlgorithm.Core.IO;
    using GeneticAlgorithm.Core.IO.Contracts;
    using GeneticAlgorithm.Entities.Contracts;

    public class Program
    {
        public static void Main(string[] args)
        {
            IReader reader = new ConsoleReader();
            IWriter writer = new ConsoleWriter();

            IPopulation population = new Population(reader, writer);

            IGenerator generator = new Generator(population, writer);

            IEngine engine = new Engine(generator);

            engine.Run();
        }
    }
}
