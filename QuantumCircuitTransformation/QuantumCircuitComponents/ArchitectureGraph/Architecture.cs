using QuantumCircuitTransformation.QuantumCircuitComponents.Gates;
using QuantumCircuitTransformation.QuantumCircuitComponents.Gates.PhysicalGates;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuantumCircuitTransformation.QuantumCircuitComponents.ArchitectureGraph
{
    /// <summary>
    ///     ArchitectureGraph:
    ///         An abstract class for the architecture graph of 
    ///         some quantum device.
    /// </summary>
    /// <remarks>
    ///     @Author:   Louis Carpentier
    ///     @Version:  2.3
    /// </remarks>
    public abstract class Architecture
    {
        /// <summary>
        /// The edges between the different nodes in this architecture. 
        /// </summary>
        protected readonly List<Tuple<int, int>> Edges;
        /// <summary>
        /// The number of nodes in this architecture. 
        /// </summary>
        public int NbNodes { get; private set; }
        /// <summary>
        /// The CNOT distance between all pairs of nodes in this architecture.  
        /// </summary>
        /// <remarks>
        /// The CNOT distance between two qubuts is the number of gates that should be 
        /// added to make sure that a CNOT gate can be executed from the first to the 
        /// second qubit. 
        /// </remarks>
        public int[,] CNOTDistance { get; private set; }


        /// <summary>
        /// Initialise a new architecture graph of a quantum device.
        /// </summary>
        /// <param name="connections"> The connections in this architecture graph. </param>
        /// <remarks>
        /// First, the edges are normalised (see <see cref="NormaliseEdges"/>), then the
        /// number of nodes is set and the CNOT distances are computed. 
        /// </remarks>
        public Architecture(List<Tuple<int, int>> connections)
        {
            Edges = connections;
            NormaliseEdges();
            SetNbNodes();
            SetCNOTDistance();
        }

        /// <summary>
        /// Normalise all the edges in this architecture graph. This is making 
        /// sure that all no nodes are skipped (and thus never used) while some
        /// greater ID's are used. 
        /// </summary>
        /// <remarks>
        /// The edges are normalised by first getting all the different ID's and 
        /// sort them. Next can all ID's be replaced by their index in the sorted
        /// array. This makes sure that the order of the different ID's remains 
        /// the same. 
        /// </remarks>
        private void NormaliseEdges()
        {
            HashSet<int> allNodeIDs = new HashSet<int>();
            allNodeIDs.UnionWith(Edges.Select(x => x.Item1));
            allNodeIDs.UnionWith(Edges.Select(x => x.Item2));

            List<int> sortedNodeIDs = allNodeIDs.ToList();
            sortedNodeIDs.Sort();

            for (int i = 0; i < Edges.Count(); i++)
            {
                Edges[i] = new Tuple<int, int>(
                    sortedNodeIDs.IndexOf(Edges[i].Item1),
                    sortedNodeIDs.IndexOf(Edges[i].Item2)
                );
            }
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
        /// Sets the cnot distances of this architecture graph. 
        /// </summary>
        private void SetCNOTDistance()
        {
            CNOTDistance = new int[NbNodes, NbNodes];
            int[,] pathLength = ShortestPathFinder.Dijkstra(NbNodes, Edges);
            for (int i = 0; i < NbNodes; i++)
                for (int j = 0; j < NbNodes; j++)
                    CNOTDistance[i, j] = Math.Max(0, NbOfCnotGatesPerSwap() * (pathLength[i, j] - 1));
        }


        /// <summary>
        /// Returns the number of swap gates that is needed to replace one 
        /// swap gate in this architecture. 
        /// </summary>
        public abstract int NbOfCnotGatesPerSwap();

        /// <summary>
        /// Check whether or not a CNOT gate with given control and target qubit 
        /// can be executed on this architecture. 
        /// </summary>
        /// <param name="cnot"> The CNOT gate to check. </param>
        /// <returns>
        /// True if and only if there exists a connection in the architecture 
        /// such that <paramref name="cnot"/> can be executed. 
        /// </returns>
        public abstract bool CanExecuteCNOT(CNOT cnot);
    }
}