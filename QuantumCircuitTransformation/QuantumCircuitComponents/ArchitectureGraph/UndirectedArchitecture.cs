using System;
using System.Collections.Generic;
using QuantumCircuitTransformation.MappingPerturbation;
using QuantumCircuitTransformation.QuantumCircuitComponents.Circuit;
using QuantumCircuitTransformation.QuantumCircuitComponents.Gates;

namespace QuantumCircuitTransformation.QuantumCircuitComponents.ArchitectureGraph
{
    /// <summary>
    ///     UndirectedArchitecture
    ///         A class for undirected architecture graphs of physical quantum devices. 
    ///         In an undirected architecture can a CNOT gate be executed on any pair
    ///         of qubits that are connected, where both qubits can be the control qubit.  
    /// </summary>
    /// <remarks>
    ///     @author:   Louis Carpentier
    ///     @version:  1.3
    /// </remarks>
    public class UndirectedArchitecture : Architecture
    {
        /// <summary>
        /// See <see cref="Architecture(List{Tuple{int, int}})"/>
        /// </summary>
        public UndirectedArchitecture(List<Tuple<int, int>> connections) 
            : base(connections) { }

        /// <summary>
        /// See <see cref="Architecture.HasConnection(int, int)"/>.
        /// </summary>
        /// <returns>
        /// True if and only if there exists a connection in any direction. 
        /// </returns>
        public override bool HasConnection(int from, int to)
        {
            return Edges[from].Contains(to) || Edges[to].Contains(from);
        }

        /// <summary>
        /// See <see cref="Architecture.AddSwapGates(PhysicalCircuit, Swap)"/>.
        /// </summary>
        public override void AddSwapGates(PhysicalCircuit circuit, Swap swap)
        {
            circuit.AddGate(new CNOT(swap.Qubit1, swap.Qubit2));
            circuit.AddGate(new CNOT(swap.Qubit2, swap.Qubit1));
            circuit.AddGate(new CNOT(swap.Qubit1, swap.Qubit2));
        }
    }
}