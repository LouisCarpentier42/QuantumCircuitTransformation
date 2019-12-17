using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public class PhysicalCircuit : QuantumCircuit
    {
        /// <summary>
        /// A variable to keep track of the architecture graph of the 
        /// physical device. 
        /// </summary>
        public readonly ArchitectureGraph.Architecture Architecture;


        public PhysicalCircuit(ArchitectureGraph.Architecture architecture) 
            : this(architecture, new List<PhysicalGate>()) { }



        public PhysicalCircuit(ArchitectureGraph.Architecture architecture, List<PhysicalGate> gates) 
            : base(gates)
        {
            Architecture = architecture;
        }



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