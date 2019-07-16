namespace GeneticAlgorithm.Core
{
    using Contracts;
    using IO.Contracts;
    using System;
    using System.Linq;
    using System.Reflection;

    public class Engine : IEngine
    {
        private const string CommandSuffix = "Command";
        private const string CommandMethod = "Execute";

        private readonly IMenu menu;
        private readonly IReader reader;
        private readonly IWriter writer;
        private readonly ICleaner cleaner;
        private readonly Assembly assembly;
        private readonly Type[] types;

        public Engine(IMenu menu, IReader reader, IWriter writer, ICleaner cleaner)
        {
            this.menu = menu;
            this.reader = reader;
            this.writer = writer;
            this.cleaner = cleaner;

            assembly = Assembly.GetExecutingAssembly();
            types = assembly.GetExportedTypes()
                         .Where(x => x.Name.EndsWith(CommandSuffix)
                         && typeof(ICommand).IsAssignableFrom(x)
                         && !x.IsInterface
                         && !x.IsAbstract)
                         .ToArray();
        }

        public void Run()
        {
            while (true)
            {
                int commandNumber = 0;
                bool result = ReadCommand(out commandNumber);
                if (result)
                {
                    try
                    {
                        ExecuteCommand(commandNumber);
                        break;
                    }
                    catch (Exception) { }
                }
            }
        }

        private bool ReadCommand(out int commandNumber)
        {
            writer.Write(this.menu.Show());
            bool result = int.TryParse(this.reader.ReadLine(), out commandNumber);
            cleaner.Clean();
            return result;
        }

        private void ExecuteCommand(int command)
        {
            string commandName = this.menu.GetCommand(command).ToLower();

            Type commandType = types
                .FirstOrDefault(c => c.Name
                                .ToString()
                                .ToLower()
                                .StartsWith(commandName));

            object classInstance = Activator.CreateInstance(commandType, new object[] { reader, writer });

            MethodInfo method = commandType.GetMethod(CommandMethod);
            method.Invoke(classInstance, new object[] { });
        }
    }
}
