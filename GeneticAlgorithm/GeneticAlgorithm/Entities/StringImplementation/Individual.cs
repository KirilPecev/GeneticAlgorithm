namespace GeneticAlgorithm.Entities.StringImplementation
{
    using GeneticAlgorithm.Entities.Contracts;
    using System;

    public class Individual : IIndividual<char>
    {
        private const string DefaultAllowedGenes = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ 1234567890, .-;:_!\"#%&/()=?@${[]}";

        private readonly string chromosome;

        public Individual() { }

        public Individual(string chromosome)
        {
            this.chromosome = chromosome;
            this.GeneLength = chromosome.Length;
            this.Fitness = 0;
            this.Genes = new char[this.GeneLength];

            this.SetGenes();
        }

        public int Fitness { get; set; }

        public int GeneLength { get; set; }

        public char[] Genes { get; set; }

        public void SetGenes()
        {
            for (int i = 0; i < this.GeneLength; i++)
            {
                this.Genes[i] = GetGene();
            }
        }

        private char GetGene()
        {
            Random rn = new Random();
            int randomIndex = rn.Next(0, this.GeneLength - 1);
            return DefaultAllowedGenes[randomIndex];
        }

        public void CalculateFitness()
        {
            this.Fitness = 0;
            for (int i = 0; i < this.GeneLength; i++)
            {
                if (this.Genes[i] == this.chromosome[i])
                {
                    this.Fitness++;
                }
            }
        }
    }
}
