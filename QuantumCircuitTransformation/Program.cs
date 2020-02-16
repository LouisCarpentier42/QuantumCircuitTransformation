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
using System.IO;

namespace QuantumCircuitTransformation
{
    class Program
    {

        private static readonly List<Tuple<string, Action>> MainMenu = new List<Tuple<string, Action>>
        {
            // Overview of the algorithms
            new Tuple<string, Action>("Give an overview of the loaded algorithms and rules",
                new Action(GiveOverviewOfLoadedAlgorithm)),
            new Tuple<string, Action>("Give an overview of all the available algorithms and rules\n",
                new Action(GiveOverviewOfAvailableAlgorithms)),

            // Initial mapping algorithms
            new Tuple<string, Action>("Load an initial mapping algorithm", 
                new Action(LoadInitialMappingAlgorithm)),
            new Tuple<string, Action>("Add a new initial mapping algorithm\n",
                new Action(AddInitialMappingAlgorithm)),

            // Transformation algorithms
            new Tuple<string, Action>("Load a transformation algorithm",
                new Action(LoadTransformationAlgorithm)),
            new Tuple<string, Action>("Add a new transformation algorithm\n",
                new Action(AddTransformationAlgorithm)),
            
            // Dependency rules
            new Tuple<string, Action>("Give an overview of the loaded dependency rules",
                new Action(GiveOverviewOfLoadedDependencyRules)),
            new Tuple<string, Action>("Give an overview of the available dependency rules",
                new Action(GiveOverviewOfAvailableDependencyRules)),
            new Tuple<string, Action>("Load a dependency rule",
                new Action(LoadDependencyRule)),
            new Tuple<string, Action>("Unload a dependency rule",
                new Action(UnloadDependencyRule)),
            new Tuple<string, Action>("Add a new dependency rule\n",
                new Action(AddDependencyRule)),

            // Benchmarks
            new Tuple<string, Action>("Give an overview of the loaded benchmarks",
                new Action(GiveOverviewOfLoadedBenchmarks)),
            new Tuple<string, Action>("Give an overview of the available benchmarks",
                new Action(GiveOverviewOfAvailableBenchmarks)),
            new Tuple<string, Action>("Load all the available benchmarks\n",
                new Action(LoadAllAvailableBenchmarks)),
            // TODO loading/unloading

            // Testing 
            new Tuple<string, Action>("Test the available initial mapping algorithms\n",
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
                } catch (InvalidCastException)
                {
                    ConsoleLayout.Error();
                    ConsoleLayout.Footer();
                }
            }
        } 


        private static void Test()
        {
            ConsoleLayout.Header("Test environment");


            string fileName = "hwb7_59";

            Globals.Timer.Restart();
            LogicalCircuit circuit = CircuitGenerator.ReadFromFile(fileName + ".qasm");
            Globals.Timer.Stop();
            Console.WriteLine("Gate imported with {0} gates in {1} milliseconds.", 
                circuit.NbGates, Globals.Timer.Elapsed.TotalMilliseconds);

            Globals.Timer.Restart();
            DependencyGraph g = DependencyGraphGenerator.Generate(circuit);
            Globals.Timer.Stop();
            Console.WriteLine("Dependency graph generated in {0} milliseconds.",
                Globals.Timer.Elapsed.TotalMilliseconds);

            //int nbDependencies = 0;
            //for (int i = 0; i < g.ExecuteBefore.Count(); i++)
            //{
            //    nbDependencies += g.ExecuteBefore[i].Count();
            //    try
            //    {
            //        Console.Write(i + ": ");
            //        Console.Write(g.ExecuteBefore[i][0]);
            //        for (int j = 1; j < g.ExecuteBefore[i].Count; j++)
            //        {
            //            Console.Write(", " + g.ExecuteBefore[i][j]);
            //        }
            //    }
            //    catch { }
            //    Console.Write('\n');
            //}
            //Console.WriteLine("Dependency graph with {0} dependencies is genereated in {1} milliseconds",
            //    nbDependencies, Globals.Timer.Elapsed.TotalMilliseconds);



            ConsoleLayout.Footer();
        }




