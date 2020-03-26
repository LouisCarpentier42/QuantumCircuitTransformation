using QuantumCircuitTransformation.MappingPerturbation;
using QuantumCircuitTransformation.QuantumCircuitComponents.Gates;
using QuantumCircuitTransformation.Algorithms.InitialMappingAlgorithm;
using QuantumCircuitTransformation.QuantumCircuitComponents.ArchitectureGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuantumCircuitTransformation.QuantumCircuitComponents.Circuit
{
    /// <summary>
    ///     QuantumCircuit
    ///         An abstract class to represent any quantum circuit. This
    ///         can be a logical or physical circuit. 
    /// </summary>
    /// <remarks>
    ///     @author:   Louis Carpentier
    ///     @version:  2.0
    /// </remarks>
    public abstract class QuantumCircuit
    {
        /// <summary>
        /// A list of the physical gates in this quantum circuit. 
        /// </summary>
        public readonly List<Gate> Gates;
        /// <summary>
        /// Variable referring to the number of gates in this quantum circuit. 
        /// </summary>
        public int NbGates { get; protected set; }
        /// <summary>
        /// A variable to keep track of the number of qubits used in this circuit.
        /// </summary>
        public int NbQubits { get; protected set; }


        /// <summary>
        /// Initialise a new quantum circuit with the given gates. 
        /// </summary>
        /// <param name="gates"> The gates for this quantum circuit. </param>
        public QuantumCircuit(List<Gate> gates)
        {
            Gates = gates;
            NbGates = gates.Count;
            if (gates.Count == 0)
            {
                NbQubits = 0;
            } else
            {
                NbQubits = gates.Max(gate => gate.GetMaxQubit()) + 1;
            }
        }

        /// <summary>
        /// Gives a string representation of this quantum circuit.  
        /// </summary>
        /// <returns>
        /// Initially is the command qreg given with the number of qubits
        /// used in this quantum circuit. Next are all the gates given in 
        /// this circuit. 
        /// </returns>
        public override string ToString()
        {
            string codeRepresenation =
                "OPENQASM 2.0;\n" +
                "include \"qelib1.inc\";\n\n" +
                "qreg q[" + NbQubits + "];\n";
            for (int i = 0; i < Gates.Count; i++)
                codeRepresenation += "\n" + Gates[i];
            return codeRepresenation;
        }
    }
}