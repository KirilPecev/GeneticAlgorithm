namespace GeneticAlgorithm.Core
{
    using Contracts;
    using Entities.Contracts;

    public class Engine : IEngine
    {
        private readonly IGenerator generator;

        public Engine(IGenerator generator)
        {
            this.generator = generator;
        }

        public void Run()
        {
            this.generator.Generate();
        }
    }
}
