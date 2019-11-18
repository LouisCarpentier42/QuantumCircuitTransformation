using QuantumCircuitTransformation.QuantumCircuitComponents.Architecture;
using QuantumCircuitTransformation.QuantumCircuitComponents.Gates;
using System;
using System.Collections.Generic;


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
        protected readonly int NbNodes; 
        protected readonly List<Tuple<int,int>> Edges;
        protected readonly int[,] CNOTDistance;


        public ArchitectureGraph(int nbQubits, List<Tuple<int, int>> connections)
        {
            NbNodes = nbQubits;
            Edges = connections;
            
            CNOTDistance = new int[nbQubits, nbQubits];
            int[,] pathLength = ShortestPathFinder.Dijkstra(nbQubits, connections);
            for (int from = 0; from < nbQubits; from++)
                for (int to = 0; to < nbQubits; to++)
                    CNOTDistance[from, to] = ComputeCNOTDistance(pathLength[from, to]);
        }


        public int GetCNOTDistance(int from, int to)
        {
            return CNOTDistance[from, to];
        }
        
        public static bool IsValidNbQubitsAndConnections(int nbQubits, List<Tuple<int, int>> connections)
        {
            throw new NotImplementedException();
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
