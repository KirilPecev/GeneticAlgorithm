namespace GeneticAlgorithm
{
    using GeneticAlgorithm.Entities.Contracts;
    using System;

    public class Individual : IIndividual
    {
        public Individual(int geneLength)
        {
            this.GeneLength = geneLength;
            this.Fitness = 0;
            this.Genes = new int[GeneLength];

            this.SetGenesRandomly();
        }

        public int[] Genes { get; private set; }

        public int GeneLength { get; private set; }

        public int Fitness { get; private set; }

        private void SetGenesRandomly()
        {
            Random rn = new Random();

            for (int i = 0; i < GeneLength; i++)
            {
                this.Genes[i] = Math.Abs(rn.Next() % 2);
            }
        }

        public void CalculateFitness()
        {
            this.Fitness = 0;
            for (int i = 0; i < this.GeneLength; i++)
            {
                if (this.Genes[i] == 1)
                {
                    this.Fitness++;
                }
            }
        }
    }
}
