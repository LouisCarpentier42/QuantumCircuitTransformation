using System;
using System.Collections.Generic;
using System.Text;
using QuantumCircuitTransformation.QuantumCircuitComponents.Architecture;

namespace QuantumCircuitTransformation.QuantumCircuitComponents
{
    /// <summary>
    ///     PhysicalCircuit
    ///         A class to represent a physical quantum circuit. This is a
    ///         quantum circuit which can be executed on a physical device,
    ///         given by an architecture graph. 
    /// </summary>
    /// <remarks>
    ///     @author:   Louis Carpentier
    ///  @version:  1.0
    /// </remarks>
    public class PhysicalCircuit : QuantumCircuit
    {
        /// <summary>
        /// A variable to keep track of the architecture graph of the 
        /// physical device. 
        /// </summary>
        public readonly ArchitectureGraph ArchitectureGraph;


        /// <summary>
        /// Initialise a new physical circuit with given architecture graph.
        /// </summary>
        /// <param name="architectureGraph"> The architecture graph of the physical device. </param>
        public PhysicalCircuit(ArchitectureGraph architectureGraph)
        {
            ArchitectureGraph = architectureGraph;
        }

        /// <summary>
        /// Adds the given CNOT gate only if it can be exucuted on the architecture of 
        /// the physical device. 
        /// </summary>
        /// <param name="cnot"> The CNOT gate to add to this circuit. </param>
        public override void AddGate(CNOT cnot)
        {
            if (ArchitectureGraph.CanExecuteCNOT(cnot))
                base.AddGate(cnot);
        }
    }
}