        /*****************************************************************
         * OVERVIEW OF ALGORITHMS
         *****************************************************************/

        /// <summary>
        /// Give an overview of the algorithms which are loaded. This is 
        /// one initial mapping and one transformation algorithm, if any
        /// is loaded. 
        /// </summary>
        private static void GiveOverviewOfLoadedAlgorithm()
        {
            ConsoleLayout.Header("Loaded algorithms");


            ConsoleLayout.SubHeader("Initial Mapping Algorithm");
            if (AlgorithmParameters.InitialMapping == null)
                Console.WriteLine("There is no initial mapping algorithm loaded...");
            else
                Console.WriteLine(AlgorithmParameters.InitialMapping.Name() + '\n' + AlgorithmParameters.InitialMapping.Parameters());


            ConsoleLayout.SubHeader("Transformation Algorithm");
            if (AlgorithmParameters.Transformation == null)
                Console.WriteLine("There is no transformation algorithm loaded...");
            else
                Console.WriteLine(AlgorithmParameters.Transformation.Name() + '\n' + AlgorithmParameters.Transformation.Parameters());

            ConsoleLayout.Footer();
        }

        /// <summary>
        /// Give an overview of all algorithms which could be loaded. 
        /// </summary>
        private static void GiveOverviewOfAvailableAlgorithms()
        {
            ConsoleLayout.Header("Available algorithms");

            ConsoleLayout.SubHeader("Initial Mapping Algorithms");
            if (AlgorithmParameters.AvailableInitialMappings.Count() == 0)
                Console.WriteLine("No initial mapping algorithms are available...");
            else
            {
                var orderedInitalMappings = AlgorithmParameters.AvailableInitialMappings.OrderBy(item => item.GetType().FullName);
                Console.WriteLine('\n' + 1 + ": " + orderedInitalMappings.ElementAt(0).Name());
                Console.WriteLine(orderedInitalMappings.ElementAt(0).Parameters());
                for (int i = 1; i < AlgorithmParameters.AvailableInitialMappings.Count(); i++)
                {
                    Console.WriteLine('\n' + (i + 1) + ": " + orderedInitalMappings.ElementAt(i).Name());
                    Console.WriteLine(orderedInitalMappings.ElementAt(i).Parameters());
                }
            }

            ConsoleLayout.SubHeader("Transformation Algorithms");
            if (AlgorithmParameters.AvailableTransformationAlgorithms.Count() == 0)
                Console.WriteLine("No transformation algorithms are available...");
            else
            {
                var orderedTransformation = AlgorithmParameters.AvailableTransformationAlgorithms.OrderBy(item => item.GetType().FullName);
                Console.WriteLine(1 + ": " + orderedTransformation.ElementAt(0).Name());
                Console.WriteLine(orderedTransformation.ElementAt(0).Parameters() + '\n');
                for (int i = 1; i < AlgorithmParameters.AvailableTransformationAlgorithms.Count(); i++)
                {
                    Console.WriteLine('\n' + (i + 1) + ": " + orderedTransformation.ElementAt(i).Name());
                    Console.WriteLine(orderedTransformation.ElementAt(i).Parameters());
                }
            }

            ConsoleLayout.Footer();
        }


        /*****************************************************************
         * INITIAL MAPPING ALGORITHMS
         *****************************************************************/

        /// <summary>
        /// Load an initial mapping algorithm.
        /// </summary>
        private static void LoadInitialMappingAlgorithm()
        {
            ConsoleLayout.Header("Select an initial mapping algorithm");

            if (AlgorithmParameters.AvailableInitialMappings.Count == 0)
                Console.WriteLine("There are no available initial mapping algorithms...");
            else
            {
                var orderedInitalMappings = AlgorithmParameters.AvailableInitialMappings.OrderBy(item => item.GetType().FullName);
                for (int i = 0; i < orderedInitalMappings.Count(); i++)
                {
                    Console.WriteLine(i + 1 + ": " + orderedInitalMappings.ElementAt(i).Name());
                    Console.WriteLine(orderedInitalMappings.ElementAt(i).Parameters() + '\n');
                }

                try
                {
                    Console.Write("Give the ID of the initial mapping to load: ");
                    int index = Convert.ToInt32(Console.ReadLine());
                    AlgorithmParameters.InitialMapping = orderedInitalMappings.ElementAt(index - 1);
                    Console.WriteLine("\nInitial mapping {0} has been loaded.", index);
                }
                catch (Exception e) when (e is ArgumentOutOfRangeException || e is FormatException)
                {
                    ConsoleLayout.Error("Failed to load an initial mapping algorithm");
                }
            }
            
            ConsoleLayout.Footer();
        }

