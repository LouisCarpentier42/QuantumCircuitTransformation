using System;
using System.Collections.Generic;
using System.Text;

namespace QuantumCircuitTransformation.QuantumCircuitComponents
{
    /// <summary>
    /// 
    /// ArchitectureGraph:
    ///    An abstract class for the architecture graph of some quantum computer.
    ///    
    /// @Author:   Louis Carpentier
    /// @Version:  1.0
    /// 
    /// </summary>
    public abstract class ArchitectureGraph
    {
        private readonly int NbNodes; 
        private readonly List<Tuple<int,int>> Edges;
        private readonly int[,] CnotDistance;


        public ArchitectureGraph(int nbNodes, List<Tuple<int, int>> connections)
        {
            NbNodes = nbNodes;
            Edges = connections;
        }






        // Baiscly the formula for the directed and undirected graph
        protected abstract int GetCnotDistance(int shortestPathLength);
        




        


        
    }
}
