using System;
using System.Collections.Generic;
using System.Linq;

namespace QuantumCircuitTransformation.QuantumCircuitComponents.ArchitectureGraph
{
    /// <summary>
    ///     ShortestPathFinder
    ///         A static class to find the shortest path in an unweighted, undirected
    ///         graph. The shortest path lengt between two nodes equals the minimal 
    ///         number of edges to connect the two nodes. 
    /// </summary>
    /// <remarks>
    ///     @author:   Louis Carpentier
    ///     @version:  2.0
    /// </remarks>
    public static class ShortestPathFinder
    {
        /// <summary>
        /// Variable referring to the shortest path between two nodes. 
        /// </summary>
        private static List<int>[,] Paths;
        /// <summary>
        /// Variable to keep track of the shortest path between two nodes.  
        /// The shortest path between the nodes i and j is at entry [i,j].
        /// </summary>
        private static int[,] PathLength;
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
        /// <param name="edges"> The edges in the graph. </param>
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
        /// ID's from high to low cause this simplifies the use of <see cref="Expanded"/>
        /// and <see cref="PathFound"/>. 
        /// Note that this is an adjustment of the traditional algorithm. Normally, 
        /// the Dijkstra-algorithm finds the shortest path between to nodes. This 
        /// algorithm finds the shortest path between each pair of nodes.
        /// </remarks>
        /// <returns>
        /// The <see cref="PathLength"/> variable, with all shortest paths in it. 
        /// </returns>
        public static int[,] Dijkstra_PathLengths(List<Tuple<int, int>> edges)
        {
            SetUp(edges.Max(edge => Math.Max(edge.Item1, edge.Item2)) + 1);
            while (Source > 0)
            {
                SetSource();
                while (PathFound.Contains(false))
                {
                    SetCurrentNode();
                    SetValidNeighbours(edges);
                    foreach (int neighbour in ValidNeighboursOfCurrentNode)
                    {
                        Paths[Source, neighbour].Add(CurrentNode);
                        PathLength[Source, neighbour] = PathLength[Source, CurrentNode] + 1;
                        Paths[Source, neighbour].Add(CurrentNode);
                        PathLength[neighbour, Source] = PathLength[CurrentNode, Source] + 1;
                        PathFound[neighbour] = true;
                    }
                    Expanded[CurrentNode] = true;
                }
            }
            return PathLength;
        }

        /// <summary>
        /// Executes the Dijkstra algorithm <see cref="Dijkstra_PathLengths(List{Tuple{int, int}})"/>
        /// but finds the specific paths and not only the pathlengths. 
        /// </summary>
        /// <param name="edges"> The edges of the graph to find the paths in. </param>
        /// <returns>
        /// A matrix containing the shortest paths. 
        /// </returns>
        public static List<int>[,] Dijkstra_Paths(List<Tuple<int, int>> edges)
        {
            int maxNode = edges.Max(edge => Math.Max(edge.Item1, edge.Item2));
            Paths = new List<int>[maxNode + 1, maxNode + 1];

            for (int i = 0; i <= maxNode; i++)
                Paths[i, i] = new List<int>();

            for (int source = 0; source < maxNode; source++)
            {
                for (int target = source + 1; target <= maxNode; target++)
                {
                    List<int> s = FindShortestPath(source, target, edges, maxNode);
                    Paths[source, target] = s;
                    Paths[target, source] = s;
                }
            }

            return Paths;
        }

