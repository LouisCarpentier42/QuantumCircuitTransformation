﻿using QuantumCircuitTransformation.Data;
using QuantumCircuitTransformation.QuantumCircuitComponents.Circuit;
using QuantumCircuitTransformation.QuantumCircuitComponents.Gates;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace QuantumCircuitTransformation.QuantumCircuitComponents.Circuit
{
    /// <summary>
    ///     CircuitGenerator:
    ///         A static class to easily generate circuits.
    /// </summary>
    /// <remarks>
    ///     @author:   Louis Carpentier
    ///     @version:  1.2
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
            List<PhysicalGate> gates = new List<PhysicalGate>();
            int controlQubit, targetQubit;
            for (int gateID = 0; gateID < nbGates; gateID++)
            {
                controlQubit = Globals.Random.Next(nbQubits);
                do targetQubit = Globals.Random.Next(nbQubits); while (controlQubit == targetQubit);
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
            List<PhysicalGate> gates = new List<PhysicalGate>();
            string[] file = File.ReadAllLines(Globals.BenchmarkFolder + fileName);
            for (int i = 0; i < file.Length; i++)
            {
                gates.Add(InitialiseGate(file[i]));
            }
            gates.RemoveAll(x => x == null);
            return new LogicalCircuit(gates);
        }


        private static PhysicalGate InitialiseGate(string code)
        {
            string prefix = code.Split()[0];
            int[] numbers = Regex.Split(code, @"\D+").Where(x => !string.IsNullOrEmpty(x)).Select(x => Convert.ToInt32(x)).ToArray();
            switch (prefix.ToLower())
            {
                case "cx":
                    return new CNOT(numbers[0], numbers[1]);
                case "h":
                    return SingleQubitGate.GetHadamardGate(numbers[0]);
                default:
                    return null;
            }
        }

    }
}
