using QuantumCircuitTransformation.MappingPerturbation;
using QuantumCircuitTransformation.Data;
using QuantumCircuitTransformation.Algorithms.InitialMappingAlgorithm;
using QuantumCircuitTransformation.QuantumCircuitComponents.Circuit;
using QuantumCircuitTransformation.QuantumCircuitComponents.ArchitectureGraph;
using QuantumCircuitTransformation.Algorithms.TransformationAlgorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using QuantumCircuitTransformation.QuantumCircuitComponents.Gates;
using System.Text.RegularExpressions;
using QuantumCircuitTransformation.DependencyGraphs;

namespace QuantumCircuitTransformation
{
    class Program
    {

        private static readonly List<Tuple<string, Action>> MainMenu = new List<Tuple<string, Action>>
        {
            new Tuple<string, Action>("Give an overview of the loaded algorithms",
                new Action(GiveOverviewOfLoadedAlgorithms)),
            new Tuple<string, Action>("Give an overview of the available initial mapping algorithms",
                new Action(GiveOverviewOfInitialMappingAlgorithms)),
            new Tuple<string, Action>("Give an overview of the available transformation algorithms",
                new Action(GiveOverviewOfTransformationAlgorithms)),
            new Tuple<string, Action>("Give an overview of all the available algorithms",
                new Action(GiveOverviewOfAllAlgorithms)),
            new Tuple<string, Action>("Load an initial mapping algorithm", 
                new Action(LoadInitialMappingAlgorithm)),
            new Tuple<string, Action>("(TODO)Load a transformation algorithm",
                new Action(LoadTransformationAlgorithm)),
            new Tuple<string, Action>("Add a new initial mapping algorithm", 
                new Action(AddInitialMappingAlgorithm)),
            new Tuple<string, Action>("(TODO)Add a new transformation algorithm",
                new Action(AddTransformationAlgorithm)),
            new Tuple<string, Action>("Test the available initial mapping algorithms",
                new Action(TestAvailableInitialMappings)),
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

            //LogicalCircuit circuit = CircuitGenerator.ReadFromFile("test.txt");
            //Console.WriteLine(circuit);

            List<PhysicalGate> gates = new List<PhysicalGate>
            {
                new CNOT(0,1),
                new CNOT(2,3),
                new CNOT(1,2),
                SingleQubitGate.GetRotationalGate(1,'z',0.25),
                new CNOT(1,0)
            };
            LogicalCircuit circuit = new LogicalCircuit(gates);

            DependencyGraph graph = DependencyGraphGenerator.Generate(circuit, AlgorithmParameters.DependencyRules);

            foreach (var x in graph.DependencyEdges) Console.WriteLine(x);



            //LogicalCircuit circuit = CircuitGenerator.RandomCircuit(10000, 20);

            //DependencyGraph graph = DependencyGraphGenerator.Generate(circuit);
            //Console.WriteLine(graph.DependencyEdges.Count);

            //DependencyGraph graphRules = DependencyGraphGenerator.Generate(circuit, rules);
            //Console.WriteLine(graphRules.DependencyEdges.Count);

            ConsoleLayout.Footer();
        }



        /*****************************************************************
         * LOAD ALGORITHMS
         *****************************************************************/

        private static void LoadInitialMappingAlgorithm()
        {
            ConsoleLayout.Header("Select an initial mapping algorithm");

            var orderedInitalMappings = AlgorithmParameters.AvailableInitialMappings.OrderBy(item => item.GetType().FullName);
            for (int i = 0; i < orderedInitalMappings.Count(); i++)
            {
                Console.WriteLine(i+1 + ": " + orderedInitalMappings.ElementAt(i).Name());
                Console.WriteLine(orderedInitalMappings.ElementAt(i).Parameters() + '\n');
            }

            try
            {
                Console.Write("Give the ID of the initial mapping: ");
                int index = Convert.ToInt32(Console.ReadLine());
                AlgorithmParameters.InitialMapping = orderedInitalMappings.ElementAt(index-1);
                Console.WriteLine("\nInitial mapping {0} has been loaded.", index);
            }
            catch (Exception e) when (e is ArgumentOutOfRangeException || e is FormatException)
            {
                ConsoleLayout.Error("Failed to load an initial mapping algorithm");
            }

            ConsoleLayout.Footer();
        }

