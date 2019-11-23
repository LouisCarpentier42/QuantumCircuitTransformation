﻿using System;
using System.Collections.Generic;
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
    /// @version:  1.2
    /// 
    /// </summary>
    public class QuantumCircuit
    {
        /// <summary>
        /// A variable to keep track of all the gates in this quantum circuit.  
        /// </summary>
        /// <remarks>
        /// There are only CNOT gates (see <see cref="CNOT"/>) used. It can be proven 
        /// that all single qubit gates and combined with CNOT gates are a universal 
        /// set of gates in quantum computing. Since single qubit gates are not of 
        /// interest in quantum circuit transformation are only the CNOT gates needed.
        /// </remarks>
        public List<CNOT> Gates { get; private set; }
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
            Gates = new List<CNOT>();
            NbGates = 0;
            NbQubits = 0;
        }

        /// <summary>
        /// Adds a CNOT gate at the end of this quantum circuit. 
        /// </summary>
        /// <param name="cnot"> The CNOT gate to add to this circuit. </param>
        public virtual void AddGate(CNOT cnot)
        {
            Gates.Add(cnot);
            NbGates++;
            NbQubits = Math.Max(Math.Max(cnot.ControlQubit, cnot.TargetQubit), NbQubits);
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
            for (int i = 0; i < Gates.Count; i++)
                codeRepresenation += "\n" + Gates[i];
            return codeRepresenation;
        }
    }
}