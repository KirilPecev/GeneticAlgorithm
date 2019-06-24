using GeneticAlgorithm.Core.Contracts;
using GeneticAlgorithm.Entities.Contracts;

namespace GeneticAlgorithm.Core
{
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
