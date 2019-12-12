using QuantumCircuitTransformation.QuantumCircuitComponents.Gates;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuantumCircuitTransformation.DependencyGraphs
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    ///     @author:   Louis Carpentier
    ///     @version:  1.0
    /// </remarks>
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
