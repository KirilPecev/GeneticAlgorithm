namespace GeneticAlgorithm.Core
{
    using Contracts;
    using Entities.Contracts;

    public class Engine : IEngine
    {
        private readonly IGenerator<char> generator;

        public Engine(IGenerator<char> generator)
        {
            this.generator = generator;
        }

        public void Run()
        {
            this.generator.Generate();
        }
    }
}
