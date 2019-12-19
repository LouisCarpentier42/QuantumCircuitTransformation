using QuantumCircuitTransformation.QuantumCircuitComponents.Gates;
using QuantumCircuitTransformation.QuantumCircuitComponents.Gates.PhysicalGates;
using System;
using System.Collections.Generic;

namespace QuantumCircuitTransformation.QuantumCircuitComponents.ArchitectureGraph
{
    /// <summary>
    ///     DirectedArchitecture:
    ///         A class for directed architecture graphs of physical quantum devices.
    ///         In an directed architecture can a CNOT gate only be executed if there 
    ///         is a directed connection from the control qubit to the target qubit.
    /// </summary>
    /// <remarks>    
    ///     @author:   Louis Carpentier
    ///     @version:  1.4
    /// </remarks>
    public class DirectedArchitecture : Architecture
    {
        /// <summary>
        /// See <see cref="Architecture(List{Tuple{int, int}})"/>
        /// </summary>
        public DirectedArchitecture(List<Tuple<int, int>> connections) 
            : base(connections) { }

        /// <summary>
        /// See <see cref="Architecture.CanExecuteCNOT(int, int)"/>.
        /// </summary>
        /// <returns>
        /// True if and only if there exists an edge which connectes the given 
        /// control and target qubit from the control qubit to the target qubit.
        /// False is returned in all other cases. 
        /// </returns>
        public override bool CanExecuteCNOT(CNOT cnot)
        {
            return Edges.Contains(new Tuple<int, int>(cnot.ControlQubit, cnot.TargetQubit));
        }

        /// <summary>
        /// See <see cref="Architecture.NbOfCnotGatesPerSwap"/>.
        /// </summary>
        /// <returns>
        /// A swap gate can be replaced by three cnot gates and 4 Hadamard gates. 
        /// First a cnot gate from a to be is needed, then a Hadamard gate on both
        /// the qubits followed by a cnot gate from a to b and anothor pair of 
        /// Hadamard gates on both the qubits, and finally a cnot gate from a to b. 
        /// </returns>
        public override int NbOfCnotGatesPerSwap()
        {
            return 7;
        }
    }
}