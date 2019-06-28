namespace GeneticAlgorithm
{
    using Core;
    using Core.Contracts;
    using Core.IO;
    using Core.IO.Contracts;
    using Entities.Contracts;

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
