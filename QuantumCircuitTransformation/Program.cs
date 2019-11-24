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

            int nbCircuits = 100;
            int nbGates = 20000;
            for (int nbQubits = 4; nbQubits < 20; nbQubits++)
            {
                int averageLayers = 0;
                for (int i = 0; i < nbCircuits; i++)
                {
                    averageLayers += (CircuitGenerator.RandomCircuit(nbGates, nbQubits).NbLayers);
                }
                Console.WriteLine("NbL / NbG: " + averageLayers / nbGates);
                //Console.WriteLine("Average number of layers: " + averageLayers / nbCircuits);
                //Console.WriteLine("NbGates / NbQubits: " + nbGates / nbQubits);
                //Console.WriteLine("NbLayers *  NbQubits: " + averageLayers * nbQubits);
                Console.WriteLine("----------------------------------------");
            }
            


            //Globals.Timer.Start();
            //QuantumCircuit c = CircuitGenerator.RandomCircuit(15000, 20);
            //Globals.Timer.Stop();
            //Console.WriteLine("In total are {0} milliseconds taken to generate the circuit", Globals.Timer.Elapsed.TotalMilliseconds);

            //ArchitectureGraph a = QuantumDevices.IBM_Q20;

            //Console.WriteLine(AllAlgorithms.InitialMappings[1].GetName() + '\n' + AllAlgorithms.InitialMappings[1].GetParameters() + '\n');
            //Globals.Timer.Restart();
            //AllAlgorithms.InitialMappings[1].Execute(a, c);
            //Globals.Timer.Stop();
            //Console.WriteLine("In total are {0} milliseconds taken to find an initial mapping.", Globals.Timer.Elapsed.TotalMilliseconds);

            //Console.WriteLine("\n\n");

            //Console.WriteLine(AllAlgorithms.InitialMappings[0].GetName() + '\n' + AllAlgorithms.InitialMappings[0].GetParameters() + '\n');
            //Globals.Timer.Restart();
            //AllAlgorithms.InitialMappings[0].Execute(a, c);
            //Globals.Timer.Stop();
            //Console.WriteLine("In total are {0} milliseconds taken to find an initial mapping.", Globals.Timer.Elapsed.TotalMilliseconds);



            //Timer.Start();
            //sa.Execute(a, c);
            //Timer.Stop();
            //Console.WriteLine("In total are {0} milliseconds taken", Timer.ElapsedMilliseconds);

            //Globals.Timer.Start();
            //AllAlgorithms.InitialMappings[0].Execute(a, c);
            //Globals.Timer.Stop();
            //Console.WriteLine("in total are {0} milliseconds taken", Globals.Timer.Elapsed.TotalMilliseconds);


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


        private static void GiveOverviewOfAllAlgorithms()
        {
            // TODO
        }
    }
}