using QuantumCircuitTransformation.Exceptions;
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
    ///     @version:  1.5
    /// </remarks>
    public class DependencyGraph
    {
        /// <summary>
        /// The different states in which a gate can be. 
        /// </summary>
        private enum GateState
        {
            Resolved,
            Blocking,
            Blocked
        }

        /// <summary>
        /// The dependency edges of this dependency graph. A connection (i,j) 
        /// means that <see cref="Gates"/>[j] depends on <see cref="Gates"/>[i].
        /// </summary>
        public readonly List<Tuple<int, int>> DependencyEdges;
        /// <summary>
        /// Variable referring to the state of each gate. 
        /// </summary>
        private List<GateState> GateStates;


        /// <summary>
        /// Initialise a new dependency graph with given gates and dependencies.
        /// </summary>
        /// <param name="nbGates"> The number of gates for this dependency graph. </param>
        /// <param name="dependencyEdges"> The edges for this dependency graph. </param>
        public DependencyGraph(int nbGates, List<Tuple<int, int>> dependencyEdges)
        {
            DependencyEdges = dependencyEdges;
            SetUpGateStates(nbGates, dependencyEdges);
        }

        /// <summary>
        /// Set the states of the gates correctly. 
        /// </summary>
        /// <param name="nbGates"> The number of gates for this dependency graph. </param>
        /// <param name="dependencyEdges"> The dependency edges of this dependency graph. </param>
        private void SetUpGateStates(int nbGates, List<Tuple<int, int>> dependencyEdges)
        {
            GateStates = new List<GateState>();
            for (int i = 0; i < nbGates; i++)
            {
                if (IsNotBlocked(i))
                    GateStates[i] = GateState.Blocking;
                else
                    GateStates[i] = GateState.Blocked;
            }
        }


        /// <summary>
        /// Resolve a given gate from this dependency graph and update the 
        /// resolved, blocking and blocked gate. 
        /// </summary>
        /// <param name="gateID"> The ID of the gate to resolve. </param>
        /// <exception cref="GateIsNotBlockingException"> If the given gate is not in the blocking state. </exception>
        private void ResolveGate(int gateID)
        {
            if (GateStates[gateID] != GateState.Blocking)
                throw new GateIsNotBlockingException();
            GateStates[gateID] = GateState.Resolved;

            for (int i = 0; i < GateStates.Count; i++)
            {
                if (GateStates[i] == GateState.Blocked && IsNotBlocked(i))
                {
                    GateStates[i] = GateState.Blocking;
                }
            }
        }

        /// <summary>
        /// Checks if the gate at the given gateID is blocked.
        /// </summary>
        /// <param name="gateID"> The ID of the gate to check. </param>
        /// <returns>
        /// True if and only if the gate at the given ID has only edges directed 
        /// to it from gates which are resolved. 
        /// </returns>
        private bool IsNotBlocked(int gateID)
        {
            return !DependencyEdges.Any(edge => edge.Item2 == gateID && GateStates[edge.Item1] != GateState.Resolved);
        }
    }
}