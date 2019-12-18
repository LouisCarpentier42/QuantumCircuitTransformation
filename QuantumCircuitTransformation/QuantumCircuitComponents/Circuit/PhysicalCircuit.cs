using System;
using System.Collections.Generic;
using System.Linq;
using QuantumCircuitTransformation.QuantumCircuitComponents.ArchitectureGraph;
using QuantumCircuitTransformation.QuantumCircuitComponents.Gates;
using QuantumCircuitTransformation.Exceptions;

namespace QuantumCircuitTransformation.QuantumCircuitComponents.Circuit
{
    /// <summary>
    ///     PhysicalCircuit
    ///         A class to represent a physical quantum circuit. This is a
    ///         quantum circuit which can be executed on a physical device,
    ///         given by an architecture graph. 
    /// </summary>
    /// <remarks>
    ///     @author:   Louis Carpentier
    ///     @version:  2.0
    /// </remarks>
    public sealed class PhysicalCircuit : QuantumCircuit
    {
        /// <summary>
        /// A variable to keep track of the architecture graph of the 
        /// physical circuit. 
        /// </summary>
        public readonly Architecture Architecture;


        /// <summary>
        /// Initialise a new physical circuit with given architecture. 
        /// </summary>
        /// <param name="architecture"> The architecture for this physical circuit. </param>
        public PhysicalCircuit(Architecture architecture) 
            : this(architecture, new List<PhysicalGate>()) { }

        /// <summary>
        /// Initialise a new physical circuit with given architecture and a 
        /// list of gates. 
        /// </summary>
        /// <param name="architecture"> The architecture for this physical circuit. </param>
        /// <param name="gates"> The gates which should already be in this physical circuit. </param>
        /// <exception cref="InvalidGateForArchitectureException"> If any gate can't be executed on the given architecture. </exception>
        public PhysicalCircuit(Architecture architecture, List<PhysicalGate> gates) : base(gates)
        {
            if (gates.Any(gate => !gate.CanBeExecutedOn(architecture)))
                throw new InvalidGateForArchitectureException();
            Architecture = architecture;
        }

        /// <summary>
        /// Adds the given gate to this physical circuit. 
        /// </summary>
        /// <param name="gate"> The gate to add to this circuit. </param>
        /// <exception cref="InvalidGateForArchitectureException"> If the given gate can' be executed on <see cref="Architecture"/>. </exception>
        public void AddGate(PhysicalGate gate)
        {
            if (!gate.CanBeExecutedOn(Architecture))
                throw new InvalidGateForArchitectureException();
            Gates.Add(gate);
            NbGates++;
            NbQubits = Math.Max(gate.GetQubits().Max(), NbQubits);   
        }
    }
}