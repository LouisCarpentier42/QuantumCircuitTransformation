using System;
using System.Collections.Generic;
using System.Text;

namespace QuantumCircuitTransformation.QuantumCircuitComponents.Gates
{
    /// <summary>
    ///     GateParts:
    ///         An enum for listing all the possible parts of a gate. 
    ///         Each part works on a different qubit.
    /// </summary>
    /// <example>
    ///     A cnot gate has two parts: the control part and the target
    ///     part. All single qubit gates only have one part. 
    /// </example>
    /// <remarks>
    ///     @author:   Louis Carpentier
    ///     @version:  1.0
    /// </remarks>
    public enum GatePart
    {
        Target, Control, // CNOT gate
        Rz, Rx, Ry, // Rotational gates
        H // Hadamard gate
    }
}