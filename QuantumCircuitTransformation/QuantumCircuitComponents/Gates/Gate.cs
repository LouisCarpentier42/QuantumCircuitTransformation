using QuantumCircuitTransformation.MappingPerturbation;
using QuantumCircuitTransformation.QuantumCircuitComponents.ArchitectureGraph;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Architecture = QuantumCircuitTransformation.QuantumCircuitComponents.ArchitectureGraph.Architecture;

namespace QuantumCircuitTransformation.QuantumCircuitComponents.Gates
{
    /// <summary>
    ///     Gate
    ///         An interface for quantum gates. 
    /// </summary>
    /// <remarks>
    ///     @author:   Louis Carpentier
    ///     @version:  1.1
    /// </remarks> 
    public interface Gate
    {
        /// <summary>
        /// Return a string representation of this executable gate. 
        /// </summary>
        string ToString();

        /// <summary>
        /// Return a list of all the qubits on which this physical 
        /// gate operates. 
        /// </summary>
        List<int> GetQubits();

        /// <summary>
        /// Gets the gate part of the given qubit. 
        /// </summary>
        /// <param name="qubit"> The qubit to get the gate part from. </param>
        /// <exception cref="QubitIsNotPartOfGateException"> If the given qubit is no part of the the gate. </exception>
        GatePart GetGatePart(int qubit);

        /// <summary>
        /// Compile the gate to a physical equivalent. 
        /// </summary>
        /// <returns>
        /// The ohysical gates which do an equivalent operation as this gate.
        /// </returns>
        List<PhysicalGate> CompileToPhysical();

        /// <summary>
        /// Checks whether or not this physical gate can be executed
        /// on the given architecture. 
        /// </summary>
        /// <param name="architecture"> The architecture to check if this gate can be executed on it. </param>
        /// <returns>
        /// True if and only if this gate can be executed on the given architecture graph. 
        /// </returns>
        bool CanBeExecutedOn(Architecture  architecture, Mapping map);
    }
}