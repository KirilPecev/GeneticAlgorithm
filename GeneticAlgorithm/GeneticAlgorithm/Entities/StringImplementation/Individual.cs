namespace GeneticAlgorithm.Entities.StringImplementation
{
    using Contracts;
    using System;

    public class Individual : IIndividual<char>
    {
        public const string DefaultAllowedGenes = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ 1234567890, .-;:_!\"#%&/()=?@${[]}";

        private readonly string chromosome;

        public Individual() { }

        public Individual(string chromosome)
        {
            this.chromosome = chromosome;
            GeneLength = chromosome.Length;
            Fitness = 0;
            Genes = new char[this.GeneLength];

            SetGenes();
        }

        public int Fitness { get; set; }

        public int GeneLength { get; set; }

        public char[] Genes { get; set; }

        public void SetGenes()
        {
            for (int i = 0; i < GeneLength; i++)
            {
                Genes[i] = GetGene();
            }
        }

        private char GetGene()
        {
            Random rn = new Random();
            int randomIndex = rn.Next(0, GeneLength - 1);
            return DefaultAllowedGenes[randomIndex];
        }

        public void CalculateFitness()
        {
            Fitness = 0;
            for (int i = 0; i < GeneLength; i++)
            {
                if (Genes[i] == chromosome[i])
                {
                    Fitness++;
                }
            }
        }
    }
}
