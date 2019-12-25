using QuantumCircuitTransformation.MappingPerturbation;
using QuantumCircuitTransformation.Data;
using QuantumCircuitTransformation.QuantumCircuitComponents.Circuit;
using QuantumCircuitTransformation.QuantumCircuitComponents.ArchitectureGraph;
using System;
using System.Linq;
using QuantumCircuitTransformation.QuantumCircuitComponents.Gates;
using QuantumCircuitTransformation.QuantumCircuitComponents.Gates.PhysicalGates;

namespace QuantumCircuitTransformation.Algorithms.InitialMappingAlgorithm
{
    /// <summary>
    /// 
    /// InitialMappingAlgorithm:
    ///    An abstract class which serves as a parent for any initial
    ///    mapping algorithm. This class offers different methods which
    ///    can serve to easily implement the desired algorithm. These 
    ///    can be overwritten if one wishes to do so. 
    ///    An initial mapping maps the qubits from a logical circuit on 
    ///    the physical qubits of an architecture. This can enormously 
    ///    improve the efficiency of a quantum circuit transformation 
    ///    algorithm.
    /// 
    /// @author:   Louis Carpentier
    /// @version:  1.9
    /// 
    /// </summary>
    public abstract class InitialMapping : Algorithm
    {
        /// <summary>
        /// Execute this inital mapping algorithm. 
        /// </summary>
        /// <param name="architecture"> The architecture to find a mapping for. </param>
        /// <param name="circuit"> The circuit containing the qubits to map. </param>
        /// <returns>
        /// The best <see cref="Mapping"/> which has been found by the initial mapping algorithm. 
        /// </returns>
        public abstract (Mapping,double) Execute(Architecture architecture, LogicalCircuit circuit);

        /// <summary>
        /// See <see cref="Algorithm.Name"/>.
        /// </summary>
        public abstract string Name();

        /// <summary>
        /// See<see cref="Algorithm.Parameters"/>.
        /// </summary>
        public abstract string Parameters();


        /// <summary>
        /// Get a random mapping with the given number of nodes. 
        /// </summary>
        /// <param name="NbNodes"> The number of nodes to map. </param>
        /// <returns>
        /// A mapping in which each of the qubit ID's are mapped on a random ID. 
        /// </returns>
        public static Mapping GetRandomMapping(int NbNodes)
        {
            return new Mapping(Enumerable.Range(0, NbNodes).OrderBy(i => Globals.Random.Next()).ToArray());
        }

        /// <summary>
        /// Returns a new swap perturbation for a mapping with the 
        /// given number of qubits.
        /// </summary>
        /// <param name="nbQubits"> The number of qubits which can be swapped </param>
        /// <returns>
        /// A random swap operation for the given mapping.
        /// </returns>
        protected static Swap GetSwapPerturbation(int nbQubits)
        {
            int swapQubit1, swapQubit2;
            swapQubit1 = Globals.Random.Next(nbQubits);
            do swapQubit2 = Globals.Random.Next(nbQubits); while (swapQubit1 == swapQubit2);
            return new Swap(swapQubit1, swapQubit2);
        }

        private const int MAX_NB_GATES_IN_COST = 10000;


        public static double GetMappingCost(Mapping mapping, Architecture architecture, LogicalCircuit circuit)
        {
            // Parameters to calculate the weight of each gate
            double p1 = circuit.NbCnotGates / Math.Min(circuit.NbGates, MAX_NB_GATES_IN_COST);
            double p2 = 0.1;

            double cost = 0;
            int NbOfGatesLookedAt = 0;

            for (int i = 0; i < circuit.NbCnotGates; i++)
            {
                double weight = 1 / (++NbOfGatesLookedAt / p1 + p2);
                if (circuit.Gates[i] is CNOT)
                    cost += architecture.GetGateCost(mapping.MapCNOT((CNOT)circuit.Gates[i]));
                if (NbOfGatesLookedAt > MAX_NB_GATES_IN_COST)
                    return cost;
            }

            return cost;
        }



    }
}