        private static void LoadTransformationAlgorithm()
        {
            ConsoleLayout.Header("Select a transformation algorithm");

            Console.WriteLine("TODO"); // Remove TODO from menu

            ConsoleLayout.Footer();
        }


        /*****************************************************************
         * ADD NEW ALGORITHMS
         *****************************************************************/

        private static void AddInitialMappingAlgorithm()
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
                Type type = initialMappingAlgorithms.ElementAt(index - 1);
                ConstructorInfo[] constructors = type.GetConstructors().ToList().FindAll(c => c.IsPublic).ToArray();
                ConstructorInfo constructorInfo;
                if (constructors.Count() == 1)
                {
                    constructorInfo = constructors[0];
                }
                else
                {
                    Console.WriteLine();
                    for (int i = 0; i < constructors.Count(); i++)
                    {
                        Console.WriteLine(i + 1 + ": " + type.Name + " " + constructors[i].ToString().Substring(10));
                    }
                    Console.Write("Choice: ");
                    int index2 = Convert.ToInt32(Console.ReadLine());
                    constructorInfo = constructors[index2-1];
                }

                Console.WriteLine();
                object[] param = new object[constructorInfo.GetParameters().Count()];
                for (int i = 0; i < constructorInfo.GetParameters().Count(); i++)
                {
                    Console.Write("> " + constructorInfo.GetParameters()[i].Name + ": ");
                    Type paramType = constructorInfo.GetParameters()[i].ParameterType;
                    param[i] = Convert.ChangeType(Console.ReadLine(),paramType);
                }
                InitialMapping initialMapping = (InitialMapping)constructorInfo.Invoke(param);
                if (!AlgorithmParameters.AvailableInitialMappings.Contains(initialMapping))
                    AlgorithmParameters.AvailableInitialMappings.Add(initialMapping);

            }
            catch (Exception e) when (e is IndexOutOfRangeException || e is ArgumentOutOfRangeException || e is FormatException)
            {
                Console.WriteLine();
                ConsoleLayout.Error();
            }

