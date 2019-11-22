using QuantumCircuitTransformation.QuantumCircuitComponents;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuantumCircuitTransformation
{
    /// <summary>
    /// 
    /// CircuitGenerator:
    ///    A static class to easily generate circuits.
    ///    
    /// @author:   Louis Carpentier
    /// @version:  1.0
    /// 
    /// </summary>
    public static class CircuitGenerator
    {
        /// <summary>
        /// Generate a random quantum circuit, existing of only CNOT gates. 
        /// </summary>
        /// <param name="NbGates"> The number of gates that should be in the circuit. </param>
        /// <param name="NbQubits"> The number of qubits that should be in the circuit. </param>
        /// <returns>
        /// A quantum circuit which has the given number of CNOT gates and given number of
        /// qubits. The gates are randomly generated such that for every CNOT gate the 
        /// control and target qubit are different. Note that it is possible (cause of the
        /// randomness) that some qubits are never used. 
        /// </returns>
        public static QuantumCircuit RandomCircuit(int NbGates, int NbQubits)
        {
            QuantumCircuit circuit = new QuantumCircuit();
            int controlQubit, targetQubit;
            for (int gateID = 0; gateID < NbGates; gateID++)
            {
                controlQubit = Globals.Random.Next(NbQubits);
                do targetQubit = Globals.Random.Next(NbQubits); while (controlQubit == targetQubit);
                circuit.AddGate(new CNOT(controlQubit, targetQubit));
            }
            return circuit;
        }
    }
}
