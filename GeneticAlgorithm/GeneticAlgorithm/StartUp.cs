namespace GeneticAlgorithm
{
    using Core;
    using Core.Contracts;
    using Core.IO;
    using Core.IO.Contracts;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            IMenu menu = new Menu();
            IReader reader = new ConsoleReader();
            IWriter writer = new ConsoleWriter();
            ICleaner cleaner = new ConsoleCleaner();

            IEngine engine = new Engine(menu, reader, writer, cleaner);

            engine.Run();
        }
    }
}
