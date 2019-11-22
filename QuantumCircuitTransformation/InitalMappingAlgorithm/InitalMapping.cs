using QuantumCircuitTransformation.QuantumCircuitComponents;
using QuantumCircuitTransformation.QuantumCircuitComponents.Architecture;
using System;
using System.Linq;

namespace QuantumCircuitTransformation.InitalMappingAlgorithm
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
    /// @version:  1.0
    /// 
    /// </summary>
    public abstract class InitalMapping
    {
        /// <summary>
        /// Execute this inital mapping algorithm. 
        /// </summary>
        /// <param name="architecture"> The architecture to find a mapping for. </param>
        /// <param name="circuit"> The circuit containing the qubits to map. </param>
        /// <returns>
        /// The best <see cref="Mapping"/> which has been found by the algorithm. 
        /// </returns>
        public abstract Mapping Execute(ArchitectureGraph architecture, QuantumCircuit circuit);

        /// <summary>
        /// Get a random mapping with the given number of nodes. 
        /// </summary>
        /// <param name="NbNodes"> The number of nodes to map. </param>
        /// <returns>
        /// A mapping in which each of the qubit ID's are mapped on a random ID. 
        /// </returns>
        public static int[] GetRandomMapping(int NbNodes)
        {
            return Enumerable.Range(0, NbNodes).OrderBy(i => Guid.NewGuid()).ToArray();
        }

        /// <summary>
        /// Give a description of the algorithm and it's parameters. 
        /// </summary>
        public abstract override string ToString();




        protected int[] PerturbatMapping(int[] mapping)
        {
            int[] perturbation = new int[mapping.Length];
            Array.Copy(mapping, perturbation, mapping.Length);

            int x = Globals.Random.Next(mapping.Length);
            int y;
            do
            {
                y = Globals.Random.Next(mapping.Length);
            } while (x == y);

            int temp = perturbation[x];
            perturbation[x] = perturbation[y];
            perturbation[y] = temp;
            return perturbation;
        }


        public static double GetMappingCost(int[] mapping, ArchitectureGraph architecture, QuantumCircuit circuit)
        {
            double cost = 0;
            double weight;
            double param = 1;

            for (int i = 0; i < circuit.GetNbGates(); i++)
            {
                weight = (-param / circuit.GetNbGates()) * i + param;
                cost += (weight * architecture.GetCNOTDistance(mapping[circuit.GetGateAt(i).ControlQubit], mapping[circuit.GetGateAt(i).TargetQubit]));
            }

            return cost;
        }

    }
}
