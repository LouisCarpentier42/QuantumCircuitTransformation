using QuantumCircuitTransformation.Data;
using QuantumCircuitTransformation.QuantumCircuitComponents.Circuit;
using QuantumCircuitTransformation.QuantumCircuitComponents.Gates;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace QuantumCircuitTransformation.QuantumCircuitComponents.Circuit
{
    /// <summary>
    ///     CircuitGenerator
    ///         A static class to easily generate logical circuits.
    /// </summary>
    /// <remarks>
    ///     @author:   Louis Carpentier
    ///     @version:  1.3
    /// </remarks>
    public static class CircuitGenerator
    {
        /// <summary>
        /// Generate a random quantum circuit, existing of only CNOT gates. 
        /// </summary>
        /// <param name="nbGates"> The number of gates that should be in the circuit. </param>
        /// <param name="nbQubits"> The number of qubits that should be in the circuit. </param>
        /// <returns>
        /// A quantum circuit which has the given number of CNOT gates and given number of
        /// qubits. The gates are randomly generated such that for every CNOT gate the 
        /// control and target qubit are different. Note that it is possible (cause of the
        /// randomness) that some qubits are never used. 
        /// </returns>
        public static LogicalCircuit RandomCircuit(int nbGates, int nbQubits)
        {
            List<Gate> gates = new List<Gate>();
            int controlQubit, targetQubit;
            for (int gateID = 0; gateID < nbGates; gateID++)
            {
                controlQubit = Globals.Random.Next(nbQubits);
                do targetQubit = Globals.Random.Next(nbQubits);
                while (controlQubit == targetQubit);
                gates.Add(new CNOT(controlQubit, targetQubit));
            }
            return new LogicalCircuit(gates);
        }

        /// <summary>
        /// Generate a quantum circuit from a file. 
        /// </summary>
        /// <param name="fileName"> The filename in which the circuit is. </param>
        /// <returns>
        /// The quantum circuit representation of the circuit in the given file.
        /// </returns>
        /// <remarks>
        /// The file should be in the folder <see cref="Globals.BenchmarkFolder"/>.
        /// </remarks>
        public static LogicalCircuit ReadFromFile(string fileName)
        {
            List<Gate> gates = new List<Gate>();
            string[] file = File.ReadAllLines(Benchmarks.BenchmarkFolder + fileName);
            for (int i = 0; i < file.Length; i++)
            {
                Gate gate = InitialiseGate(file[i]);
                if (gate != null)
                {
                    gates.Add(gate);
                }
                    
            }
            file = null;
            GC.Collect();
            return new LogicalCircuit(gates);
        }


        /// <summary>
        /// Initialise a gate by the given line code. 
        /// </summary>
        /// <param name="code"> The line code which represents a gate. </param>
        /// <returns>
        /// The gate which is represented by the given line. If the given
        /// line doesn't represent a valid gate or a gate which is not 
        /// implemented, then is null returned. 
        /// </returns>
        private static Gate InitialiseGate(string code)
        {
            if (!Regex.Match(code, @"^\S+ (q[\D+],)*(q[\D])").Success || Regex.Match(code, @"^qreg\s").Success)
                return null;
            string prefix = code.Split()[0].ToUpper();
            List<double> numbers = Regex.Split(code, @"[^0-9\.]+").Where(x => !string.IsNullOrEmpty(x)).Select(x => Convert.ToDouble(x)).ToList();
            switch (prefix)
            {
                case "CX": // CNOT gate
                    return new CNOT((int)numbers[0], (int)numbers[1]);
                case "H": // Hadamard gate
                    return U3.GetHadamardGate((int)numbers[0]);
                case "U3": 
                case "U": // U3 rotation gate
                    return new U3((int)numbers[3], numbers[0], numbers[1], numbers[2], GatePart.U3);
                case "RX": // Rotation over x axis
                    return new U3((int)numbers[1], numbers[0], -Math.PI / 2, Math.PI / 2, GatePart.Rx);
                case "RY": // Rotation over y axis
                    return new U3((int)numbers[1], numbers[0], 0, 0, GatePart.Ry);
                case "RZ": // Rotation over z axis
                    return new U3((int)numbers[1], 0, numbers[0], 0, GatePart.Rz);
                case "X": // Pauli X gate
                    return new U3((int)numbers[0], Math.PI, -Math.PI / 2, Math.PI / 2, GatePart.X);
                case "Y": // Pauli Y gate
                    return new U3((int)numbers[0], Math.PI, 0, 0, GatePart.Y);
                case "Z": // Pauli Z gate
                    return new U3((int)numbers[0], 0, Math.PI, 0, GatePart.Z);
                case "T": // T gate
                    return new U3((int)numbers[0], 0, Math.PI / 4, 0, GatePart.T);
                case "TDG": // Invers of t gate
                    return new U3((int)numbers[0], 0, -Math.PI / 4, 0, GatePart.TDG);
            }
            return null;
        }
    }
}