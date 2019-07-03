namespace GeneticAlgorithm
{
    using Entities.Contracts;
    using System;

    public class Individual : IIndividual
    {
        public Individual() { }

        public Individual(int geneLength)
        {
            this.GeneLength = geneLength;
            this.Fitness = 0;
            this.Genes = new int[GeneLength];

            this.SetGenes();
        }

        public int[] Genes { get; set; }

        public int GeneLength { get; set; }

        public int Fitness { get; set; }

        public void SetGenes()
        {
            for (int i = 0; i < GeneLength; i++)
            {
                this.Genes[i] = GetGene();
            }
        }

        private static int GetGene()
        {
            Random rn = new Random();
            return Math.Abs(rn.Next() % 2);
        }

        public void CalculateFitness()
        {
            this.Fitness = 0;
            for (int i = 0; i < this.GeneLength; i++)
            {
                if (this.Genes[i] == 1)
                {
                    //The fitness is incemented by one
                    this.Fitness++;
                }
            }
        }
    }
}
