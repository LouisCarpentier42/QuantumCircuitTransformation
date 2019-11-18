using System;
using System.Collections.Generic;

namespace QuantumCircuitTransformation.QuantumCircuitComponents.Architecture
{
    /// <summary>
    /// 
    /// DirectedArchitecture
    ///    A class for directed architecture graphs of physical quantum devices.
    ///    In an directed architecture can a CNOT gate only be executed if there 
    ///    is a directed connection from the control qubit to the target qubit.
    ///    
    /// @author:   Louis Carpentier
    /// @version:  1.0
    /// 
    /// </summary>
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
        /// See <see cref="ArchitectureGraph.ComputeCNOTDistance(int)"/>.
        /// </summary>
        /// <returns>
        /// For every SWAP operations are seven extra gates needed (cause the architecture 
        /// is directed). The number of SWAP gates to add equals to the length of the 
        /// shortest path minus one. 
        /// </returns>
        protected override int ComputeCNOTDistance(int shortestPathLength)
        {
            return 7 * (shortestPathLength - 1);
        }
    }
}