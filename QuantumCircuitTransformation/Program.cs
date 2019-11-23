using System;
using System.Collections.Generic;
using QuantumCircuitTransformation.QuantumCircuitComponents.Architecture;
using System.Linq;
using QuantumCircuitTransformation.QuantumCircuitComponents;
using QuantumCircuitTransformation.InitalMappingAlgorithm;
using System.Timers;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Reflection;

namespace QuantumCircuitTransformation
{
    class Program
    {
        private static InitialMapping mappingAlgo;
        private static readonly Stopwatch Timer = new Stopwatch();
        private static readonly List<Tuple<string, Action>> Menu = new List<Tuple<string, Action>>
        {
            new Tuple<string, Action>("String", new Action(SelectLoadedInitialMappings))
        };




        static void Main(string[] args)
        {
            int selection = -1;
            while (selection != 0)
            {
                ConsoleLayout.Header("Main");
                
                ConsoleLayout.PrintMenu(Menu);

                Console.Write("\nSelection: ");
                selection = Convert.ToInt32(Console.ReadLine());

                switch (selection)
                {
                    case 2:
                        return;
                    case 1:
                        SelectLoadedInitialMappings();
                        break;
                    case 42:
                        Test();
                        break;
                    default:
                        ConsoleLayout.InvalidInput();
                        break;
                }
            }

            
        } 

        private static void Test()
        {
            /*
            FileInfo[] Files = new DirectoryInfo(Globals.BenchmarkFolder).GetFiles();
            foreach (FileInfo f in Files)
                Console.Write(f.Name);
            */


            InitialMapping sa = new SimulatedAnnealing(100, 1, 0.95, 100);
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


            /*
            
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("------------------ITERATION {0}------------------", i);
                QuantumCircuit c = CircuitGenerator.RandomCircuit(1000, 10);
                sa.Execute(QuantumDevices.IBM_Q20, c);
            }
            
            */
        }




        private static void SelectLoadedInitialMappings()
        {
            ConsoleLayout.Header("Select an initial mapping algorithm");
            var orderedInitalMapping = LoadedAlgorithms.InitialMapping.OrderBy(item => item.GetType().FullName);
            for (int i = 0; i < orderedInitalMapping.Count(); i++)
            {
                Console.WriteLine(i+1 + ": " + orderedInitalMapping.ElementAt(i).GetName());
                Console.WriteLine(orderedInitalMapping.ElementAt(i).GetParameters() + '\n');
            }

            try
            {
                Console.Write("Give the ID of the initial mapping: ");
                int index = Convert.ToInt32(Console.ReadLine());
                mappingAlgo = orderedInitalMapping.ElementAt(index-1);
                Console.WriteLine("\nInitial mapping {0} has been selected.", index);
                ConsoleLayout.Footer();
            } catch (Exception e) when (e is ArgumentOutOfRangeException || e is FormatException)
            {
                ConsoleLayout.InvalidInput();
                ConsoleLayout.Footer();
                SelectLoadedInitialMappings();
            }
        }





        




     

    }
}