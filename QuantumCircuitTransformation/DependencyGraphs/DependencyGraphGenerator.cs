using QuantumCircuitTransformation.QuantumCircuitComponents.Gates;
using System;
using System.Collections.Generic;
using System.Text;

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

        public static DependencyGraph DependencyGraphMaker(List<PhysicalGate> gates)
        {
            List<Tuple<int, int>> edges = new List<Tuple<int, int>>();
            
            for (int i = 0; i < gates.Count; i++)
            {
                for (int j = i + 1; j < gates.Count; j++)
                {
                    throw new NotImplementedException("Dependency rules must be implemented");
                }
            }

            return new DependencyGraph(new List<PhysicalGate>(gates), edges);
        }



    }
}
