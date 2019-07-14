namespace GeneticAlgorithm.Core
{
    using Contracts;
    using System;

    public class Menu : IMenu
    {
        public string Show()
        {
            return "Menu" +
                $"{Environment.NewLine}" +
                "1. Integers implementation." +
                $"{Environment.NewLine}" +
                "2. String implementation." +
                 $"{Environment.NewLine}" +
                "Choose which implementation you want to see: ";
        }

        public string GetCommand(int commandNumber)
        {
            switch (commandNumber)
            {
                case 1:
                    return "IntegersImplementation";
                case 2:
                    return "StringImplementation";
                default:
                    return "Invalid command!";
            }
        }
    }
}
