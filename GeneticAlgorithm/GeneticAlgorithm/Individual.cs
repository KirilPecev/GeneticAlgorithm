namespace GeneticAlgorithm
{
    using System;

    public class Individual
    {
        public Individual()
        {
            this.GeneLength = 5;
            this.Fitness = 0;
            this.Genes = new int[GeneLength];

            this.SetGenesRandomly();
        }

        public int[] Genes { get; private set; }

        public int GeneLength { get; set; }

        public int Fitness { get; private set; }

        private void SetGenesRandomly()
        {
            Random rn = new Random();

            for (int i = 0; i < GeneLength; i++)
            {
                this.Genes[i] = Math.Abs(rn.Next() % 2);
            }

            this.Fitness = 0;
        }

        public void CalculateFitness()
        {
            this.Fitness = 0;
            for (int i = 0; i < 5; i++)
            {
                if (this.Genes[i] == 1)
                {
                    this.Fitness++;
                }
            }
        }
    }
}
