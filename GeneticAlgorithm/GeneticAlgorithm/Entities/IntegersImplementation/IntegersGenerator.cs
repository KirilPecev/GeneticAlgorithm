namespace GeneticAlgorithm.Entities.IntegersImplementation
{
    using Contracts;
    using Core.IO.Contracts;
    using System.Linq;

    public class IntegersGenerator : GeneticAlgorithm<int>
    {
        public IntegersGenerator(IPopulation<int> population, IWriter writer)
            : base(population, writer)
        {
            FittestIndividual = new Individual(population.GeneLength);
            SecondFittestIndividual = new Individual(population.GeneLength);
            FittestIndividualForAllTime = new Individual(population.GeneLength);
        }

        public override void CreateTheFittestForAllTimeIndividual(int generationCount)
        {

            if (FittestIndividualForAllTime.Fitness <= FittestIndividual.Fitness)
            {
                FittestIndividualForAllTime = new Individual
                {
                    GeneLength = FittestIndividual.GeneLength,
                    Genes = FittestIndividual.Genes.ToArray(),
                    Fitness = FittestIndividual.Fitness
                };

                BestGeneration = generationCount;
            }
        }

        protected override void FlipValues(IIndividual<int> individual, int mutationPoint)
        {
            if (individual.Genes[mutationPoint] == 0)
            {
                individual.Genes[mutationPoint] = 1;
            }
            else
            {
                individual.Genes[mutationPoint] = 0;
            }
        }
    }
}
