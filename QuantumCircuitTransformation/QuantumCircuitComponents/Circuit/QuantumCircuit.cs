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
        public readonly List<PhysicalGate> Gates;
        /// <summary>
        /// Variable referring to the number of gates in this quantum circuit. 
        /// </summary>
        public int NbGates { get; protected set; }
        /// <summary>
        /// A variable to keep track of the number of qubits used in this circuit.
        /// </summary>
        public int NbQubits { get; protected set; }


        // To remove
        /// <summary>
        /// Variable referring to all the gates in this quantum circuit. The
        /// gates are sorted in layers, such that the execution of a gate in
        /// some layer doesn't affect the outcome of any other gate in the 
        /// same layer. 
        /// </summary>
        public List<List<PhysicalGate>> Layers { get; private set; }
        /// <summary>
        /// Variable referring to the size of each layer. At index i is the number
        /// of gates in layer i of <see cref="Layers"/>.
        /// </summary>
        public List<int> LayerSize { get; private set; }
        /// <summary>
        /// Variable referring to the number of layers this quantum circuit has. 
        /// </summary>
        public int NbLayers { get; private set; }


        /// <summary>
        /// Initialise a new Quantum circuit without any gates. 
        /// </summary>
        public QuantumCircuit(List<PhysicalGate> gates)
        {
            Gates = gates;
            NbGates = gates.Count;
            NbQubits = gates.Max(gate => gate.GetQubits().Max()) + 1;

            // To remove
            Layers = new List<List<PhysicalGate>> { new List<PhysicalGate>() };
            LayerSize = new List<int> { 0 };
            NbLayers = 1;
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