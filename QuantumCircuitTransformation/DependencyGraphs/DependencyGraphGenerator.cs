using QuantumCircuitTransformation.DependencyGraphs.DependencyRules;
using QuantumCircuitTransformation.QuantumCircuitComponents.Gates;
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
    ///     @version:  1.0
    /// </remarks>
    public static class DependencyGraphGenerator
    {

        public static DependencyGraph DependencyGraphMaker(List<PhysicalGate> gates, List<DependencyRule> rules)
        {
            List<Tuple<int, int>> edges = new List<Tuple<int, int>>();
            
            for (int i = 0; i < gates.Count; i++)
            {
                for (int j = i + 1; j < gates.Count; j++)
                {
                    if (rules.Any(rule => rule.MustBeExecutedBefore(gates[i], gates[j])))
                    {
                        edges.Add(new Tuple<int, int>(i, j));
                    }
                }
            }

            return new DependencyGraph(new List<PhysicalGate>(gates), edges);
        }




    }
}
