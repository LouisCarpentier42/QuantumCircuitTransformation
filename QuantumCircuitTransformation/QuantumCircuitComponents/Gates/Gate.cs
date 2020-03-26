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
    ///         An abstract class for quantum gates. 
    /// </summary>
    /// <remarks>
    ///     @author:   Louis Carpentier
    ///     @version:  1.1
    /// </remarks> 
    public abstract class Gate
    {
        /// <summary>
        /// Variable referring to the qubits in this quantum gate. 
        /// </summary>
        protected readonly List<int> Qubits;

        /// <summary>
        /// Initialise a new quantum gate with given qubits. 
        /// </summary>
        /// <param name="qubits"> The qubits of this quantum gate. </param>
        protected Gate(List<int> qubits)
        {
            Qubits = qubits;
        }

        /// <summary>
        /// Return the Qubits of this quantum gate. 
        /// </summary>
        /// <returns></returns>
        public List<int> GetQubits()
        {
            return Qubits;
        }


        /// <summary>
        /// Gives a mapping of this gate according to the given mapping.  
        /// </summary>
        /// <param name="mapping"> The mapping according which should be mapped. </param>
        /// <return>
        /// A new gate in which all qubits are mapped according to the given mapping. 
        /// </return>
        public abstract Gate Map(Mapping mapping);

        /// <summary>
        /// Checks whether or not this physical gate can be executed
        /// on the given architecture according to the given mapping. 
        /// </summary>
        /// <param name="architecture"> The architecture to check if this gate can be executed on it. </param>
        /// <param name="map"> The mapping according to which the gate should be executable. </param>
        /// <returns>
        /// True if and only if this gate can be executed on the given architecture graph, 
        /// according to the given mapping. 
        /// </returns>
        public abstract bool CanBeExecutedOn(Architecture architecture, Mapping map);

        /// <summary>
        /// Gets the gate part of the given qubit. 
        /// </summary>
        /// <param name="qubit"> The qubit to get the gate part from. </param>
        /// <exception cref="QubitIsNotPartOfGateException"> If the given qubit is no part of the the gate. </exception>
        public abstract GatePart GetGatePart(int qubit);

        /// <summary>
        /// Returns the maximum highest qubit id this gate operates on. 
        /// </summary>
        public abstract int GetMaxQubit();
    }
}