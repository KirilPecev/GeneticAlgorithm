//namespace GeneticAlgorithm.Entities.StringImplementation
//{
//    using GeneticAlgorithm.Entities.Contracts;
//    using System;

//    public class Generator : IGenerator<int>
//    {
//        private readonly Population population;

//        public Generator(Population population)
//        {
//            this.population = population;
//        }

//        public Individual Fittest { get; private set; }

//        public Individual SecondFittest { get; private set; }

//        public IIndividual<int> FittestIndividual => throw new NotImplementedException();

//        public IIndividual<int> SecondFittestIndividual => throw new NotImplementedException();

//        public void Generate()
//        {
//            Random rn = new Random();

//            population.InitializePopulation();
//            population.CalculateFitness();

//            int generationCount = 0;

//            Console.WriteLine($"Generation: {generationCount} Fittest: {population.Fittest}");

//            bool found = false;

//            while (!found)
//            {
//                generationCount++;

//                Selection();

//                Crossover();

//                //Do mutation under a random probability
//                if (rn.Next() % 7 < 100)
//                {
//                    Mutation();
//                }

//                AddFittestOffspring();

//                population.CalculateFitness();

//                //if (Fittest.Genes == Fittest.Choromosome)
//                //{
//                //    found = true;
//                //}

//                Console.WriteLine($"Generation: {generationCount} Fittest: {population.Fittest} Genes: {Fittest.Genes}");
//            }

//            Console.WriteLine($"Solution found in generation {generationCount}");
//            Console.WriteLine($"Fitness: {population.GetFittest().Fitness}");
//            Console.WriteLine("Genes: ");

//            for (int i = 0; i < population.Chromosome.Length; i++)
//            {
//                Console.Write(Fittest.Genes[i]);
//            }
//        }

//        private void AddFittestOffspring()
//        {
//            //Update fitness values of offspring
//            Fittest.CalculateFitness();
//            SecondFittest.CalculateFitness();

//            //Get index of least fit individual
//            int leastFittestIndex = population.GetLeastFittestIndex();

//            //Replace least fittest individual from most fittest offspring
//            population.Individuals[leastFittestIndex] = GetFittestOffspring();
//        }

//        private Individual GetFittestOffspring()
//        {
//            if (Fittest.Fitness > SecondFittest.Fitness)
//            {
//                return Fittest;
//            }

//            return SecondFittest;
//        }

//        private void Selection()
//        {
//            Fittest = population.GetFittest();
//            SecondFittest = population.GetSecondFittest();
//        }

//        private void Crossover()
//        {
//            Random rn = new Random();

//            //Select a random crossover point
//            int crossOverPoint = rn.Next(population.Individuals[0].Choromosome.Length);

//            //Swap values among parents
//            char[] fittestGenes = Fittest.Genes.ToCharArray();
//            char[] secondFittestGenes = SecondFittest.Genes.ToCharArray();

//            for (int i = 0; i < crossOverPoint; i++)
//            {
//                char temp = Fittest.Genes[i];
//                fittestGenes[i] = SecondFittest.Genes[i];
//                secondFittestGenes[i] = temp;
//            }

//            Fittest.Genes = string.Join("", fittestGenes);
//            SecondFittest.Genes = string.Join("", secondFittestGenes);
//        }

//        private void Mutation()
//        {
//            Random rn = new Random();

//            //Select a random mutation point
//            int mutationPoint = rn.Next(population.Individuals[0].Genes.Length);

//            //Flip values at the mutation point
//            char[] fittestGenes = Fittest.Genes.ToCharArray();
//            if (fittestGenes[mutationPoint] != Fittest.Choromosome[mutationPoint])
//            {
//                fittestGenes[mutationPoint] = Fittest.Choromosome[mutationPoint];
//            }
//            else
//            {
//                fittestGenes[mutationPoint] = '#';
//            }

//            Fittest.Genes = string.Join("", fittestGenes);

//            mutationPoint = rn.Next(population.Individuals[0].Genes.Length);

//            char[] secondFittestGenes = SecondFittest.Genes.ToCharArray();
//            if (secondFittestGenes[mutationPoint] != SecondFittest.Choromosome[mutationPoint])
//            {
//                secondFittestGenes[mutationPoint] = SecondFittest.Choromosome[mutationPoint];
//            }
//            else
//            {
//                secondFittestGenes[mutationPoint] = '#';
//            }

//            SecondFittest.Genes = string.Join("", secondFittestGenes);
//        }

//        void IGenerator<int>.Mutation()
//        {
//            throw new NotImplementedException();
//        }

//        void IGenerator<int>.Crossover()
//        {
//            throw new NotImplementedException();
//        }

//        void IGenerator<int>.Selection()
//        {
//            throw new NotImplementedException();
//        }

//        public IIndividual<int> GetFittestFromOffspring()
//        {
//            throw new NotImplementedException();
//        }

//        public void ReplaceLeastFittestFromOffspring()
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
