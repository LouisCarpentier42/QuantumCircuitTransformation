using QuantumCircuitTransformation.QuantumCircuitComponents.Circuit;
using QuantumCircuitTransformation.QuantumCircuitComponents.Architecture;
using QuantumCircuitTransformation.Data;
using System;
using QuantumCircuitTransformation.MappingPerturbation;

namespace QuantumCircuitTransformation.Algorithms.InitialMappingAlgorithm
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
    /// @version:  1.2
    /// 
    /// </summary>
    public class SimulatedAnnealing : InitialMapping
    {
        /// <summary>
        /// Variable referring to the maximum temperature during the algorithm. 
        /// </summary>
        public readonly int MaxTemperature;
        /// <summary>
        /// Variable referring to the minimum temperature during the algorithm. 
        /// </summary>
        public readonly int MinTemperature;
        /// <summary>
        /// Variable referring to the cooling factor of the temperature. 
        /// </summary>
        public readonly double CoolingFactor;
        /// <summary>
        /// Variable referring to the number of repetitions for a som fixed temperature.
        /// </summary>
        public readonly int NbRepetitions;


        /// <summary>
        /// Initialise a new simuted annealing algorithms with all it's parameters. 
        /// </summary>
        /// <param name="maxTemperature"> The variable for <see cref="MaxTemperature"/>. </param>
        /// <param name="minTemperature"> The variable for <see cref="MinTemperature"/>. </param>
        /// <param name="coolingFactor"> The variable for <see cref="CoolingFactor"/>. </param>
        /// <param name="nbRepetitions"> The variable for <see cref="NbRepetitions"/>. </param>
        public SimulatedAnnealing(int maxTemperature, int minTemperature, double coolingFactor, int nbRepetitions)
        {
            MaxTemperature = maxTemperature;
            MinTemperature = minTemperature;
            CoolingFactor = coolingFactor;
            NbRepetitions = nbRepetitions;
        }

        /// <summary>
        /// See <see cref="Algorithm.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object other)
        {
            if (other == null) return false;
            try
            {
                SimulatedAnnealing o = (SimulatedAnnealing)other;
                return MaxTemperature == o.MaxTemperature &&
                       MinTemperature == o.MinTemperature &&
                       CoolingFactor == o.CoolingFactor &&
                       NbRepetitions == o.NbRepetitions;
            } catch (InvalidCastException)
            {
                return false;
            }
        }

        /// <summary>
        /// See <see cref="Algorithm.GetHashCode"/>.
        /// </summary>
        public override int GetHashCode()
        {
            return (int)(Math.Pow(2, MaxTemperature)) *
                   (int)(Math.Pow(3, MinTemperature)) *
                   (int)(Math.Pow(5, CoolingFactor*10000)) *
                   (int)(Math.Pow(7, NbRepetitions));
        }

        /// <summary>
        /// See <see cref="Algorithm.Name"/>.
        /// </summary>
        public override string Name()
        {
            return "Simulated Annealing";
        }

        /// <summary>
        /// See <see cref="Algorithm.Parameters"/>.
        /// </summary>
        public override string Parameters()
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
        public override (Mapping, double) Execute(ArchitectureGraph architecure, QuantumCircuit circuit)
        {
            Mapping bestMapping = GetRandomMapping(architecure.NbNodes);
            double bestCost = GetMappingCost(bestMapping, architecure, circuit);

            Mapping mapping = bestMapping.Clone();
            double cost = bestCost;

            Swap swap;
            Mapping newMapping;
            
            for (double t = MaxTemperature; t > MinTemperature; t *= CoolingFactor)
            {
                for (int i = 0; i < NbRepetitions; i++)
                {
                    swap = GetSwapPerturbation(mapping.NbQubits);

                    newMapping = mapping.Clone();
                    swap.Apply(newMapping);
                    double newCost = GetMappingCost(newMapping, architecure, circuit);

                    if (newCost < bestCost)
                    {
                        bestMapping = newMapping;
                        bestCost = newCost;
                    }

                    if (newCost < cost || DoDownhillMove(cost, newCost, t))
                    {
                        mapping = newMapping;
                        cost = newCost;
                    }
                    // Console.WriteLine("Best: {0} - Cost: {1} - newCost: {2}", bestCost, cost, newCost);
                }
            }
            //Console.WriteLine("Best: {0}", bestCost);
            return (bestMapping, bestCost);
        }

        /// <summary>
        /// Checks if a downhill move should be made or not. 
        /// </summary>
        /// <param name="cost"> The cost of the current mapping. </param>
        /// <param name="newCost"> The cost of the newly generated mapping. </param>
        /// <param name="temperature"> The current temperature in the algorithm. </param>
        /// <returns>
        /// True is returened with a probability equal to: 
        ///       exp[(cost - newCost) / temperature]
        /// </returns>
        private bool DoDownhillMove(double cost, double newCost, double temperature)
        {
            return Globals.Random.NextDouble() < Math.Exp((cost - newCost) / temperature);
        }
    }
}