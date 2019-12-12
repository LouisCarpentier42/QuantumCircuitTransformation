using QuantumCircuitTransformation.MappingPerturbation;
using QuantumCircuitTransformation.Data;
using QuantumCircuitTransformation.Algorithms.InitialMappingAlgorithm;
using QuantumCircuitTransformation.QuantumCircuitComponents.Circuit;
using QuantumCircuitTransformation.QuantumCircuitComponents.Architecture;
using QuantumCircuitTransformation.Algorithms.TransformationAlgorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using QuantumCircuitTransformation.QuantumCircuitComponents.Gates;

namespace QuantumCircuitTransformation
{
    class Program
    {

        private static readonly List<Tuple<string, Action>> MainMenu = new List<Tuple<string, Action>>
        {
            new Tuple<string, Action>("Load an initial mapping algorithm", 
                new Action(LoadInitialMappings)),
            new Tuple<string, Action>("Add a new initial mapping algorithm", 
                new Action(AddInitialMapping)),
            new Tuple<string, Action>("Give an overview of all the available algorithms", 
                new Action(GiveOverviewOfAllAlgorithms)),
            new Tuple<string, Action>("Give an overview of the loaded algorithms", 
                new Action(GiveOverviewOfLoadedAlgorithms)),
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

            HeuristicSearch hs = new HeuristicSearch();
            //hs.Execute();


            List<PhysicalGate> x = new List<PhysicalGate>
            {
                new CNOT(0,1),
                new CNOT(1,2),
                new CNOT(2,3),
                new CNOT(3,4)
            };
            List<PhysicalGate> y = new List<PhysicalGate>(x);

            x[2] = SingleQubitGate.GetHadamardGate(10);

            foreach (var a in x)
                Console.WriteLine(a);
            foreach (var a in y)
                Console.WriteLine(a);



            //QuantumCircuit c = new QuantumCircuit();
            //c.AddGate(new CNOT(0, 1));
            //c.AddGate(SingleQubitGate.GetHadamardGate(1));
            //c.AddGate(SingleQubitGate.GetHadamardGate(1));
            //c.AddGate(new CNOT(0, 1));


            //AllAlgorithms.InitialMappings[0].Execute(QuantumDevices.IBM_Q20, c);
            //Console.ReadLine();
            //AllAlgorithms.InitialMappings[1].Execute(QuantumDevices.IBM_Q20, c);




            //for(int i = 0; i < nbNodes; i++)
            //{
            //    for (int j = 0; j < nbNodes; j++)
            //    {
            //        Console.Write(paths[i, j] + " ");
            //    }
            //    Console.WriteLine();
            //}



            //int nbCircuits = 100;
            //int nbGates = 20000;
            //for (int nbQubits = 4; nbQubits < 20; nbQubits++)
            //{
            //    int averageLayers = 0;
            //    for (int i = 0; i < nbCircuits; i++)
            //    {
            //        averageLayers += (CircuitGenerator.RandomCircuit(nbGates, nbQubits).NbLayers);
            //    }
            //    Console.WriteLine("NbL / NbG: " + averageLayers / nbGates);
            //    //Console.WriteLine("Average number of layers: " + averageLayers / nbCircuits);
            //    //Console.WriteLine("NbGates / NbQubits: " + nbGates / nbQubits);
            //    //Console.WriteLine("NbLayers *  NbQubits: " + averageLayers * nbQubits);
            //    Console.WriteLine("----------------------------------------");
            //}



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
                if (!AllAlgorithms.InitialMappings.Contains(initialMapping))
                    AllAlgorithms.InitialMappings.Add(initialMapping);

            }
            catch (Exception e) when (e is IndexOutOfRangeException || e is ArgumentOutOfRangeException || e is FormatException)
            {
                Console.WriteLine();
                ConsoleLayout.Error();
            }

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

        private static void TestAvailableInitialMappings()
        {
            ConsoleLayout.Header("Initial mapping test");

            int nbRep = 10;
            int nbQubits = 20;
            int nbGates = 500;
            ArchitectureGraph architecture = QuantumDevices.IBM_Q20;
            
            double[] totalCost = new double[AllAlgorithms.InitialMappings.Count()];
            double[] titotalTime = new double[AllAlgorithms.InitialMappings.Count()];

            for (int i = 0; i < nbRep; i++)
            {
                Console.WriteLine("Iteration " + (i + 1));
                QuantumCircuit circuit = CircuitGenerator.RandomCircuit(nbGates, nbQubits);
                for (int j = 0; j < AllAlgorithms.InitialMappings.Count(); j++)
                {
                    Globals.Timer.Restart();
                    (_, double cost) = AllAlgorithms.InitialMappings[j].Execute(architecture, circuit);
                    Globals.Timer.Stop();
                    totalCost[j] += cost;
                    titotalTime[j] += Globals.Timer.Elapsed.TotalMilliseconds;
                    //Console.WriteLine("cost = {1} - time = {2} ({0})", AllAlgorithms.InitialMappings[j].Name(), cost, Globals.Timer.Elapsed.TotalMilliseconds);
                }
            }

            for (int i = 0; i < AllAlgorithms.InitialMappings.Count(); i++)
            {
                Console.WriteLine();
                Console.WriteLine(AllAlgorithms.InitialMappings[i].Name());
                Console.WriteLine(AllAlgorithms.InitialMappings[i].Parameters());
                Console.WriteLine("Result: Cost = {0} - time = {1}", totalCost[i] / nbRep, titotalTime[i] / nbRep);
            }

            ConsoleLayout.Footer();
        }





        private static void DependencyGraphMaker(List<PhysicalGate> gates)
        {
            List<Tuple<int, int>> edges = new List<Tuple<int, int>>();
            List<PhysicalGate> nodes = new List<PhysicalGate>(gates);

            for (int i = 0; i < gates.Count; i++)
            {
                for (int j = i + 1; j < gates.Count; j++)
                {
                    if (gates[i].DependsOn(gates[j]))
                        edges.Add(new Tuple<int, int>(i, j));
                    // Later remove redundant edges. 
                }
            }

            // Dependency graph: edges and nodes
        }




    }
}