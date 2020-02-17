using QuantumCircuitTransformation.QuantumCircuitComponents.Circuit;
using QuantumCircuitTransformation.QuantumCircuitComponents.Gates;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuantumCircuitTransformation.DependencyGraphs
{
    /// <summary>
    ///     DependencyGraphGenerator
    ///         A static class for constructing dependency graphs.
    /// </summary>
    /// <remarks>
    ///     @author:   Louis Carpentier
    ///     @version:  1.4
    /// </remarks>
    public static class DependencyGraphGenerator
    {
        /// <summary>
        /// The gates which must be executed before the gates in the 
        /// corresponding list. Gate i must be executed before gate j, 
        /// if ExecuteBefore[i].contains(j).
        /// </summary>
        private static List<List<int>> ExecuteBefore;
        /// <summary>
        /// The gates which must be executed after the gates in the 
        /// corresponding list. Gate i must be executed before gate j, 
        /// if ExecuteAfter[j].contains(i).
        /// </summary>
        private static List<List<int>> ExecuteAfter;
        /// <summary>
        /// Variable referring to whether or not a path exists between two 
        /// gate ID's. 
        /// </summary>
        private static bool[,] PathExists;



        /// <summary>
        /// Creates a dependency graph for the given gates, based on
        /// the given rules. 
        /// </summary>
        /// <param name="circuit"> The logical circuit to create the dependency graph for. </param>
        /// <param name="rules"> The rules for the dependencies. </param>
        /// <returns>
        /// A new dependency graph with the given gates and connections
        /// according to the given rules. 
        /// </returns>
        public static DependencyGraph Generate(LogicalCircuit circuit, List<DependencyRule> rules = null)
        {
            SetUp(circuit);

            for (int to = 1; to < circuit.NbGates; to++)
            {
                for (int from = to - 1; from >= 0; from--)
                {
                    if (!PathExists[from, to] && GatesAreDependent(circuit.Gates[from], circuit.Gates[to], rules))
                    {
                        ExecuteBefore[from].Add(to);
                        ExecuteAfter[to].Add(from);
                        UpdateExistingPaths(from, to);
                    }
                }
            }

            PathExists = null;
            GC.Collect();
            return new DependencyGraph(ExecuteBefore, ExecuteAfter);
        }

        /// <summary>
        /// Setup all the parameters for the generation of the dependency graph
        /// for the given circuit. 
        /// </summary>
        /// <param name="circuit"> The circuit to generate a dependency graph for. </param>
        private static void SetUp(LogicalCircuit circuit)
        {
            ExecuteBefore = new List<List<int>>();
            ExecuteAfter = new List<List<int>>();
            PathExists = new bool[circuit.NbGates, circuit.NbGates];
            for (int i = 0; i < circuit.NbGates; i++)
            {
                ExecuteBefore.Add(new List<int>());
                ExecuteAfter.Add(new List<int>());
                PathExists[i, i] = true;
            }
        }

        /// <summary>
        /// Checks if two gates are dependent based on their overlapping gate parts
        /// and the dependency rule. 
        /// </summary>
        /// <param name="gate1"> The first gate in the comparisson. </param>
        /// <param name="gate2"> The second gate in the comparisson. </param>
        /// <param name="rules"> The dependency rules to take into account. </param>
        /// <returns>
        /// True if and only if there are overlapping gate parts and no rule allows 
        /// to switch the gates. 
        /// </returns>
        private static bool GatesAreDependent(Gate gate1, Gate gate2, List<DependencyRule> rules)
        {
            List<GatePart> overlappingGateParts = GetOverlappingGateParts(gate1, gate2);
            return overlappingGateParts.Count > 0 && (rules == null || !rules.Any(rule => rule.CanBeSwitched(overlappingGateParts)));
        }

        /// <summary>
        /// Returns the overlapping gate parts of the given gates.
        /// </summary>
        /// <param name="gate1"> The first gate. </param>
        /// <param name="gate2"> The second gate. </param>
        /// <returns>
        /// A hashset of the gate parts which overlap.
        /// </returns>
        public static List<GatePart> GetOverlappingGateParts(Gate gate1, Gate gate2)
        {
            IEnumerable<int> overlappingQubits = gate1.GetQubits().Intersect(gate2.GetQubits());
            List<GatePart> overlappingGateParts = new List<GatePart>();
            foreach (int qubit in overlappingQubits)
            {
                overlappingGateParts.Add(gate1.GetGatePart(qubit));
                overlappingGateParts.Add(gate2.GetGatePart(qubit));
            }
            overlappingGateParts.Sort();
            return overlappingGateParts;
        }

        /// <summary>
        /// Updates the existing paths 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        private static void UpdateExistingPaths(int from, int to)
        {
            PathExists[from, to] = true; 
            for (int i = 0; i < to; i++)
            {
                PathExists[i, to] = PathExists[i, to] || PathExists[i, from]; 
            }
        }
    }
} 
