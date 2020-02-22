using QuantumCircuitTransformation.MappingPerturbation;
using QuantumCircuitTransformation.QuantumCircuitComponents.Circuit;
using QuantumCircuitTransformation.QuantumCircuitComponents.Gates;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuantumCircuitTransformation.QuantumCircuitComponents.ArchitectureGraph
{
    /// <summary>
    ///     Architecture
    ///         An abstract class for the architecture graph of 
    ///         some quantum device.
    /// </summary>
    /// <remarks>
    ///     @Author:   Louis Carpentier
    ///     @Version:  2.3
    /// </remarks>
    public abstract class Architecture
    {
        /// <summary>
        /// The number of nodes in this architecture. 
        /// </summary>
        public readonly int NbNodes;
        /// <summary>
        /// The edges between the different nodes in this architecture. 
        /// </summary>
        protected readonly List<int>[] Edges;
        /// <summary>
        /// The CNOT distance between all pairs of nodes in this architecture.  
        /// </summary>
        /// <remarks>
        /// The CNOT distance between two qubuts is the number of gates that should be 
        /// added to make sure that a CNOT gate can be executed from the first to the 
        /// second qubit. 
        /// </remarks>
        private readonly int[,] CNOTDistance;


        /// <summary>
        /// Initialise a new architecture graph of a quantum device.
        /// </summary>
        /// <param name="connections"> The connections in this architecture graph. </param>
        /// <remarks>
        /// The edges should be normalised, this means that no ID's may be skipped. 
        /// </remarks>
        public Architecture(List<Tuple<int, int>> connections)
        {
            // Set The number of nodes
            NbNodes = connections.Max(edge => Math.Max(edge.Item1, edge.Item2)) + 1;
            // Add the connections to the edges
            Edges = new List<int>[NbNodes];
            for (int i = 0; i < NbNodes; i++)
                Edges[i] = new List<int>();
            foreach (Tuple<int, int> edge in connections)
                Edges[edge.Item1].Add(edge.Item2);
            // Se t the CNOT distance
            CNOTDistance = ShortestPathFinder.Dijkstra(connections);
        }

        /// <summary>
        /// Returns the cost of a CNOT gate with given control and target qubit. 
        /// </summary>
        /// <param name="controlQubit"> The control qubit of the CNOT gate. </param>
        /// <param name="TargetQubit"> The target qubit of the CNOT gate. </param>
        /// <returns>
        /// The path distance between two nodes. 
        /// </returns>
        public int GetCost(int controlQubit, int TargetQubit)
        {
            return CNOTDistance[controlQubit, TargetQubit];
        }

        /// <summary>
        /// Checks whether or not there exists a connection between 
        /// the two given id's.
        /// </summary>
        /// <param name="from"> The id from where the connection should start. </param>
        /// <param name="to"> The id to where the connection should go. </param>
        /// <returns>
        /// True if and only if there exists a connection from the 'from' id to
        /// the 'to' id. 
        /// </returns>
        public abstract bool HasConnection(int from, int to);

        /// <summary>
        /// Adds the gates needed to execute the given swap on the given circuit.
        /// </summary>
        /// <param name="circuit"> The circuit to execute the swap on. </param>
        /// <param name="swap"> The swap to execute on the circuit. </param>
        public abstract void AddSwapGates(PhysicalCircuit circuit, Swap swap);
    }
}