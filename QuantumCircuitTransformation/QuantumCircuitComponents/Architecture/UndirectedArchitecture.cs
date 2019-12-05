using QuantumCircuitTransformation.QuantumCircuitComponents.Gates;
using System;
using System.Collections.Generic;

namespace QuantumCircuitTransformation.QuantumCircuitComponents.Architecture
{
    /// <summary>
    ///     UndirectedArchitecture:
    ///         A class for undirected architecture graphs of physical quantum devices. 
    ///         In an undirected architecture can a CNOT gate be executed on any pair
    ///         of qubits that are connected, where both qubits can be the control qubit.  
    /// </summary>
    /// <remarks>
    ///     @author:   Louis Carpentier
    ///     @version:  1.2
    /// </remarks>
    public class UndirectedArchitecture : ArchitectureGraph
    {
        /// <summary>
        /// See <see cref="ArchitectureGraph(List{Tuple{int, int}})"/>
        /// </summary>
        public UndirectedArchitecture(List<Tuple<int, int>> connections) 
            : base(connections) { }

        /// <summary>
        /// Initialise a new undirected architecture graph with given edges, 
        /// number of nodes and cnot distances. 
        /// </summary>
        /// <param name="edges"> The edges for this architecture. </param>
        /// <param name="nbNodes"> The number of nodes for this architecture. </param>
        /// <param name="cnotDistance"> The cnot distances for this architecture. </param>
        protected UndirectedArchitecture(List<Tuple<int, int>> edges, int nbNodes, int[,] cnotDistance)
            : base(edges, nbNodes, cnotDistance) { }


        /// <summary>
        /// See <see cref="ArchitectureGraph.CanExecuteCNOT(CNOT)"/>.
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
        /// See <see cref="ArchitectureGraph.NbOfCnotGatesPerSwap"/>.
        /// </summary>
        /// <returns>
        /// A swap gate can be replaced by three cnot gates. First a cnot gate from a
        /// to be, then a cnot gate from b to a and finally a cnot gate from a to b. 
        /// </returns>
        public override int NbOfCnotGatesPerSwap()
        {
            return 3;
        }

        /// <summary>
        /// See <see cref="ArchitectureGraph.Clone(List{Tuple{int, int}}, int, int[,])"/>.
        /// </summary>
        protected override ArchitectureGraph Clone(List<Tuple<int, int>> edges, int nbNodes, int[,] cnotDistance)
        {
            return new UndirectedArchitecture(edges, nbNodes, cnotDistance);
        }
    }
}