        /// <summary>
        /// Add a new initial mapping algorithm. 
        /// </summary>
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
                Console.WriteLine(i + 1 + ": " + initialMappingAlgorithms.ElementAt(i).GetTypeInfo().Name);
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
                    constructorInfo = constructors[index2 - 1];
                }

                Console.WriteLine();
                object[] param = new object[constructorInfo.GetParameters().Count()];
                for (int i = 0; i < constructorInfo.GetParameters().Count(); i++)
                {
                    Console.Write("> " + constructorInfo.GetParameters()[i].Name + ": ");
                    Type paramType = constructorInfo.GetParameters()[i].ParameterType;
                    param[i] = Convert.ChangeType(Console.ReadLine(), paramType);
                }
                InitialMapping initialMapping = (InitialMapping)constructorInfo.Invoke(param);
                if (!AlgorithmParameters.AvailableInitialMappings.Contains(initialMapping))
                    AlgorithmParameters.AvailableInitialMappings.Add(initialMapping);

                Console.WriteLine("The initial mapping algorithm is now available.");

            }
            catch (Exception e) when (e is IndexOutOfRangeException || e is ArgumentOutOfRangeException || e is FormatException)
            {
                Console.WriteLine();
                ConsoleLayout.Error();
            }

            ConsoleLayout.Footer();
        }


        /*****************************************************************
         * TRANSFORMATION ALGORITHMS
         *****************************************************************/

        /// <summary>
        /// Load a transformation algorithm. 
        /// </summary>
        private static void LoadTransformationAlgorithm()
        {
            ConsoleLayout.Header("Select a transformation algorithm");

            if (AlgorithmParameters.AvailableTransformationAlgorithms.Count == 0)
                Console.WriteLine("There are no available transformation algorithms...");
            else
            {
                var orderedTransformation = AlgorithmParameters.AvailableTransformationAlgorithms.OrderBy(item => item.GetType().FullName);
                for (int i = 0; i < orderedTransformation.Count(); i++)
                {
                    Console.WriteLine(i + 1 + ": " + orderedTransformation.ElementAt(i).Name());
                    Console.WriteLine(orderedTransformation.ElementAt(i).Parameters() + '\n');
                }

                try
                {
                    Console.Write("Give the ID of the transformation algorithm to load: ");
                    int index = Convert.ToInt32(Console.ReadLine());
                    AlgorithmParameters.Transformation = orderedTransformation.ElementAt(index - 1);
                    Console.WriteLine("\nTransformation algorithm {0} has been loaded.", index);
                }
                catch (Exception e) when (e is ArgumentOutOfRangeException || e is FormatException)
                {
                    ConsoleLayout.Error("Failed to load a transformation algorithm");
                }
            }

            ConsoleLayout.Footer();
        }

        /// <summary>
        /// Add a new transformation algorithm. 
        /// </summary>
        private static void AddTransformationAlgorithm()
        {
            ConsoleLayout.Header("Add a transformation algorithm");

            var TransformationAlgorithms = Assembly
               .GetAssembly(typeof(InitialMapping))
               .GetTypes()
               .Where(t => t.IsSubclassOf(typeof(Transformation)));
            Console.WriteLine("Select a transformation algorithm to add.");
            for (int i = 0; i < TransformationAlgorithms.Count(); i++)
            {
                Console.WriteLine(i + 1 + ": " + TransformationAlgorithms.ElementAt(i).GetTypeInfo().Name);
            }
            Console.Write("Choice: ");

            try
            {
                int index = Convert.ToInt32(Console.ReadLine());
                Type type = TransformationAlgorithms.ElementAt(index - 1);
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
                    constructorInfo = constructors[index2 - 1];
                }

                Console.WriteLine();
                object[] param = new object[constructorInfo.GetParameters().Count()];
                for (int i = 0; i < constructorInfo.GetParameters().Count(); i++)
                {
                    Console.Write("> " + constructorInfo.GetParameters()[i].Name + ": ");
                    Type paramType = constructorInfo.GetParameters()[i].ParameterType;
                    param[i] = Convert.ChangeType(Console.ReadLine(), paramType);
                }
                Transformation Transformation = (Transformation)constructorInfo.Invoke(param);
                if (!AlgorithmParameters.AvailableTransformationAlgorithms.Contains(Transformation))
                    AlgorithmParameters.AvailableTransformationAlgorithms.Add(Transformation);

                Console.WriteLine("The transformation algorithm is now available.");

            }
            catch (Exception e) when (e is IndexOutOfRangeException || e is ArgumentOutOfRangeException || e is FormatException)
            {
                Console.WriteLine();
                ConsoleLayout.Error();
            }

            ConsoleLayout.Footer();
        }


        /*****************************************************************
         * DEPENDENCY RULES
         *****************************************************************/

        /// <summary>
        /// Give an overview of the loaded rules. 
        /// </summary>
        private static void GiveOverviewOfLoadedDependencyRules()
        {
            ConsoleLayout.Header("Loaded Dependency Rules");

            if (AlgorithmParameters.DependencyRules.Count == 0)
                Console.WriteLine("There are no dependency rules loaded...");
            else
            {
                for (int i = 0; i < AlgorithmParameters.DependencyRules.Count; i++)
                    Console.WriteLine(i + 1 + ": " + AlgorithmParameters.DependencyRules[i]);
            }

            ConsoleLayout.Footer();
        }

        /// <summary>
        /// Give an overview of the available rules. 
        /// </summary>
        private static void GiveOverviewOfAvailableDependencyRules()
        {
            ConsoleLayout.Header("Available Dependency Rules");

            if (AlgorithmParameters.AvailableDependencyRules.Count == 0)
                Console.WriteLine("There are no dependency rules available...");
            else
            {
                for (int i = 0; i < AlgorithmParameters.AvailableDependencyRules.Count; i++)
                    Console.WriteLine(i + 1 + ": " + AlgorithmParameters.AvailableDependencyRules[i]);
            }

            ConsoleLayout.Footer();
        }

        /// <summary>
        /// Load a dependency rule. 
        /// </summary>
        private static void LoadDependencyRule()
        {
            ConsoleLayout.Header("Select a dependency rule");

            if (AlgorithmParameters.AvailableDependencyRules.Count == 0)
                Console.WriteLine("There are no dependency rules available...");
            else
            {
                for (int i = 0; i < AlgorithmParameters.AvailableDependencyRules.Count; i++)
                    Console.WriteLine(i + 1 + ": " + AlgorithmParameters.AvailableDependencyRules[i]);

                try
                {
                    Console.Write("\nGive the ID of the dependency rule to load: ");
                    int index = Convert.ToInt32(Console.ReadLine());
                    if (AlgorithmParameters.DependencyRules.Contains(AlgorithmParameters.AvailableDependencyRules[index - 1]))
                        Console.WriteLine("This rule is already loaded.");
                    else
                    {
                        Console.WriteLine("\nThe '{0}'-rule has been loaded.", AlgorithmParameters.AvailableDependencyRules[index - 1]);
                        AlgorithmParameters.DependencyRules.Add(AlgorithmParameters.AvailableDependencyRules[index-1]);
                    }
                }
                catch (Exception e) when (e is ArgumentOutOfRangeException || e is FormatException)
                {
                    ConsoleLayout.Error("Failed to load a dependency rule");
                }
            }

            ConsoleLayout.Footer();
        }

        /// <summary>
        /// Unload a dependency rule. 
        /// </summary>
        private static void UnloadDependencyRule()
        {
            ConsoleLayout.Header("Select a dependency rule");

            if (AlgorithmParameters.DependencyRules.Count == 0)
                Console.WriteLine("There are no dependency rules loaded...");
            else
            {
                for (int i = 0; i < AlgorithmParameters.DependencyRules.Count; i++)
                {
                    Console.WriteLine(i + 1 + ": " + AlgorithmParameters.DependencyRules[i]);
                }

                try
                {
                    Console.Write("\nGive the ID of the dependency rule to unload: ");
                    int index = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("\nThe '{0}'-rule has been unloaded.", AlgorithmParameters.DependencyRules[index-1]);
                    AlgorithmParameters.DependencyRules.RemoveAt(index - 1);
                }
                catch (Exception e) when (e is ArgumentOutOfRangeException || e is FormatException)
                {
                    ConsoleLayout.Error("Failed to unload a dependency rule");
                }
            }

            ConsoleLayout.Footer();
        }

        /// <summary>
        /// Add a new dependency rule. 
        /// </summary>
        private static void AddDependencyRule()
        {
            ConsoleLayout.Header("Add a dependency rule");

            try
            {
                Console.Write("The number of gateparts that are involved: ");
                int nbGateParts = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();
                List<GatePart> gateParts = new List<GatePart>();
                for (int i = 0; i < nbGateParts; i++)
                {
                    Console.Write("Gatepart " + (i + 1) + ": ");
                    string gatePartName = Console.ReadLine();
                    gateParts.Add((GatePart)Enum.Parse(typeof(GatePart), gatePartName));
                    if (gatePartName == "None")
                        throw new ArgumentException();
                }
                DependencyRule rule = new DependencyRule(gateParts);
                AlgorithmParameters.AvailableDependencyRules.Add(rule);
                Console.WriteLine("\nThe dependency rule '{0}' has been added.", rule);
            } catch (Exception e) when (e is ArgumentOutOfRangeException || e is FormatException)
            {
                ConsoleLayout.Error("Failed to add a new dependency rule");
            } catch (ArgumentException)
            {
                ConsoleLayout.Error("This is no valid gatepart, the addition of a dependency rule is aborted.");
            }

            ConsoleLayout.Footer();
        }



        /*****************************************************************
         * BENCHMARKS
         *****************************************************************/

        /// <summary>
        /// Give an overview of all the benchmarks which are loaded, if 
        /// any are loade. 
        /// </summary>
        private static void GiveOverviewOfLoadedBenchmarks()
        {
            ConsoleLayout.Header("All benchmarks");

            if (Benchmarks.LoadedBenchmarks.Count == 0)
                Console.WriteLine("There are no benchmarks loaded...");

            for (int i = 0; i < Benchmarks.LoadedBenchmarks.Count; i++)
                Console.WriteLine(i + 1 + ": " + Benchmarks.LoadedBenchmarks[i].Name);

            ConsoleLayout.Footer();
        }

        /// <summary>
        /// Give an overview of all available benchmarks. 
        /// </summary>
        private static void GiveOverviewOfAvailableBenchmarks()
        {
            ConsoleLayout.Header("All benchmarks");
            
            FileInfo[] benchmarks = (new DirectoryInfo(Globals.BenchmarkFolder)).GetFiles();
            for (int i = 0; i < benchmarks.Length; i++) 
                Console.WriteLine(i + 1 + ": " + benchmarks[i].Name);

            ConsoleLayout.Footer();
        }

        /// <summary>
        /// Load all the available benchmarks. 
        /// </summary>
        private static void LoadAllAvailableBenchmarks()
        {
            Benchmarks.LoadedBenchmarks = (new DirectoryInfo(Globals.BenchmarkFolder)).GetFiles().ToList();
            Console.WriteLine("All available benchmarks are loaded.");

            ConsoleLayout.Footer();
        }


        /*****************************************************************
         * TESTING
         *****************************************************************/

        /// <summary>
        /// Test all available initial mapping algorithms. 
        /// </summary>
        private static void TestAvailableInitialMappings()
        {
            ConsoleLayout.Header("Initial mapping test");

            int nbRep = 10;
            int nbQubits = 20;
            int nbGates = 5000;
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