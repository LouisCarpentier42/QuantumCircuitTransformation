using QuantumCircuitTransformation.QuantumCircuitComponents;
using QuantumCircuitTransformation.QuantumCircuitComponents.Architecture;
using System;

namespace QuantumCircuitTransformation.InitalMappingAlgorithm
{
    /// <summary>
    /// 
    /// SimulatedAnnealing:
    ///    A class for finding an initial mapping of logical qubits on a given
    ///    physical architecture. (See <see cref="InitialMapping"/>).
    ///    Simmulated Annealing tries to find a solution with minimal cost. This
    ///    is done by generating a new mapping from a some mapping. If this new 
    ///    mapping is better, it is chosen. To escape from local a local optima, 
    ///    the algorithm will accept a certain mapping even though it is worse 
    ///    than the current mapping. This is done with a probability corresponding
    ///    to the current temperature, which is declined during the algorithm. 
    ///    
    /// @author:   Louis Carpentier
    /// @version:  1.0
    /// 
    /// </summary>
    public class SimulatedAnnealing : InitialMapping
    {
        /// <summary>
        /// Variable referring to the maximum temperature during the algorithm. 
        /// </summary>
        private readonly int MaxTemperature;
        /// <summary>
        /// Variable referring to the minimum temperature during the algorithm. 
        /// </summary>
        private readonly int MinTemperature;
        /// <summary>
        /// Variable referring to the cooling factor of the temperature. 
        /// </summary>
        private readonly double CoolingFactor;
        /// <summary>
        /// Variable referring to the number of repetitions for a som fixed temperature.
        /// </summary>
        private readonly int NbRepetitions;

        /// <summary>
        /// Initialise a new simuted annealing algorithms with all it's parameters. 
        /// </summary>
        /// <param name="maxT"> The variable for <see cref="MaxTemperature"/>. </param>
        /// <param name="minT"> The variable for <see cref="MinTemperature"/>. </param>
        /// <param name="delta"> The variable for <see cref="CoolingFactor"/>. </param>
        /// <param name="r"> The variable for <see cref="NbRepetitions"/>. </param>
        public SimulatedAnnealing(int maxT, int minT, double delta, int r)
        {
            MaxTemperature = maxT;
            MinTemperature = minT;
            CoolingFactor = delta;
            NbRepetitions = r;
        }

        /// <summary>
        /// See <see cref="InitialMapping.GetName"/>.
        /// </summary>
        public override string GetName()
        {
            return "Simulated Annealing";
        }

        /// <summary>
        /// See <see cref="InitialMapping.GetParameters"/>.
        /// </summary>
        public override string GetParameters()
        {
            return 
                " > The maximum temperature: " + MaxTemperature + '\n' +
                " > The minimum temperature: " + MinTemperature + '\n' +
                " > The cooling factor: " + CoolingFactor + '\n' +
                " > The number of repetitions for given temperature: " + NbRepetitions;
        }






        /// <summary>
        /// Execute simulated annealing to find a mapping for the given circuit, which
        /// fits on the given architecture. 
        /// (See <see cref="InitialMapping.Execute(ArchitectureGraph, QuantumCircuit)"/>)
        /// </summary>
        public override Mapping Execute(ArchitectureGraph architecure, QuantumCircuit circuit)
        {
            // SetGates(architecure, circuit);
            int[] bestMapping = GetRandomMapping(architecure.NbNodes);
            int[] mapping = new int[bestMapping.Length];
            Array.Copy(bestMapping, mapping, bestMapping.Length);
            double bestCost = GetMappingCost(bestMapping, architecure, circuit);
            double cost = bestCost * 1.5;

            for (double t = MaxTemperature; t > MinTemperature; t *= CoolingFactor)
            {
                //Console.WriteLine("------------------{0}------------------", t);
                for (int i = 0; i < NbRepetitions; i++)
                {
                    //Console.WriteLine("Best: {0} - Cost: {1}", bestCost, cost);
                    (int[] newMapping, int swap1, int swap2) = PerturbatMapping(mapping);
                    //double newCost = CalculateCostDifference(cost, swap1, swap2, architecure, circuit);
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
                    else if (DoDownhillMove(cost, newCost, t))
                    {
                        cost = newCost;
                        mapping = newMapping;
                    }
                }
            }
            Console.WriteLine("Best: {0}", bestCost);
            return new Mapping(bestMapping);
        }

        /// <summary>
        /// Checks if a downhill move should be made or not. 
        /// </summary>
        /// <param name="cost"> The cost of the current mapping. </param>
        /// <param name="newCost"> The cost of the newly generated mapping. </param>
        /// <param name="temperature"> The current temperature in the algorithm. </param>
        /// <returns>
        /// True is returened with a probability equal to: 
        ///    exp[(cost - newCost) / temperature]
        /// </returns>
        private bool DoDownhillMove(double cost, double newCost, double temperature)
        {
            return Globals.Random.NextDouble() < Math.Exp((cost - newCost) / temperature);
        }
    }
}