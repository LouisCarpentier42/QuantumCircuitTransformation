using System;
using System.Collections.Generic;

namespace QuantumCircuitTransformation.QuantumCircuitComponents.Architecture
{
    /// <summary>
    /// 
    /// UndirectedArchitecture
    ///    A class for undirected architecture graphs of physical quantum devices. 
    ///    In an undirected architecture can a CNOT gate be executed on any pair
    ///    of qubits that are connected, where both qubits can be the control qubit. 
    ///    
    /// @author:   Louis Carpentier
    /// @version:  1.0
    /// 
    /// </summary>
    public class UndirectedArchitecture : ArchitectureGraph
    {
        /// <summary>
        /// See <see cref="ArchitectureGraph(List{Tuple{int, int}})"/>
        /// </summary>
        public UndirectedArchitecture(List<Tuple<int, int>> connections) 
            : base(connections) { }

        /// <summary>
        /// See <see cref="ArchitectureGraph.CanExecuteCNOT(CNOT)"/>.
        /// </summary>
        /// <returns>
        /// True if and only if there exists an edge which connectes the control  
        /// and target qubit of the given CNOT gate in any direction. In all other 
        /// cases is false returned. 
        /// </returns>
        public override bool CanExecuteCNOT(CNOT cnot)
        {
            return Edges.Contains(new Tuple<int, int>(cnot.ControlQubit, cnot.TargetQubit)) ||
                   Edges.Contains(new Tuple<int, int>(cnot.TargetQubit, cnot.ControlQubit));
        }


        protected override int ComputeCNOTDistance(int pathLength)
        {
            return Math.Max(0, 3 * (pathLength - 1));
        }
    }
}