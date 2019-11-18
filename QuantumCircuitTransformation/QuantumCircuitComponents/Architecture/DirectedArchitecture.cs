using System;
using System.Collections.Generic;
using System.Text;

namespace QuantumCircuitTransformation.QuantumCircuitComponents
{
    public class DirectedArchitecture : ArchitectureGraph
    {

        public DirectedArchitecture(int nbQubits, List<Tuple<int, int>> connections) : base(nbQubits, connections) { }


        /// <summary>
        /// See <see cref="ArchitectureGraph.CanExecuteCNOT(int, int)"/>.
        /// </summary>
        /// <returns>
        /// True if and only if there exists an edge which connectes the given 
        /// control and target qubit from the control qubit to the target qubit.
        /// False is returned in all other cases. 
        /// </returns>
        public override bool CanExecuteCNOT(int control, int target)
        {
            return Edges.Contains(new Tuple<int, int>(control, target));
        }

        /// <summary>
        /// See <see cref="ArchitectureGraph.ComputeCNOTDistance(int)"/>.
        /// </summary>
        /// <returns>
        /// For every SWAP operations are seven extra gates needed (cause the architecture is
        /// directed). The number of SWAP gates to add equals to the length of the shortest path
        /// minus one. 
        /// </returns>
        protected override int ComputeCNOTDistance(int shortestPathLength)
        {
            // TODO path toevoegen zodat je eventuele +4 kan toevoegen (CNOT omdraaien)
            return 7 * (shortestPathLength - 1);
        }
    }
}
