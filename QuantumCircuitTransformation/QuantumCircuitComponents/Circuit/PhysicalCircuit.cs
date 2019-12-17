using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuantumCircuitTransformation.QuantumCircuitComponents.Architecture;
using QuantumCircuitTransformation.QuantumCircuitComponents.Gates;

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
        public readonly ArchitectureGraph Architecture;


        public PhysicalCircuit(ArchitectureGraph architecture) 
            : this(architecture, new List<PhysicalGate>()) { }



        public PhysicalCircuit(ArchitectureGraph architecture, List<PhysicalGate> gates) 
            : base(gates)
        {
            Architecture = architecture;
        }




        /// <summary>
        /// Adds the given CNOT gate only if it can be exucuted on the architecture of 
        /// the physical device. 
        /// </summary>
        /// <param name="cnot"> The CNOT gate to add to this circuit. </param>
        public override void AddGate(CNOT cnot)
        {
            if (Architecture.CanExecuteCNOT(cnot))
                base.AddGate(cnot);
        }
    }
}