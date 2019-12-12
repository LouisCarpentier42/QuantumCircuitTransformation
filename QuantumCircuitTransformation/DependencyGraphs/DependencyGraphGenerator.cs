using QuantumCircuitTransformation.QuantumCircuitComponents.Gates;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuantumCircuitTransformation.DependencyGraphs
{
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
                    if (true) // TODO dependencyrules
                        edges.Add(new Tuple<int, int>(i, j));
                    // Remove redundant edges. 
                }
            }

            return new DependencyGraph(new List<PhysicalGate>(gates), edges);
        }



    }
}
