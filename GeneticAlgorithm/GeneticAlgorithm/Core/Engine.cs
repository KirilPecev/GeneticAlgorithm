namespace GeneticAlgorithm.Core
{
    using Contracts;
    using IO.Contracts;
    using System;
    using System.Linq;
    using System.Reflection;

    public class Engine : IEngine
    {
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
            this.assembly = Assembly.GetExecutingAssembly();
            this.types = assembly.GetExportedTypes()
                         .Where(x => x.Name.EndsWith("Command"))
                         .ToArray();
        }

        public void Run()
        {
            int commandNumber = 0;
            while (true)
            {
                this.writer.Write(this.menu.Show());
                bool result = int.TryParse(this.reader.ReadLine(), out commandNumber);
                this.cleaner.Clean();
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

        private void ExecuteCommand(int command)
        {
            string commandName = this.menu.GetCommand(command).ToLower();

            Type commandType = types
                .FirstOrDefault(c => c.Name
                                .ToString()
                                .ToLower()
                                .StartsWith(commandName));

            object classInstance = Activator.CreateInstance(commandType, new object[] { this.reader, this.writer });

            MethodInfo method = commandType.GetMethod("Execute");
            method.Invoke(classInstance, new object[] { });
        }
    }
}
