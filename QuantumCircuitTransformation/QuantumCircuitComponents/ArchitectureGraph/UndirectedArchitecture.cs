using QuantumCircuitTransformation.QuantumCircuitComponents.Gates;
using System;
using System.Collections.Generic;

namespace QuantumCircuitTransformation.QuantumCircuitComponents.ArchitectureGraph
{
    /// <summary>
    ///     UndirectedArchitecture:
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
        /// See <see cref="Architecture.CanExecuteCNOT(CNOT)"/>.
        /// </summary>
        /// <returns>
        /// True if and only if there exists an edge which connectes the control  
        /// and target qubit of the given CNOT gate in any direction. In all other 
        /// cases is false returned. 
        /// </returns>
        public override bool CanExecuteCNOT(CNOT cnot)
        {
            return Edges.Contains(new Tuple<int, int>(cnot.ControlQubit, cnot.TargetQubit)) ||
                   Edges.Contains(new Tuple<int, int>(cnot.TargetQubit, cnot.ControlQubit));
        }

        /// <summary>
        /// See <see cref="Architecture.NbOfCnotGatesPerSwap"/>.
        /// </summary>
        /// <returns>
        /// A swap gate can be replaced by three cnot gates. First a cnot gate from a
        /// to be, then a cnot gate from b to a and finally a cnot gate from a to b. 
        /// </returns>
        public override int NbOfCnotGatesPerSwap()
        {
            return 3;
        }
    }
}