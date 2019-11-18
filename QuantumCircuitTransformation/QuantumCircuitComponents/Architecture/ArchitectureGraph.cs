using System;
using System.Collections.Generic;
using System.Linq;

namespace QuantumCircuitTransformation.QuantumCircuitComponents.Architecture
{
    /// <summary>
    /// 
    /// ArchitectureGraph:
    ///    An abstract class for the architecture graph of some quantum device.
    ///    
    /// @Author:   Louis Carpentier
    /// @Version:  1.2
    /// 
    /// </summary>
    public abstract class ArchitectureGraph
    {
        /// <summary>
        /// The edges between the different nodes in this architecture. 
        /// </summary>
        protected readonly List<Tuple<int,int>> Edges;
        /// <summary>
        /// The number of nodes in this architecture. 
        /// </summary>
        private int NbNodes;
        /// <summary>
        /// The CNOT distance between all pairs of nodes in this architecture.  
        /// </summary>
        /// <remarks>
        /// The CNOT distance between two qubuts is the number of gates that should be 
        /// added to make sure that a CNOT gate can be executed from the first to the 
        /// second qubit. 
        /// </remarks>
        private int[,] CNOTDistance;

        /// <summary>
        /// Initialise a new architecture graph of a quantum device.
        /// </summary>
        /// <param name="connections"> The connections in this architecture graph. </param>
        /// <remarks>
        /// First, the edges are normalised (see <see cref="NormaliseEdges"/>), then the
        /// number of nodes is set and the CNOT distances are computed. 
        /// </remarks>
        public ArchitectureGraph(List<Tuple<int, int>> connections)
        {
            Edges = connections;
            NormaliseEdges();
            SetNbNodes();
            SetCNOTDistance();
        }

        /// <summary>
        /// Get the CNOT distance between the given control and target qubit. 
        /// </summary>
        /// <param name="controlQubit"> The ID of the control qubit. </param>
        /// <param name="targetQubit"> The ID of the target qubit. </param>
        public int GetCNOTDistance(int controlQubit, int targetQubit)
        {
            return CNOTDistance[controlQubit, targetQubit];
        }

        
        private void NormaliseEdges()
        {

            throw new NotImplementedException();
        }

        /// <summary>
        /// Set the number of nodes to the highest used ID in <see cref="Edges"/> added with one. 
        /// </summary>
        /// <remarks> 
        /// The edges should be normalised first (see <see cref="NormaliseEdges"/>). 
        /// </remarks>
        private void SetNbNodes()
        {
            NbNodes = Edges.Max(x => Math.Max(x.Item1, x.Item2)) + 1;
        }

        /// <summary>
        /// Computes all the CNOT distances and set them in <see cref="CNOTDistance"/>.
        /// </summary>
        private void SetCNOTDistance()
        {
            CNOTDistance = new int[NbNodes, NbNodes];
            int[,] pathLength = ShortestPathFinder.Dijkstra(NbNodes, Edges);
            for (int from = 0; from < NbNodes; from++)
                for (int to = 0; to < NbNodes; to++)
                    CNOTDistance[from, to] = ComputeCNOTDistance(pathLength[from, to]);
        }

        /// <summary>
        /// Check whether or not a CNOT gate with given control and target qubit 
        /// can be executed on this architecture. 
        /// </summary>
        /// <param name="cnot"> The CNOT gate to check. </param>
        /// <returns>
        /// True if and only if there exists a connection in the architecture 
        /// from <paramref name="control"/> to <paramref name="target"/>.
        /// </returns>
        public abstract bool CanExecuteCNOT(CNOT cnot);

        /// <summary>
        /// Computes the CNOT distance between two given nodes.
        /// </summary>
        /// <param name="shortestPathLength"> The shortest path between the two nodes. </param>
        /// <returns>
        /// The CNOT distance between <paramref name="from"/> and <paramref name="to"/>. This is 
        /// number of extra gates that need to be added to make sure a CNOT gate with from as the 
        /// control qubit and to as the target qubit can be executed. 
        /// </returns>
        protected abstract int ComputeCNOTDistance(int shortestPathLength);
    }
}
