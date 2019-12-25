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
        /// Variables referring to the parameters of the dependency graph
        /// to generate. 
        /// </summary>
        private static List<List<int>> Dependencies;

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
            Dependencies = new List<List<int>>();
            List<List<int>> executeBefore = new List<List<int>>();
            List<List<int>> executeAfter = new List<List<int>>();
            for (int i = 0; i < circuit.NbGates; i++)
            {
                Dependencies.Add(new List<int>());
                executeBefore.Add(new List<int>());
                executeAfter.Add(new List<int>());
            }
            for (int i = 0; i <circuit.NbGates; i++)
            {
                for (int j = i+1; j < circuit.NbGates; j++)
                {
                    List<GatePart> overlappingGateParts = GetOverlappingGateParts(circuit.Gates[i], circuit.Gates[j]);
                    if (GatesAreDependent(overlappingGateParts, rules))
                    {
                        Dependencies[i].Add(j);
                    }
                }
            }


            Console.WriteLine("Dependencies are set");
            int nbDependencies = 0;
            for (int i = 0; i < circuit.NbGates; i++)
            {
                nbDependencies += Dependencies[i].Count();
            }
            Console.WriteLine("In total there are {0} dependencies", nbDependencies);


            //for (int i = 0; i < circuit.NbGates; i++)
            //{
            //    Console.WriteLine(i);
            //    foreach (int j in Dependencies[i])
            //    {
            //        if (!PathExists(i, j, i))
            //        {
            //            executeBefore[i].Add(j);
            //            executeAfter[j].Add(i);
            //        }
            //    }
            //}


            Dependencies = null;
            GC.Collect();
            return new DependencyGraph(circuit.NbGates, executeBefore, executeAfter);
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
        /// Checks if two gates are dependent based on their overlapping gate parts
        /// and the dependency rule. 
        /// </summary>
        /// <param name="overlappingGateParts"> The overlapping gate parts. </param>
        /// <param name="rules"> The dependency rules to take into account. </param>
        /// <returns>
        /// True if and only if there are overlapping gate parts and no rule allows 
        /// to switch the gates. 
        /// </returns>
        private static bool GatesAreDependent(List<GatePart> overlappingGateParts, List<DependencyRule> rules)
        {
            return overlappingGateParts.Count > 0 && (rules == null || !rules.Any(rule => rule.CanBeSwitched(overlappingGateParts)));
        }

        private static bool PathExists(int start, int goal, int forbiddenStart)
        {
            if (start == goal)
                return true;
            else if (start > goal)
                return false;
            else
                return Dependencies[start].Any(nextNode => !(start == forbiddenStart && nextNode == goal) && PathExists(nextNode, goal, forbiddenStart));
        }
    }
} 
