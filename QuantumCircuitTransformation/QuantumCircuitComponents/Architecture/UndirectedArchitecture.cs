using System;
using System.Collections.Generic;
using System.Text;

namespace QuantumCircuitTransformation.QuantumCircuitComponents
{
    public class UndirectedArchitecture : ArchitectureGraph
    {

        public UndirectedArchitecture(int nbQubits, List<Tuple<int, int>> connections) : base(nbQubits, connections) { }


        /// <summary>
        /// See <see cref="ArchitectureGraph.CanExecuteCNOT(int, int)"/>.
        /// </summary>
        /// <returns>
        /// True if and only if there exists an edge which connectes the given 
        /// control and target qubit in any direction. In all other cases is 
        /// false returned. 
        /// </returns>
        public override bool CanExecuteCNOT(int control, int target)
        {
            return Edges.Contains(new Tuple<int, int>(control, target)) ||
                   Edges.Contains(new Tuple<int, int>(target, control));
        }


        /// <summary>
        /// See <see cref="ArchitectureGraph.ComputeCNOTDistance(int)"/>.
        /// </summary>
        /// <returns>
        /// For every SWAP operation are three extra gates needed cause the graph is undirected. 
        /// The number of SWAP gates to add equals to the length of the shortest path minus one. 
        /// </returns>
        protected override int ComputeCNOTDistance(int shortestPathLength)
        {
            return 3 * (shortestPathLength - 1);
        }
    }
}
