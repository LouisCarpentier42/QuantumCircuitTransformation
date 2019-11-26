using QuantumCircuitTransformation.Data;
using QuantumCircuitTransformation.InitalMappingAlgorithm;
using QuantumCircuitTransformation.QuantumCircuitComponents;
using QuantumCircuitTransformation.QuantumCircuitComponents.Architecture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace QuantumCircuitTransformation
{
    class Program
    {

        private static readonly List<Tuple<string, Action>> MainMenu = new List<Tuple<string, Action>>
        {
            new Tuple<string, Action>("Load an initial mapping algorithm", new Action(LoadInitialMappings)),
            new Tuple<string, Action>("Add a new initial mapping algorithm", new Action(AddInitialMapping)),
            new Tuple<string, Action>("Give an overview of all the available algorithms", new Action(GiveOverviewOfAllAlgorithms)),
            new Tuple<string, Action>("Give an overview of the loaded algorithms", new Action(GiveOverviewOfLoadedAlgorithms)),
            new Tuple<string, Action>("Execute the loaded initial mapping algorithm", new Action(ExecuteInitialMapping)),

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
                    else if (selection == 42) Test();
                    else MainMenu[selection-1].Item2.Invoke();
                } catch (ArgumentOutOfRangeException)
                {
                    ConsoleLayout.Error();
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




        private static void LoadInitialMappings()
        {
            ConsoleLayout.Header("Select an initial mapping algorithm");
            var orderedInitalMappings = AllAlgorithms.InitialMappings.OrderBy(item => item.GetType().FullName);
            for (int i = 0; i < orderedInitalMappings.Count(); i++)
            {
                Console.WriteLine(i+1 + ": " + orderedInitalMappings.ElementAt(i).Name());
                Console.WriteLine(orderedInitalMappings.ElementAt(i).Parameters() + '\n');
            }

            try
            {
                Console.Write("Give the ID of the initial mapping: ");
                int index = Convert.ToInt32(Console.ReadLine());
                LoadedAlgorithms.InitialMapping = orderedInitalMappings.ElementAt(index-1);
                Console.WriteLine("\nInitial mapping {0} has been loaded.", index);
            }
            catch (Exception e) when (e is ArgumentOutOfRangeException || e is FormatException)
            {
                ConsoleLayout.Error("Failed to load an initial mapping algorithm");
            }
            ConsoleLayout.Footer();
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
                ConsoleLayout.Error();
            }

            


            Console.WriteLine("TODO");

            ConsoleLayout.Footer();
        }

        private static void GiveOverviewOfLoadedAlgorithms()
        {
            ConsoleLayout.Header("Loaded algorithms");

            if (LoadedAlgorithms.InitialMapping == null)
                Console.WriteLine("There is no initial mapping algorithm loaded...");
            else
                Console.WriteLine("Initial mapping: " + LoadedAlgorithms.InitialMapping.Name() + '\n' + LoadedAlgorithms.InitialMapping.Parameters());

            Console.WriteLine();

            if (LoadedAlgorithms.Transformation == null)
                Console.WriteLine("There is no transformation algorithm loaded...");
            else
                Console.WriteLine("Transformation: " + LoadedAlgorithms.Transformation.Name() + '\n' + LoadedAlgorithms.Transformation.Parameters());

            ConsoleLayout.Footer();
        }

        private static void GiveOverviewOfAllAlgorithms()
        {
            ConsoleLayout.Header("Available algorithms");

            ConsoleLayout.SubHeader("Initial Mapping Algorithms");

            if (AllAlgorithms.InitialMappings.Count() == 0)
                Console.WriteLine("No initial mapping algorithms are available...");
            var orderedInitalMappings = AllAlgorithms.InitialMappings.OrderBy(item => item.GetType().FullName);
            for (int i = 0; i < AllAlgorithms.InitialMappings.Count(); i++)
            {
                Console.WriteLine(i + 1 + ": " + orderedInitalMappings.ElementAt(i).Name());
                Console.WriteLine(orderedInitalMappings.ElementAt(i).Parameters() + '\n');
            }

            ConsoleLayout.SubHeader("Transformation Algorithms");
            if (AllAlgorithms.Transformations.Count() == 0)
                Console.WriteLine("No transformation algorithms are available...");
            var orderedTransformation = AllAlgorithms.Transformations.OrderBy(item => item.GetType().FullName);
            for (int i = 0; i < AllAlgorithms.Transformations.Count(); i++)
            {
                Console.WriteLine(i + 1 + ": " + orderedTransformation.ElementAt(i).Name());
                Console.WriteLine(orderedTransformation.ElementAt(i).Parameters() + '\n');
            }

            ConsoleLayout.Footer();
        }

        private static void ExecuteInitialMapping()
        {
            ConsoleLayout.Header("Initial mapping");

            ArchitectureGraph a = QuantumDevices.IBM_Q20;
            QuantumCircuit c = CircuitGenerator.RandomCircuit(100, 20);

            Globals.Timer.Restart();
            LoadedAlgorithms.InitialMapping.Execute(a, c);
            Globals.Timer.Stop();
            Console.WriteLine("In total are {0} milliseconds taken to find an initial mapping.", Globals.Timer.Elapsed.TotalMilliseconds);

            ConsoleLayout.Footer();
        }


    }
}