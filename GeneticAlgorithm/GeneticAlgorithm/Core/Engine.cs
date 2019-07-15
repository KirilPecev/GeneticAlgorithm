﻿namespace GeneticAlgorithm.Core
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

            assembly = Assembly.GetExecutingAssembly();
            types = assembly.GetExportedTypes()
                         .Where(x => x.Name.EndsWith("Command")
                         && typeof(ICommand).IsAssignableFrom(x)
                         && !x.IsInterface
                         && !x.IsAbstract)
                         .ToArray();
        }

        public void Run()
        {
            int commandNumber = 0;
            while (true)
            {
                writer.Write(this.menu.Show());
                bool result = int.TryParse(this.reader.ReadLine(), out commandNumber);
                cleaner.Clean();
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

            object classInstance = Activator.CreateInstance(commandType, new object[] { reader, writer });

            MethodInfo method = commandType.GetMethod("Execute");
            method.Invoke(classInstance, new object[] { });
        }
    }
}
