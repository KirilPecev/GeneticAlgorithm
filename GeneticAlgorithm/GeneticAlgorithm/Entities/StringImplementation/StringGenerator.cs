namespace GeneticAlgorithm.Entities.StringImplementation
{
    using Core.IO.Contracts;
    using Entities.Contracts;
    using System;

    public class StringGenerator : GeneticAlgorithm<char>
    {
        public StringGenerator(IPopulation<char> population, IWriter writer)
            : base(population, writer)
        {
            this.FittestIndividual = new Individual(population.Chromosome);
            this.SecondFittestIndividual = new Individual(population.Chromosome);
            this.FittestIndividualForAllTime = new Individual(population.Chromosome);
        }

        protected override void FlipValues(IIndividual<char> individual, int mutationPoint)
        {
            if (individual.Genes[mutationPoint] != population.Chromosome[mutationPoint])
            {
                individual.Genes[mutationPoint] = population.Chromosome[mutationPoint];
            }
            else
            {
                individual.Genes[mutationPoint] = '#';
            }
        }

        public override void CreateTheFittestForAllTimeIndividual(int generationCount)
        {
            if (this.FittestIndividualForAllTime.Fitness <= this.FittestIndividual.Fitness)
            {
                this.FittestIndividualForAllTime = new Individual
                {
                    GeneLength = this.FittestIndividual.GeneLength,
                    Genes = GetGenes(FittestIndividual),
                    Fitness = this.FittestIndividual.Fitness
                };

                this.BestGeneration = generationCount;
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
