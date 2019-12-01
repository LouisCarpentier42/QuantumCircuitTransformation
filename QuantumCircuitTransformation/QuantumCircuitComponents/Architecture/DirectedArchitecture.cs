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
    ///     @version:  1.2
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
        /// Compute the CNOT distance for two nodes with given pathlength
        /// between them. 
        /// </summary>
        /// <param name="pathLength"> The pathlength between the two nodes. </param>
        /// <returns>
        /// For every swap gate are 7 extra gates needed, thus the pathlength 
        /// minus one and times 7.
        /// </returns>
        protected override int ComputeCNOTDistance(int pathLength)
        {
            return Math.Max(0, 7 * (pathLength - 1));
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