        /// <summary>
        /// Finds the shortest path between the two given nodes. 
        /// </summary>
        /// <param name="from"> The starting node. </param>
        /// <param name="to"> The ending node. </param>
        /// <param name="edges"> The edges of the graph. </param>
        /// <param name="maxNode"> The highest node id. </param>
        /// <returns>
        /// A list containing the shortest path between the two nodes.
        /// </returns>
        private static List<int> FindShortestPath(int from, int to, List<Tuple<int, int>> edges, int maxNode)
        {
            // Setup
            List<int> q = new List<int>();
            int[] dist = new int[maxNode + 1];
            int[] prev = new int[maxNode + 1];
            for (int i = 0; i <= maxNode; i++)
            {
                dist[i] = int.MaxValue;
                prev[i] = -1;
                q.Add(i);
            }
            dist[from] = 0;

            // Find path
            while (q.Count >= 0)
            {
                int u = q[0];
                for (int i = 0; i < q.Count; i++)
                {
                    if (dist[q[i]] < dist[u])
                    {
                        u = q[i];
                    }
                }
                
                if (u == to)
                {
                    break;
                }

                q.Remove(u);

                List<int> neighbours = new List<int>();
                foreach (int x in q)
                {
                    if (edges.Any(edge => (edge.Item1 == u && edge.Item2 == x) || (edge.Item2 == u && edge.Item1 == x)))
                    {
                        neighbours.Add(x);
                    }
                }

                foreach (int v in neighbours)
                {
                    int alt = dist[u] + 1;
                    if (alt < dist[v])
                    {
                        dist[v] = alt;
                        prev[v] = u;
                    }
                }
            }

            // Format path
            List<int> s = new List<int>();
            int u2 = to;
            if (prev[u2] != -1 || u2 == from)
            {
                while (u2 != from)
                {
                    s.Insert(0, u2);
                    u2= prev[u2];
                }
            }

            return s;
        }

        /// <summary>
        /// Set the variables ready for the algorithm. 
        /// </summary>
        /// <param name="nbNodes"> The number of nodes in the graph. </param>
        /// <remarks>
        /// The <see cref="PathLength"/> variable is set to a square matrix of size
        /// <paramref name="nbNodes"/>. The <see cref="Source"/> variable is set to 
        /// <paramref name="nbNodes"/>.
        /// </remarks>
        private static void SetUp(int nbNodes)
        {
            // Initialise all path lengths to infinite
            Paths = new List<int>[nbNodes, nbNodes];
            PathLength = new int[nbNodes, nbNodes];
            for (int row = 0; row < nbNodes; row++)
            {
                for (int Column = 0; Column < nbNodes; Column++)
                {
                    PathLength[row, Column] = int.MaxValue;
                    Paths[row, Column] = new List<int>();
                } 
            }            
            // Set the source node to the highest node ID
            Source = nbNodes;
        }

        /// <summary>
        /// Set <see cref="Source"/> and it's related parameters. 
        /// </summary>
        /// <remarks>
        /// All the nodes with an ID less than or equal to the source are not expanded 
        /// and has no path been found for. The path from the source to the source equals
        /// 0 and thus is this path the first path found.  
        /// </remarks>
        private static void SetSource()
        {
            // Decrement the source
            Source--;
            // Initialse the Expanded and PathFound variables
            Expanded = new bool[Source + 1];
            PathFound = new bool[Source + 1];
            // The path from the source to the source is trivially 0
            PathLength[Source, Source] = 0;
            PathFound[Source] = true;
        }

        /// <summary>
        /// Set <see cref="CurrentNode"/> to the node with the shortest path and 
        /// which has not been expanded yet. 
        /// </summary>
        private static void SetCurrentNode()
        {
            // Set the current node to the first not expanded node
            CurrentNode = Expanded.ToList().IndexOf(false);
            // Find a node with a shorter path that has not been expanded
            for (int node = CurrentNode + 1; node <= Source; node++)
                if (!Expanded[node] && PathLength[Source, node] < PathLength[Source, CurrentNode])
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
        private static void SetValidNeighbours(List<Tuple<int, int>> edges)
        {
            ValidNeighboursOfCurrentNode = new List<int>();
            foreach (Tuple<int, int> edge in edges)
            {
                // If the ID's are greater than the source, the solution has already been found
                if (edge.Item1 <= Source && edge.Item2 <= Source)
                {
                    // Add the node if it connects to the current node and has no path yet
                    if (edge.Item1 == CurrentNode && !PathFound[edge.Item2])
                        ValidNeighboursOfCurrentNode.Add(edge.Item2);
                    else if (edge.Item2 == CurrentNode && !PathFound[edge.Item1])
                        ValidNeighboursOfCurrentNode.Add(edge.Item1);
                }
            }
        }
    }
}
