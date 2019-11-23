using System;
using System.Collections.Generic;
using QuantumCircuitTransformation.QuantumCircuitComponents.Architecture;
using System.Linq;
using QuantumCircuitTransformation.QuantumCircuitComponents;
using QuantumCircuitTransformation.InitalMappingAlgorithm;
using System.Timers;
using System.Diagnostics;

namespace QuantumCircuitTransformation
{
    class Program
    {
        private static readonly Stopwatch Timer = new Stopwatch();



        static void Main(string[] args)
        {

            


            
            InitalMapping sa = new SimulatedAnnealing(100, 1, 0.95, 100);
            InitalMapping ts = new TabuSearch(50,5,1000);
            QuantumCircuit c = CircuitGenerator.RandomCircuit(10000, 20);
            ArchitectureGraph a = QuantumDevices.IBM_Q20;

            Timer.Start();
            sa.Execute(a, c);
            Timer.Stop();
            Console.WriteLine("In total are {0} milliseconds taken", Timer.ElapsedMilliseconds);

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





            /*

            List<Tuple<int, int>> e = new List<Tuple<int, int>>
            {
                new Tuple<int, int>(0,1),
                new Tuple<int, int>(1,2),
                new Tuple<int, int>(3,2),
                new Tuple<int, int>(0,3),
                new Tuple<int, int>(1,3),
            };



            int NbNodes = 16;
            ArchitectureGraph a = QuantumDevices.IBM_QX5;
           
            
            string Result = ""; 
            for (int i = 0; i < NbNodes; i++)
            {
                Result += a.GetCNOTDistance(i, 0).ToString();
                for (int j = 1; j < NbNodes; j++)
                    Result += " - " + a.GetCNOTDistance(i, j);
                Result += "\n";
            }
            Console.WriteLine(Result);
            
            */


            //Console.ReadLine();
        } 




     

    }
}