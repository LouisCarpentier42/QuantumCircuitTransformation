using QuantumCircuitTransformation.QuantumCircuitComponents.Gates;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuantumCircuitTransformation.Exceptions
{
    /// <summary>
    ///     QubitIsNotPartOfGateException:
    ///         An exception for when an attempt is made to access
    ///         a qubit in a gate, which is no part of the gate. 
    /// </summary>
    /// <remarks>
    ///     @author:   Louis Carpentier
    ///     @version:  1.0
    /// </remarks>
    class QubitIsNotPartOfGateException : Exception
    {
        public QubitIsNotPartOfGateException() { }

        public QubitIsNotPartOfGateException(int Qubit, PhysicalGate gate)
            : base(Qubit + " is no part of gate '" + gate.ToString() + "'") { }

        public QubitIsNotPartOfGateException(string message) : base(message) { }
    }
}
