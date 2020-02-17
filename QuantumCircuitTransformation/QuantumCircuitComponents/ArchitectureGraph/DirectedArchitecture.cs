using System;
using System.Collections.Generic;

namespace QuantumCircuitTransformation.QuantumCircuitComponents.ArchitectureGraph
{
    /// <summary>
    ///     DirectedArchitecture
    ///         A class for directed architecture graphs of physical quantum devices.
    ///         In an directed architecture can a CNOT gate only be executed if there 
    ///         is a directed connection from the control qubit to the target qubit.
    /// </summary>
    /// <remarks>    
    ///     @author:   Louis Carpentier
    ///     @version:  1.5
    /// </remarks>
    public class DirectedArchitecture : Architecture
    {
        /// <summary>
        /// See <see cref="Architecture(List{Tuple{int, int}})"/>
        /// </summary>
        public DirectedArchitecture(List<Tuple<int, int>> connections) 
            : base(connections) { }

        /// <summary>
        /// See <see cref="Architecture.HasConnection(int, int)"/>.
        /// </summary>
        /// <returns>
        /// True if and only if there exists a connection in the direction 'from->to'.
        /// </returns>
        public override bool HasConnection(int from, int to)
        {
            return Edges[from].Contains(to);
        }
    }
}