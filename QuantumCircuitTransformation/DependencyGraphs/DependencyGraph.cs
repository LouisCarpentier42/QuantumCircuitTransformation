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
    ///     @version:  1.6
    /// </remarks>
    public class DependencyGraph
    {
        /// <summary>
        /// The different states in which a gate can be. 
        /// </summary>
        //private enum GateState
        //{
        //    Resolved,
        //    Blocking,
        //    Blocked
        //}

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
        /// Variable referring to the state of each gate. 
        /// </summary>
        //private List<GateState> GateStates;


        /// <summary>
        /// Initialise a new dependency graph with given gates and dependencies.
        /// </summary>
        /// <param name="nbGates"> The number of gates for this dependency graph. </param>
        /// <param name="executeBefore"> The dependencies of gates to execute before. </param>
        /// <param name="executeAfter"> The dependencies of gates to execute after. </param>
        public DependencyGraph(int nbGates, List<List<int>> executeBefore, List<List<int>> executeAfter)
        {
            ExecuteBefore = executeBefore;
            ExecuteAfter = executeAfter;
            //SetUpGateStates(nbGates);
        }

        ///// <summary>
        ///// Set the states of the gates correctly. 
        ///// </summary>
        ///// <param name="nbGates"> The number of gates for this dependency graph. </param>
        //private void SetUpGateStates(int nbGates)
        //{
        //    GateStates = new List<GateState>();
        //    for (int i = 0; i < nbGates; i++)
        //    {
        //        if (IsNotBlocked(i))
        //            GateStates.Add(GateState.Blocking);
        //        else
        //            GateStates.Add(GateState.Blocked);
        //    }
        //}


        /// <summary>
        /// Resolve a given gate from this dependency graph and update the 
        /// resolved, blocking and blocked gate. 
        /// </summary>
        /// <param name="gateID"> The ID of the gate to resolve. </param>
        /// <exception cref="GateIsNotBlockingException"> If the given gate is not in the blocking state. </exception>
        //private void ResolveGate(int gateID)
        //{
        //    if (GateStates[gateID] != GateState.Blocking)
        //        throw new GateIsNotBlockingException();
        //    GateStates[gateID] = GateState.Resolved;

        //    foreach (int i in ExecuteBefore[gateID])
        //    {
        //        ExecuteAfter[i].Remove(gateID);
        //        if (IsNotBlocked(i) && GateStates[i] == GateState.Blocked)
        //        {
        //            GateStates[i] = GateState.Blocking;
        //        }
        //    }

        //    ExecuteBefore[gateID] = null;
        //}

        ///// <summary>
        ///// Checks if the gate at the given gateID is blocked.
        ///// </summary>
        ///// <param name="gateID"> The ID of the gate to check. </param>
        ///// <returns>
        ///// True if and only if the given gate ID has no gates after which it 
        ///// must be executed. 
        ///// </returns>
        //private bool IsNotBlocked(int gateID)
        //{
        //    return ExecuteAfter[gateID] == null;
        //}
    }
}