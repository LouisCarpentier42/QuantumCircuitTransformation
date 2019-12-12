using QuantumCircuitTransformation.QuantumCircuitComponents.Gates;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuantumCircuitTransformation.DependencyGraphs
{
    /// <summary>
    ///     DependencyGraph:
    ///         A dependency graph has a list of gates and a set of
    ///         edges, which connenct the gates that depend on eachother.
    ///         Gate g1 depends on gate g2 if g2 has to be exectuted
    ///         before g1.
    /// </summary>
    /// <remarks>
    ///     @author:   Louis Carpentier
    ///     @version:  1.0
    /// </remarks>
    public class DependencyGraph
    {
        /// <summary>
        /// A list of all the gates in this dependency graph. 
        /// </summary>
        public readonly List<PhysicalGate> Gates;
        /// <summary>
        /// The dependency edges of this dependency graph. A connection (i,j) 
        /// means that <see cref="Gates"/>[j] depends on <see cref="Gates"/>[i].
        /// </summary>
        public readonly List<Tuple<int, int>> DependencyEdges;


        /// <summary>
        /// Initialise a new dependency graph with given gates and dependency edges. 
        /// </summary>
        /// <param name="gates"> The gates in this dependency graph. </param>
        /// <param name="dependencyEdges"> The dependency edges of the gates. </param>
        public DependencyGraph(List<PhysicalGate> gates, List<Tuple<int, int>> dependencyEdges)
        {
            Gates = gates;
            DependencyEdges = dependencyEdges;
        }

    }
}
