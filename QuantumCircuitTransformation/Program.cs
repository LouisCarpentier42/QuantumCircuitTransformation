using System;
using System.Collections.Generic;
using QuantumCircuitTransformation.QuantumCircuitComponents.Architecture;
using System.Linq;
using QuantumCircuitTransformation.QuantumCircuitComponents;
using QuantumCircuitTransformation.InitalMappingAlgorithm;

namespace QuantumCircuitTransformation
{
    class Program
    {
        static void Main(string[] args)
        {
            SimulatedAnnealing sa = new SimulatedAnnealing(100, 1, 0.95, 100);

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("------------------ITERATION {0}------------------", i);
                QuantumCircuit c = GetRandomCircuit(1000, 10);
                sa.Execute(QuantumDevices.IBM_Q20, c);
            }
            




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


            Console.ReadLine();
        } 




        private static QuantumCircuit GetRandomCircuit(int NbGates, int NbQubits)
        {
            QuantumCircuit circuit = new QuantumCircuit();

            for (int i = 0; i < NbGates; i++)
            {
                int x = Globals.Random.Next(NbQubits);
                int y;
                do
                {
                    y = Globals.Random.Next(NbQubits);
                } while (x == y);
                circuit.AddGate(new CNOT(x, y));
            }

            return circuit;
        }

    }
}