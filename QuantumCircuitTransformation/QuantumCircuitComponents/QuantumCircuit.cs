using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuantumCircuitTransformation.QuantumCircuitComponents
{
    /// <summary>
    /// 
    /// QuantumCircuit
    ///    A class to represent any quantum circuit. In general this class 
    ///    represents logical circuits. See <see cref="PhysicalCircuit"/> 
    ///    for a class which can represent a physical circuit.
    ///    
    /// @author:   Louis Carpentier
    /// @version:  1.3
    /// 
    /// </summary>
    public class QuantumCircuit
    {
        /// <summary>
        /// Variable referring to all the gates in this quantum circuit. The
        /// gates are sorted in layers, such that the execution of a gate in
        /// some layer doesn't affect the outcome of any other gate in the 
        /// same layer. 
        /// </summary>
        public List<List<CNOT>> Layers { get; private set; }
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
        /// Variable referring to the number of gates in this quantum circuit. 
        /// </summary>
        public int NbGates { get; private set; }
        /// <summary>
        /// A variable to keep track of the number of qubits used in this circuit.
        /// </summary>
        private int NbQubits;

        /// <summary>
        /// Initialise a new Quantum circuit without any gates. 
        /// </summary>
        public QuantumCircuit()
        {
            Layers = new List<List<CNOT>> { new List<CNOT>() };
            LayerSize = new List<int> { 0 };
            NbLayers = 1;
            NbGates = 0;
            NbQubits = 0;
        }

        /// <summary>
        /// Adds a CNOT gate at the end of this quantum circuit. 
        /// </summary>
        /// <param name="newGate"> The gate to add to this circuit. </param>
        public virtual void AddGate(CNOT newGate)
        {
            if (Layers[0].Any(cnot => cnot.TargetQubit == newGate.ControlQubit))
            {
                Layers.Add(new List<CNOT> { newGate });
                LayerSize.Add(1);
                NbLayers++;
            }
            else
            {
                Layers[NbLayers - 1].Add(newGate);
                LayerSize[NbLayers - 1]++;
            }
            NbGates++;
            NbQubits = Math.Max(Math.Max(newGate.ControlQubit, newGate.TargetQubit), NbQubits);
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
                "qreg q[" + NbQubits + "];\n" +
                "creg q[" + NbQubits + "];\n"; ;
            for (int i = 0; i < NbLayers; i++)
                for (int j = 0; j < Layers[i].Count; j++)
                    codeRepresenation += "\n" + Layers[i][j];
            return codeRepresenation;
        }
    }
}