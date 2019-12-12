using QuantumCircuitTransformation.QuantumCircuitComponents.Gates;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuantumCircuitTransformation.DependencyGraphs
{
    public class DependencyGraph
    {
        public readonly List<PhysicalGate> Gates;
        public readonly List<Tuple<int, int>> DependencyEdges;


        public DependencyGraph(List<PhysicalGate> gates, List<Tuple<int, int>> dependencyEdges)
        {
            Gates = gates;
            DependencyEdges = dependencyEdges;
        }

    }
}
