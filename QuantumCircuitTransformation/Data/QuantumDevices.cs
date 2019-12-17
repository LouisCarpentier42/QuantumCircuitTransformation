using QuantumCircuitTransformation.QuantumCircuitComponents.ArchitectureGraph;
using System;
using System.Collections.Generic;

namespace QuantumCircuitTransformation.Data
{
    /// <summary>
    ///     QuantumDevices:
    ///         A static class to keep track of different quantum devices. These 
    ///         can be real life machines or imaginary ones. 
    /// </summary>
    /// <remarks>
    ///     @author:   Louis Carpentier
    ///     @version:  1.0
    /// </remarks>
    public static class QuantumDevices
    {
        /// <summary>
        /// IBM QX5 is an existing IBM device. It has a directed architecture
        /// graph and exists of 16 qubits. 
        /// </summary>
        private static readonly List<Tuple<int, int>> IBM_QX5e_dges = new List<Tuple<int, int>>
        {
            new Tuple<int, int>(1,2),
            new Tuple<int, int>(2,3),
            new Tuple<int, int>(3,4),
            new Tuple<int, int>(5,4),
            new Tuple<int, int>(6,5),
            new Tuple<int, int>(6,7),
            new Tuple<int, int>(8,7),
            new Tuple<int, int>(15,0),
            new Tuple<int, int>(15,14),
            new Tuple<int, int>(13,14),
            new Tuple<int, int>(12,13),
            new Tuple<int, int>(12,11),
            new Tuple<int, int>(11,10),
            new Tuple<int, int>(9,10),
            new Tuple<int, int>(1,0),
            new Tuple<int, int>(15,2),
            new Tuple<int, int>(3,14),
            new Tuple<int, int>(13,4),
            new Tuple<int, int>(12,5),
            new Tuple<int, int>(6,11),
            new Tuple<int, int>(7,10),
            new Tuple<int, int>(9,8),
        };
        public static readonly Architecture IBM_QX5 = new DirectedArchitecture(IBM_QX5e_dges);

        /// <summary>
        /// IBM Q20 is an existing IBM device. It has an undirected architecture
        /// graph and exists of 20 qubits. 
        /// </summary>
        private static readonly List<Tuple<int, int>> IBMQ20edges = new List<Tuple<int, int>>
        {
            new Tuple<int, int>(0,1),
            new Tuple<int, int>(1,2),
            new Tuple<int, int>(2,3),
            new Tuple<int, int>(3,4),
            new Tuple<int, int>(5,6),
            new Tuple<int, int>(6,7),
            new Tuple<int, int>(7,8),
            new Tuple<int, int>(8,9),
            new Tuple<int, int>(10,11),
            new Tuple<int, int>(11,12),
            new Tuple<int, int>(12,13),
            new Tuple<int, int>(13,14),
            new Tuple<int, int>(15,16),
            new Tuple<int, int>(16,17),
            new Tuple<int, int>(17,18),
            new Tuple<int, int>(18,19),
            new Tuple<int, int>(0,5),
            new Tuple<int, int>(1,6),
            new Tuple<int, int>(2,7),
            new Tuple<int, int>(3,8),
            new Tuple<int, int>(4,9),
            new Tuple<int, int>(5,10),
            new Tuple<int, int>(6,11),
            new Tuple<int, int>(7,12),
            new Tuple<int, int>(8,13),
            new Tuple<int, int>(9,14),
            new Tuple<int, int>(10,15),
            new Tuple<int, int>(11,16),
            new Tuple<int, int>(12,17),
            new Tuple<int, int>(13,18),
            new Tuple<int, int>(14,19),
            new Tuple<int, int>(1,7),
            new Tuple<int, int>(2,6),
            new Tuple<int, int>(3,9),
            new Tuple<int, int>(4,8),
            new Tuple<int, int>(5,11),
            new Tuple<int, int>(6,10),
            new Tuple<int, int>(7,13),
            new Tuple<int, int>(8,12),
            new Tuple<int, int>(11,17),
            new Tuple<int, int>(12,16),
            new Tuple<int, int>(13,19),
            new Tuple<int, int>(14,18),
        };
        public static readonly Architecture IBM_Q20 = new UndirectedArchitecture(IBMQ20edges);
    }
}
