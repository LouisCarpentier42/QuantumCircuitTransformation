using QuantumCircuitTransformation.QuantumCircuitComponents.Gates;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuantumCircuitTransformation.Exceptions
{
    /// <summary>
    ///     GateIsNotBlockingException:
    ///         An exception which can be used a try is made to add 
    ///         an gate to the physical circuit, even though it is 
    ///         blocked by some other gate. 
    /// </summary>
    /// <remarks>
    ///     @author:   Louis Carpentier
    ///     @version:  1.0
    /// </remarks>
    class GateIsNotBlockingException : Exception
    {
        public GateIsNotBlockingException() { }

        public GateIsNotBlockingException(Gate gate)
            : base("Gate '" + gate.ToString() + "' is blocked") { }

        public GateIsNotBlockingException(string message) : base(message) { }
    }
}
