using System;
using System.Collections.Generic;
using System.Text;

namespace QuantumCircuitTransformation.QuantumCircuitComponents.Gates
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    ///     @author:   Louis Carpentier
    ///     @version:  1.0
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
    }
}