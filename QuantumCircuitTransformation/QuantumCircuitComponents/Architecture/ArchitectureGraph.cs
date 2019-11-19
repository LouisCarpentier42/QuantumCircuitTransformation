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
    /// @Version:  1.4
    /// 
    /// </summary>
    public abstract class ArchitectureGraph
    {
        /// <summary>
        /// The edges between the different nodes in this architecture. 
        /// </summary>
        protected readonly List<Tuple<int, int>> Edges;
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
        /// Computes all the CNOT distances and set them in <see cref="CNOTDistance"/>.
        /// </summary>
        private void SetCNOTDistance()
        {
            CNOTDistanceCalculator.Dijkstra(this);
        }

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

        /// <summary>
        /// Computes the CNOT distance between two given nodes.
        /// </summary>
        /// 
        /// <returns>
        /// The CNOT distance between <paramref name="control"/> and <paramref name="Target"/>. 
        /// This is number of extra gates that need to be added to make sure a CNOT gate with 
        /// from as the control qubit and to as the target qubit can be executed. 
        /// </returns>
        protected abstract int ComputeCNOTDistance(int control, int target, int Source);





        /// <summary>
        /// 
        /// ShortestPathFinder: 
        ///    A static class to find the shortest path in an unweighted, undirected
        ///    graph. The shortest path lengt between two nodes equals the minimal 
        ///    number of edges to connect the two nodes. 
        ///    
        /// @Author:   Louis Carpentier
        /// @Version:  1.0
        /// 
        /// </summary>
        public static class CNOTDistanceCalculator
        {
            private static ArchitectureGraph Architecture;
            /// <summary>
            /// Variable to keep track of the shortest path between two nodes.  
            /// The shortest path between the nodes i and j is at entry [i,j].
            /// </summary>
            // private static int[,] PathLength;
            /// <summary>
            /// Variable to keep track of the node from which all shortest paths
            /// is being computed. 
            /// </summary>
            private static int Source;
            /// <summary>
            /// Variable to keep track of all the nodes that have been expanded. 
            /// </summary>
            private static bool[] Expanded;
            /// <summary>
            /// Variable to keep track of the nodes from which the shortest path 
            /// has been found, starting from <see cref="Source"/>
            /// </summary>
            private static bool[] PathFound;
            /// <summary>
            /// Variable to keep track of the node that is being expanded int the algorithm. 
            /// </summary>
            private static int CurrentNode;
            /// <summary>
            /// Variable to keep track of the neighbours of <see cref="CurrentNode"/>.
            /// </summary>
            private static List<int> ValidNeighboursOfCurrentNode;


            /// <summary>
            /// Executes the Dijkstra-algorithm to find all shortest paths in the graph. 
            /// </summary>
            /// <param name="architecture"> The architecture to compute the CNOT distance from. </param>
            /// <remarks>
            /// To calculate the path from a source to all other nodes is an path length 
            /// of infinity given to each node and a path length of 0 to the source. The 
            /// algorithm will select a node which has not been expanded and which has 
            /// the shortest path. This node is expanded by setting the path to it's
            /// neighbours as one more of it's own path. The shortest path to these nodes
            /// has been found now. It can be proven that these are the shortest paths by
            /// induction. This process continues untill a path has been found from the 
            /// source to all other nodes. 
            /// Once a node has been used as source, it doesn't need to be used in the 
            /// computations of the other nodes cause the shortest path from a to be is 
            /// equal to the shortest path from b to a. There is iterated over the node
            /// ID's from high to low cause this simplifies the use of the <see cref="Expanded"/>
            /// and <see cref="PathFound"/> variables. 
            /// Note that this is an adjustment of the traditional algorithm. Normally, 
            /// the Dijkstra-algorithm finds the shortest path between to nodes. This 
            /// algorithm finds the shortest path between each pair of nodes.
            /// </remarks>
            public static void Dijkstra(ArchitectureGraph architecture)
            {
                SetUp(architecture);
                while (Source > 0)
                {
                    SetSource();
                    Console.WriteLine("---- SOURCE: " + Source + "----");
                    while (PathFound.Contains(false))
                    {
                        
                        // Choose a new node to expand and set it's neighbours
                        SetCurrentNode();
                        SetValidNeighbours();
                        Console.WriteLine("CURRENT NODE: " + CurrentNode);
                        foreach (int neighbour in ValidNeighboursOfCurrentNode)
                        {
                            // The shortest path length from the source to a node equals the 
                            // shortest path length of all neighbour but incremented
                            Architecture.CNOTDistance[Source, neighbour]
                                = Architecture.CNOTDistance[Source, CurrentNode]
                                  + Architecture.ComputeCNOTDistance(CurrentNode, neighbour, Source);
                            Console.WriteLine("Cost from " + Source + " to " + neighbour + " equals to " + Architecture.CNOTDistance[Source, neighbour]);
                            // Architecture.CNOTDistance[neighbour, Source] = Architecture.CNOTDistance[CurrentNode, Source] + 1;
                            // The path to the neighbour has been found
                            PathFound[neighbour] = true;
                        }
                        Console.WriteLine("----");
                        // The current node has been expanded
                        Expanded[CurrentNode] = true;
                    }
                }
                Console.WriteLine("Done");
            }

            /// <summary>
            /// Set the variables ready for the algorithm. 
            /// </summary>
            /// <param name="architecture"> The architecture to compute the CNOT distances from. </param>
            /// <remarks>
            /// The <paramref name="architecture"/> is set to the given architecture
            /// graph.
            /// The <see cref="CNOTDistance"/> variable is set to a square matrix of size  
            /// equal to the number of nodes in <paramref name="architecture"/>. 
            /// The source node is also set to this number. 
            /// </remarks>
            private static void SetUp(ArchitectureGraph architecture)
            {
                Architecture = architecture;

                architecture.CNOTDistance = new int[architecture.NbNodes, architecture.NbNodes];
                for (int row = 0; row < architecture.NbNodes; row++)
                    for (int Column = 0; Column < architecture.NbNodes; Column++)
                        architecture.CNOTDistance[row, Column] = int.MaxValue;

                Source = architecture.NbNodes; Console.WriteLine(Source);
            }

            /// <summary>
            /// Set <see cref="Source"/> and it's related parameters. 
            /// </summary>
            /// <remarks>
            /// The ID of the source node is decremented, which means that the next node
            /// can be used as source. 
            /// All the nodes with an ID less than or equal to the source are not expanded 
            /// and has no path been found for yet. 
            /// The path from the source to the source equals 0 and thus is this path the 
            /// first path found.  
            /// </remarks>
            private static void SetSource()
            {
                Source--;
                
                Expanded = new bool[Source + 1];
                PathFound = new bool[Source + 1];

                Architecture.CNOTDistance[Source, Source] = 0;
                PathFound[Source] = true;
            }

            /// <summary>
            /// Set <see cref="CurrentNode"/> to the node with the smallest CNOT distance and 
            /// which has not been expanded yet. 
            /// </summary>
            private static void SetCurrentNode()
            {
                CurrentNode = Expanded.ToList().IndexOf(false);
                for (int node = CurrentNode + 1; node <= Source; node++)
                    if (!Expanded[node] && Architecture.CNOTDistance[Source, node] < Architecture.CNOTDistance[Source, CurrentNode])
                        CurrentNode = node;
                
            }

            /// <summary>
            /// Set the valid neighbours of <see cref="CurrentNode"/> in <see cref="ValidNeighboursOfCurrentNode"/>. 
            /// </summary>
            /// <param name="edges"> The edges of the graph. </param>
            /// <remarks>
            /// A valid node is connected to <see cref="CurrentNode"/> with an edge in <paramref name="edges"/>
            /// for which no path has been found yet and for which the node ID is smaller than <see cref="Source"/>.
            /// </remarks>
            private static void SetValidNeighbours()
            {
                ValidNeighboursOfCurrentNode = new List<int>();
                foreach (Tuple<int, int> edge in Architecture.Edges)
                {
                    if (edge.Item1 <= Source && edge.Item2 <= Source)
                    {
                        if (edge.Item1 == CurrentNode && !PathFound[edge.Item2])
                            ValidNeighboursOfCurrentNode.Add(edge.Item2);
                        else if (edge.Item2 == CurrentNode && !PathFound[edge.Item1])
                            ValidNeighboursOfCurrentNode.Add(edge.Item1);
                    }
                }
            }
        }
    }
}