            ConsoleLayout.Footer();
        }

        private static void AddTransformationAlgorithm()
        {
            ConsoleLayout.Header("Add a transformation algorithm");

            Console.WriteLine("TODO"); // remove TODO from menu

            ConsoleLayout.Footer();
        }


        /*****************************************************************
         * OVERVIEW OF ALGORITHMS
         *****************************************************************/

        private static void GiveOverviewOfLoadedAlgorithms()
        {
            ConsoleLayout.Header("Loaded algorithms");

            if (AlgorithmParameters.InitialMapping == null)
                Console.WriteLine("There is no initial mapping algorithm loaded...");
            else
                Console.WriteLine("Initial mapping: " + AlgorithmParameters.InitialMapping.Name() + '\n' + AlgorithmParameters.InitialMapping.Parameters());

            Console.WriteLine();

            if (AlgorithmParameters.Transformation == null)
                Console.WriteLine("There is no transformation algorithm loaded...");
            else
                Console.WriteLine("Transformation: " + AlgorithmParameters.Transformation.Name() + '\n' + AlgorithmParameters.Transformation.Parameters());

            ConsoleLayout.Footer();
        }

        private static void GiveOverviewOfAllAlgorithms()
        {
            ConsoleLayout.Header("Available algorithms");

            ConsoleLayout.SubHeader("Initial Mapping Algorithms");
            if (AlgorithmParameters.AvailableInitialMappings.Count() == 0)
                Console.WriteLine("No initial mapping algorithms are available...");
            var orderedInitalMappings = AlgorithmParameters.AvailableInitialMappings.OrderBy(item => item.GetType().FullName);
            for (int i = 0; i < AlgorithmParameters.AvailableInitialMappings.Count(); i++)
            {
                Console.WriteLine(i + 1 + ": " + orderedInitalMappings.ElementAt(i).Name());
                Console.WriteLine(orderedInitalMappings.ElementAt(i).Parameters() + '\n');
            }

            ConsoleLayout.SubHeader("Transformation Algorithms");
            if (AlgorithmParameters.AvailableTransformationAlgorithms.Count() == 0)
                Console.WriteLine("No transformation algorithms are available...");
            var orderedTransformation = AlgorithmParameters.AvailableTransformationAlgorithms.OrderBy(item => item.GetType().FullName);
            for (int i = 0; i < AlgorithmParameters.AvailableTransformationAlgorithms.Count(); i++)
            {
                Console.WriteLine(i + 1 + ": " + orderedTransformation.ElementAt(i).Name());
                Console.WriteLine(orderedTransformation.ElementAt(i).Parameters() + '\n');
            }

            ConsoleLayout.Footer();
        }

        private static void GiveOverviewOfInitialMappingAlgorithms()
        {
            ConsoleLayout.Header("Available initial mapping algorithms");

            if (AlgorithmParameters.AvailableInitialMappings.Count() == 0)
                Console.WriteLine("No initial mapping algorithms are available...");
            var orderedInitalMappings = AlgorithmParameters.AvailableInitialMappings.OrderBy(item => item.GetType().FullName);
            for (int i = 0; i < AlgorithmParameters.AvailableInitialMappings.Count(); i++)
            {
                Console.WriteLine(i + 1 + ": " + orderedInitalMappings.ElementAt(i).Name());
                Console.WriteLine(orderedInitalMappings.ElementAt(i).Parameters() + '\n');
            }

            ConsoleLayout.Footer();
        }

        private static void GiveOverviewOfTransformationAlgorithms()
        {
            ConsoleLayout.Header("Available transformation algorithms");

            if (AlgorithmParameters.AvailableTransformationAlgorithms.Count() == 0)
                Console.WriteLine("No transformation algorithms are available...");
            var orderedTransformation = AlgorithmParameters.AvailableTransformationAlgorithms.OrderBy(item => item.GetType().FullName);
            for (int i = 0; i < AlgorithmParameters.AvailableTransformationAlgorithms.Count(); i++)
            {
                Console.WriteLine(i + 1 + ": " + orderedTransformation.ElementAt(i).Name());
                Console.WriteLine(orderedTransformation.ElementAt(i).Parameters() + '\n');
            }

            ConsoleLayout.Footer();
        }



        private static void TestAvailableInitialMappings()
        {
            ConsoleLayout.Header("Initial mapping test");

            int nbRep = 10;
            int nbQubits = 20;
            int nbGates = 1000000;
            Architecture architecture = QuantumDevices.IBM_Q20;
            
            double[] totalCost = new double[AlgorithmParameters.AvailableInitialMappings.Count()];
            double[] titotalTime = new double[AlgorithmParameters.AvailableInitialMappings.Count()];

            for (int i = 0; i < nbRep; i++)
            {
                Console.WriteLine("Iteration " + (i + 1));
                LogicalCircuit circuit = CircuitGenerator.RandomCircuit(nbGates, nbQubits);
                for (int j = 0; j < AlgorithmParameters.AvailableInitialMappings.Count(); j++)
                {
                    Globals.Timer.Restart();
                    (_, double cost) = AlgorithmParameters.AvailableInitialMappings[j].Execute(architecture, circuit);
                    Globals.Timer.Stop();
                    totalCost[j] += cost;
                    titotalTime[j] += Globals.Timer.Elapsed.TotalMilliseconds;
                    Console.WriteLine("cost = {1} - time = {2} ({0})", AlgorithmParameters.AvailableInitialMappings[j].Name(), cost, Globals.Timer.Elapsed.TotalMilliseconds);
                }
            }

            for (int i = 0; i < AlgorithmParameters.AvailableInitialMappings.Count(); i++)
            {
                Console.WriteLine();
                Console.WriteLine(AlgorithmParameters.AvailableInitialMappings[i].Name());
                Console.WriteLine(AlgorithmParameters.AvailableInitialMappings[i].Parameters());
                Console.WriteLine("Result: Cost = {0} - time = {1}", totalCost[i] / nbRep, titotalTime[i] / nbRep);
            }

            ConsoleLayout.Footer();
        }

    }
}