namespace GeneticAlgorithm.Core.Commands
{
    using Contracts;
    using Entities.Contracts;
    using Entities.IntegersImplementation;
    using IO.Contracts;

    public class IntegersImplementationCommand : ICommand
    {
        private readonly IReader reader;
        private readonly IWriter writer;
        private readonly IPopulation<int> population;
        private readonly IntegersGenerator generator;

        public IntegersImplementationCommand(IReader reader, IWriter writer)
        {
            this.reader = reader;
            this.writer = writer;
            population = new Population(this.reader, this.writer);
            generator = new IntegersGenerator(population, writer);
        }

        public void Execute()
        {
            generator.Generate();
        }
    }
}
