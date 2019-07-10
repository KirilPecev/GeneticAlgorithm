namespace GeneticAlgorithm.Core
{
    using Contracts;
    using Entities.Contracts;

    public class Engine : IEngine
    {
        private readonly IGenerator<int> generator;

        public Engine(IGenerator<int> generator)
        {
            this.generator = generator;
        }

        public void Run()
        {
            this.generator.Generate();
        }
    }
}
