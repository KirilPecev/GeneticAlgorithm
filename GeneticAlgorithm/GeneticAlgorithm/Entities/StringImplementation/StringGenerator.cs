namespace GeneticAlgorithm.Entities.StringImplementation
{
    using Core.IO.Contracts;
    using Entities.Contracts;
    using System;

    public class StringGenerator : GeneticAlgorithm<char>
    {
        private const char MutationChar = '#';

        public StringGenerator(IPopulation<char> population, IWriter writer)
            : base(population, writer)
        {
            FittestIndividual = new Individual(population.Chromosome);
            SecondFittestIndividual = new Individual(population.Chromosome);
            FittestIndividualForAllTime = new Individual(population.Chromosome);
        }

        protected override void FlipValues(IIndividual<char> individual, int mutationPoint)
        {
            if (individual.Genes[mutationPoint] != population.Chromosome[mutationPoint])
            {
                individual.Genes[mutationPoint] = population.Chromosome[mutationPoint];
            }
            else
            {
                individual.Genes[mutationPoint] = MutationChar;
            }
        }

        public override void CreateTheFittestForAllTimeIndividual(int generationCount)
        {
            if (FittestIndividualForAllTime.Fitness <= FittestIndividual.Fitness)
            {
                FittestIndividualForAllTime = new Individual
                {
                    GeneLength = FittestIndividual.GeneLength,
                    Genes = GetGenes(FittestIndividual),
                    Fitness = FittestIndividual.Fitness
                };

                BestGeneration = generationCount;
            }
        }

        private char[] GetGenes(IIndividual<char> individual)
        {
            char[] array = new char[individual.Genes.Length];
            Array.Copy(individual.Genes, array, individual.Genes.Length);
            return array;
        }
    }
}
