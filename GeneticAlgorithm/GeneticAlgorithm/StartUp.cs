namespace GeneticAlgorithm
{
    using Core;
    using Core.Contracts;
    using Core.IO;
    using Core.IO.Contracts;
    using Entities.Contracts;
    using Entities.StringImplementation;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            IReader reader = new ConsoleReader();
            IWriter writer = new ConsoleWriter();

            IPopulation<char> population = new Population(reader, writer);

            IGenerator<char> generator = new Generator(population, writer);

            IEngine engine = new Engine(generator);

            engine.Run();
        }
    }
}
