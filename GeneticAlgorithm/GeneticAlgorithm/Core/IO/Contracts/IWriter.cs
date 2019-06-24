namespace GeneticAlgorithm.Core.IO.Contracts
{
    public interface IWriter
    {
        void WriteLine(string content);

        void Write(string content);
    }
}
