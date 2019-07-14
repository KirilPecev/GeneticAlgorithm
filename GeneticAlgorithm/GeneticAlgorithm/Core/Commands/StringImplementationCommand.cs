namespace GeneticAlgorithm.Core.Commands
{
    using Contracts;
    using Entities.Contracts;
    using Entities.StringImplementation;
    using IO.Contracts;

    public class StringImplementationCommand : ICommand
    {
        private readonly IReader reader;
        private readonly IWriter writer;
        private readonly IPopulation<char> population;
        private readonly StringGenerator generator;

        public StringImplementationCommand(IReader reader, IWriter writer)
        {
            this.reader = reader;
            this.writer = writer;
            this.population = new Population(this.reader, this.writer);
            this.generator = new StringGenerator(population, writer);
        }

        public void Execute()
        {
            this.generator.Generate();
        }
    }
}
