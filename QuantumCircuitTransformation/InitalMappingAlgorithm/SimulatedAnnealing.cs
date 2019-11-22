using QuantumCircuitTransformation.QuantumCircuitComponents;
using QuantumCircuitTransformation.QuantumCircuitComponents.Architecture;
using System;
using System.Linq;

namespace QuantumCircuitTransformation.InitalMappingAlgorithm
{
    public class SimulatedAnnealing : InitalMapping
    {
        private readonly int MaxTemperature;
        private readonly int MinTemperature;
        private readonly double CoolingCoefficient;
        private readonly int NbRepetitions;
       
        public SimulatedAnnealing(int maxT, int minT, double delta, int r)
        {
            MaxTemperature = maxT;
            MinTemperature = minT;
            CoolingCoefficient = delta;
            NbRepetitions = r;
        }

        public override Mapping Execute(ArchitectureGraph architecure, QuantumCircuit circuit)
        {
            // Setup 
            int[] bestMapping = GetRandomMapping(architecure.NbNodes);
            int[] mapping = new int[bestMapping.Length];
            Array.Copy(bestMapping, mapping, bestMapping.Length);
            double bestCost = double.MaxValue;
            double cost = double.MaxValue;

            // algo
            for (double t = MaxTemperature; t > MinTemperature; t *= CoolingCoefficient)
            {
                //Console.WriteLine("------------------{0}------------------", t);
                for (int i = 0; i < NbRepetitions; i++)
                {
                    //Console.WriteLine("Best: {0} - Cost: {1}", bestCost, cost);
                    int[] newMapping = PerturbatMapping(mapping);
                    double newCost = GetMappingCost(newMapping, architecure, circuit);

                    if (newCost < bestCost)
                    {
                        bestCost = newCost;
                        bestMapping = newMapping;
                    }
                    
                    if (newCost < cost)
                    {
                        cost = newCost;
                        mapping = newMapping;
                    }
                    else if (Globals.Random.NextDouble() < Math.Exp((cost - newCost) / t))
                    {
                        cost = newCost;
                        mapping = newMapping;
                    }
                    
                }
            }
            Console.WriteLine("Best: {0}", bestCost);
            return new Mapping(bestMapping);
        }




    }
}
