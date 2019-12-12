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
                    if (true)
                        edges.Add(new Tuple<int, int>(i, j));
                    // Later remove redundant edges. 
                    // Put depends on in a rule class
                }
            }

            return new DependencyGraph(new List<PhysicalGate>(gates), edges);
        }



    }
}
