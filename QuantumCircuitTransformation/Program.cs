using System;
using System.Collections.Generic;
using QuantumCircuitTransformation.QuantumCircuitComponents.Architecture;
using System.Linq;
using QuantumCircuitTransformation.QuantumCircuitComponents;
using QuantumCircuitTransformation.InitalMappingAlgorithm;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Reflection;

namespace QuantumCircuitTransformation
{
    class Program
    {
        
        private static readonly List<Tuple<string, Action>> MainMenu = new List<Tuple<string, Action>>
        {
            new Tuple<string, Action>("Select an initial mapping algorithm", new Action(SelectLoadedInitialMappings)),
            new Tuple<string, Action>("Add a new initial mapping algorithm", new Action(AddInitialMapping)),
        };




        static void Main(string[] args)
        {
            while (true)
            {
                ConsoleLayout.Header("Main");
                int selection = ConsoleLayout.SelectFromMenu(MainMenu);
                try
                {
                    if (selection == 0) return;
                    if (selection == 42)
                    {
                        Test();
                        continue;
                    }
                    MainMenu[selection-1].Item2.Invoke();
                } catch (ArgumentOutOfRangeException)
                {
                    ConsoleLayout.InvalidInput();
                    ConsoleLayout.Footer();
                }
            }
        } 




        private static void Test()
        {
            ConsoleLayout.Header("Test environment");


            //InitalMapping ts = new TabuSearch(50,5,1000);
            //QuantumCircuit c = CircuitGenerator.RandomCircuit(1000, 20);
            //ArchitectureGraph a = QuantumDevices.IBM_Q20;

            //Timer.Start();
            //sa.Execute(a, c);
            //Timer.Stop();
            //Console.WriteLine("In total are {0} milliseconds taken", Timer.ElapsedMilliseconds);

            //Timer.Start();
            //ts.Execute(a, c);
            //Timer.Stop();
            //Console.WriteLine("In total are {0} milliseconds taken", Timer.ElapsedMilliseconds);


            ConsoleLayout.Footer();
        }




        private static void SelectLoadedInitialMappings()
        {
            ConsoleLayout.Header("Select an initial mapping algorithm");
            var orderedInitalMappings = AllAlgorithms.InitialMappings.OrderBy(item => item.GetType().FullName);
            for (int i = 0; i < orderedInitalMappings.Count(); i++)
            {
                Console.WriteLine(i+1 + ": " + orderedInitalMappings.ElementAt(i).GetName());
                Console.WriteLine(orderedInitalMappings.ElementAt(i).GetParameters() + '\n');
            }

            try
            {
                Console.Write("Give the ID of the initial mapping: ");
                int index = Convert.ToInt32(Console.ReadLine());
                LoadedAlgorithms.InitialMapping = orderedInitalMappings.ElementAt(index-1);
                Console.WriteLine("\nInitial mapping {0} has been selected.", index);
                ConsoleLayout.Footer();
            }
            catch (Exception e) when (e is ArgumentOutOfRangeException || e is FormatException)
            {
                ConsoleLayout.InvalidInput();
                ConsoleLayout.Footer();
            }
        }


        private static void AddInitialMapping()
        {
            ConsoleLayout.Header("New initial mapping");


            var initialMappingAlgorithms = Assembly
               .GetAssembly(typeof(InitialMapping))
               .GetTypes()
               .Where(t => t.IsSubclassOf(typeof(InitialMapping)));
            Console.WriteLine("Select an inital mapping algorithm to add.");
            for (int i = 0; i < initialMappingAlgorithms.Count(); i++)
            {
                Console.WriteLine(i+1 + ": " + initialMappingAlgorithms.ElementAt(i).GetTypeInfo().Name);
            }
            Console.Write("Choice: ");

            try
            {
                int index = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();
                Type t = initialMappingAlgorithms.ElementAt(index - 1);
            }
            catch (Exception e) when (e is ArgumentOutOfRangeException || e is FormatException)
            {
                Console.WriteLine();
                ConsoleLayout.InvalidInput();
            }

            


            Console.WriteLine("TODO");

            ConsoleLayout.Footer();
        }
    }
}