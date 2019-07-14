namespace GeneticAlgorithm.Core.Contracts
{
    public interface IMenu
    {
        string Show();

        string GetCommand(int commandNumber);
    }
}
