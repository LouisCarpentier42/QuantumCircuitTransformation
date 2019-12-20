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
    public sealed class PhysicalCircuit : QuantumCircuit<PhysicalGate>
    {
        /// <summary>
        /// A variable to keep track of the architecture graph of the 
        /// physical circuit. 
        /// </summary>
        public readonly Architecture Architecture;


        /// <summary>
        /// Initialise a new, empty physical circuit with given architecture. 
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
        /// <remarks>
        /// All the given gates should be executable on the given architecture. 
        /// </remarks>
        public PhysicalCircuit(Architecture architecture, List<PhysicalGate> gates) : base(gates)
        {
            Architecture = architecture;
        }


        /// <summary>
        /// Adds the given gate to this physical circuit. 
        /// </summary>
        /// <param name="gate"> The gate to add to this circuit. </param>
        /// <remarks>
        /// The given gate should be able to be executed on the the architecture
        /// of this physical circuit. 
        /// </remarks>
        public void AddGate(PhysicalGate gate)
        {
            Gates.Add(gate);
            NbGates++;
            NbQubits = Math.Max(gate.GetQubits().Max(), NbQubits);   
        }

        /// <summary>
        /// Adds a range of gates to this physical circuit. 
        /// </summary>
        /// <param name="gates"> The gates to add to this circuit. </param>
        /// <remarks> 
        /// This method calls <see cref="AddGate(PhysicalGate)"/> for every gate 
        /// in the given list of gates. 
        /// </remarks>
        public void AddGate(List<PhysicalGate> gates)
        {
            for (int i = 0; i < gates.Count; i++)
                AddGate(gates[i]);
        }
    }
}