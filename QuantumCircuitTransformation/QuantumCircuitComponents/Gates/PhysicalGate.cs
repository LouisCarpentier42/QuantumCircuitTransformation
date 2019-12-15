using System;
using System.Collections.Generic;
using QuantumCircuitTransformation.Exceptions;

namespace QuantumCircuitTransformation.QuantumCircuitComponents.Gates
{
    /// <summary>
    ///     PhysicalGate:
    ///         An interface for physical gates. These are gates which
    ///         can be executed on an physical quantum device such as
    ///         those of IBM. In this project, there is no need to be
    ///         able to represent other gates cause these ar not used
    ///         in circuit transformation. 
    ///         This is a simplified representation of gates, since 
    ///         these represent quantum gates on a classical computer. 
    /// </summary>
    /// <remarks>
    ///     @author:   Louis Carpentier
    ///     @version:  1.4
    /// </remarks>
    public interface PhysicalGate
    {
        /// <summary>
        /// Return a clone of this physical gate.
        /// </summary>
        PhysicalGate Clone();

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
    }
}
