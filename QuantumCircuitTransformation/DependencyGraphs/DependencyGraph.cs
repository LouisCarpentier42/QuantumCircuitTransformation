using QuantumCircuitTransformation.Exceptions;
using QuantumCircuitTransformation.QuantumCircuitComponents.Circuit;
using System;
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
        /// The dependency graph this dependency graph refers to. 
        /// </summary>
        public readonly LogicalCircuit Circuit;

        /// <summary>
        /// Initialise a new dependency graph with given gates and dependencies.
        /// </summary>
        /// <param name="executeBefore"> The dependencies of gates to execute before. </param>
        /// <param name="executeAfter"> The dependencies of gates to execute after. </param>
        /// <param name="circuit"> The logical circuit this dependency graph should refer to. </param>
        public DependencyGraph(List<List<int>> executeBefore, List<List<int>> executeAfter, LogicalCircuit circuit)
        {
            ExecuteBefore = executeBefore;
            ExecuteAfter = executeAfter;
            Circuit = circuit;
        }

        /// <summary>
        /// Checks if the given gateID can be executed.
        /// </summary>
        /// <param name="gateID"> The gate ID to check. </param>
        /// <returns>
        /// True if and only if there are no gates remaining which must be
        /// executed before the given gateID.
        /// </returns>
        public bool CanBeExecuted(int gateID)
        {
            return ExecuteAfter[gateID].Count == 0;
        }

        /// <summary>
        /// Changes the <see cref="ExecuteBefore"/> and <see cref="ExecuteAfter"/>
        /// properties of this Dependency graph, in such way as if the given gateID
        /// was executed and thus added to the physical circuit. 
        /// </summary>
        /// <param name="gateID"> The gate id to simulate an execution. </param>
        internal void SimulateExecution(int gateID)
        {
            foreach (int gateToExecuteAfter in ExecuteBefore[gateID])
                ExecuteAfter[gateToExecuteAfter].Remove(gateID);
            ExecuteBefore[gateID] = null;
        }
    }
}