using QuantumCircuitTransformation.QuantumCircuitComponents.Gates;
using System;
using System.Collections.Generic;
using System.Linq;
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
    ///     @version:  1.1
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
        /// A list to keep track of all the gates which have been resolved already. 
        /// </summary>
        private List<int> ResolvedGates;
        /// <summary>
        /// A list to keep track of all the gates which are not blocked by another gate.
        /// </summary>
        private List<int> BlockingGates;
        /// <summary>
        /// A list to keep track of all the gates which are blocked by some other gate. 
        /// </summary>
        private List<int> BlockedGates;


        /// <summary>
        /// Initialise a new dependency graph with given gates and dependencies.
        /// </summary>
        /// <param name="gates"> The gates for this dependency graph. </param>
        /// <param name="dependencyEdges"> The edges for this dependency graph. </param>
        public DependencyGraph(List<PhysicalGate> gates, List<Tuple<int, int>> dependencyEdges)
        {
            Gates = gates;
            DependencyEdges = dependencyEdges;
            SetUpGates(gates, dependencyEdges);
        }

        /// <summary>
        /// Setup of the resolved, blocking and blocked gates. 
        /// </summary>
        /// <param name="gates"> The gates which should be set in the different lists. </param>
        /// <param name="dependencyEdges"> The dependency edges to sort the gates with. </param>
        private void SetUpGates(List<PhysicalGate> gates, List<Tuple<int, int>> dependencyEdges)
        {
            ResolvedGates = new List<int>();
            BlockingGates = new List<int>();
            BlockedGates = new List<int>();
            for (int i = 0; i < gates.Count; i++)
            {
                if (dependencyEdges.Any(edge => edge.Item2 == 1))
                    BlockedGates.Add(i);
                else
                    BlockingGates.Add(i);
            }
        }



    }
}
