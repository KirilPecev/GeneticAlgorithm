namespace GeneticAlgorithm.Entities.IntegersImplementation
{
    using Contracts;
    using System;

    public class Individual : IIndividual<int>
    {
        public Individual() { }

        public Individual(int geneLength)
        {
            GeneLength = geneLength;
            Fitness = 0;
            Genes = new int[GeneLength];

            SetGenes();
        }

        public int[] Genes { get; set; }

        public int GeneLength { get; set; }

        public int Fitness { get; set; }

        public void SetGenes()
        {
            for (int i = 0; i < GeneLength; i++)
            {
                Genes[i] = GetGene();
            }
        }

        private int GetGene()
        {
            Random rn = new Random();
            return Math.Abs(rn.Next() % 2);
        }

        public void CalculateFitness()
        {
            Fitness = 0;
            for (int i = 0; i < GeneLength; i++)
            {
                if (Genes[i] == 1)
                {
                    //The fitness is incremented by one
                    Fitness++;
                }
            }
        }
    }
}
