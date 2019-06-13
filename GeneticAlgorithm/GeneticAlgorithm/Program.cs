namespace GeneticAlgorithm
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Population population = new Population(10);

            Generator generator = new Generator(population);

            generator.Generate();
        }
    }
}
