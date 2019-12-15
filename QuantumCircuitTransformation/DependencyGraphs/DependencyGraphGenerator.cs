﻿using QuantumCircuitTransformation.QuantumCircuitComponents.Gates;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuantumCircuitTransformation.DependencyGraphs
{
    /// <summary>
    ///     DependencyGraphGenerator:
    ///         A static class for constructing dependency graphs.
    /// </summary>
    /// <remarks>
    ///     @author:   Louis Carpentier
    ///     @version:  1.1
    /// </remarks>
    public static class DependencyGraphGenerator
    {
        /// <summary>
        /// Creates a dependency graph for the given gates, based on
        /// the given rules. 
        /// </summary>
        /// <param name="gates"> The gates for the dependency graph. </param>
        /// <param name="rules"> The rules for the dependencies. </param>
        /// <returns>
        /// A new dependency graph with the given gates and connections
        /// according to the given rules. 
        /// </returns>
        public static DependencyGraph DependencyGraphMaker(List<PhysicalGate> gates, List<DependencyRule> rules)
        {
            List<Tuple<int, int>> edges = new List<Tuple<int, int>>();
            for (int i = 0; i < gates.Count; i++)
            {
                for (int j = i + 1; j < gates.Count; j++)
                {
                    List<GatePart> overlappingGateParts = GetOverlappingGateParts(gates[i], gates[j])
;                   if (rules.Any(rule => rule.CanBeSwitched(overlappingGateParts)))
                    {
                        edges.Add(new Tuple<int, int>(i, j));
                    }
                }
            }
            return new DependencyGraph(new List<PhysicalGate>(gates), edges);
        }

        /// <summary>
        /// Returns the overlapping gate parts of the given gates.
        /// </summary>
        /// <param name="gate1"> The first gate. </param>
        /// <param name="gate2"> The second gate. </param>
        /// <returns>
        /// A hashset of the gate parts which overlap.
        /// </returns>
        public static List<GatePart> GetOverlappingGateParts(PhysicalGate gate1, PhysicalGate gate2)
        {
            List<int> overlappingQubits = gate1.GetQubits().Intersect(gate2.GetQubits()).ToList();
            List<GatePart> overlappingGateParts = new List<GatePart>();
            foreach (int qubit in overlappingQubits)
            {
                overlappingGateParts.Add(gate1.GetGatePart(qubit));
                overlappingGateParts.Add(gate2.GetGatePart(qubit));
            }
            overlappingGateParts.Sort();
            return overlappingGateParts;
        }
    }
}
