using QuantumCircuitTransformation.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace QuantumCircuitTransformation.DependencyGraphs
{
    /// <summary>
    ///     DependencyGraph
    ///         A dependency graph has a list of gates and a set of
    ///         edges, which connenct the gates that depend on eachother.
    ///         Gate g1 depends on gate g2 if g2 has to be exectuted
    ///         before g1.
    /// </summary>
    /// <remarks>
    ///     @author:   Louis Carpentier
    ///     @version:  1.7
    /// </remarks>
    public class DependencyGraph
    {
        /// <summary>
        /// The gates which must be executed before the gates in the 
        /// corresponding list. Gate i must be executed before gate j, 
        /// if ExecuteBefore[i].contains(j).
        /// </summary>
        public List<List<int>> ExecuteBefore;
        /// <summary>
        /// The gates which must be executed after the gates in the 
        /// corresponding list. Gate i must be executed before gate j, 
        /// if ExecuteAfter[j].contains(i).
        /// </summary>
        public List<List<int>> ExecuteAfter;


        /// <summary>
        /// Initialise a new dependency graph with given gates and dependencies.
        /// </summary>
        /// <param name="executeBefore"> The dependencies of gates to execute before. </param>
        /// <param name="executeAfter"> The dependencies of gates to execute after. </param>
        public DependencyGraph(List<List<int>> executeBefore, List<List<int>> executeAfter)
        {
            ExecuteBefore = executeBefore;
            ExecuteAfter = executeAfter;
        }
    }
}