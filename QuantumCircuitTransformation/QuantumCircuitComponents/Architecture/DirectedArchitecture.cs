using System;
using System.Collections.Generic;

namespace QuantumCircuitTransformation.QuantumCircuitComponents.Architecture
{
    /// <summary>
    ///     DirectedArchitecture:
    ///         A class for directed architecture graphs of physical quantum devices.
    ///         In an directed architecture can a CNOT gate only be executed if there 
    ///         is a directed connection from the control qubit to the target qubit.
    /// </summary>
    /// <remarks>    
    ///     @author:   Louis Carpentier
    ///     @version:  1.3
    /// </remarks>
    public class DirectedArchitecture : ArchitectureGraph
    {
        /// <summary>
        /// See <see cref="ArchitectureGraph(List{Tuple{int, int}})"/>
        /// </summary>
        public DirectedArchitecture(List<Tuple<int, int>> connections) 
            : base(connections) { }

        /// <summary>
        /// See <see cref="ArchitectureGraph.CanExecuteCNOT(int, int)"/>.
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
        /// Initialise a new directed architecture graph with given edges, 
        /// number of nodes and cnot distances. 
        /// </summary>
        /// <param name="edges"> The edges for this architecture. </param>
        /// <param name="nbNodes"> The number of nodes for this architecture. </param>
        /// <param name="cnotDistance"> The cnot distances for this architecture. </param>
        protected DirectedArchitecture(List<Tuple<int, int>> edges, int nbNodes, int[,] cnotDistance)
            : base(edges, nbNodes, cnotDistance) { }

        /// <summary>
        /// See <see cref="ArchitectureGraph.NbOfCnotGatesPerSwap"/>.
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

        /// <summary>
        /// See <see cref="ArchitectureGraph.Clone(List{Tuple{int, int}}, int, int[,])"/>.
        /// </summary>
        protected override ArchitectureGraph Clone(List<Tuple<int, int>> edges, int nbNodes, int[,] cnotDistance)
        {
            return new DirectedArchitecture(edges, nbNodes, cnotDistance);
        }
    }
}