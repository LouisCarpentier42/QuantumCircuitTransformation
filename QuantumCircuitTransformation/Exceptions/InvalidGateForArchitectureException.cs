using System;
using System.Collections.Generic;
using System.Text;

namespace QuantumCircuitTransformation.Exceptions
{
    /// <summary>
    ///     InvalidGateForArchitectureException:
    ///         An exception for when an attempt was made to add a gate
    ///         to an architecture, on which the gate can't be executed.
    /// </summary>
    /// <remarks>
    ///     @author:   Louis Carpentier
    ///     @version:  1.0
    /// </remarks>
    class InvalidGateForArchitectureException : Exception
    {
        public InvalidGateForArchitectureException() { }

        public InvalidGateForArchitectureException(string message) : base(message) { }
    }
}
