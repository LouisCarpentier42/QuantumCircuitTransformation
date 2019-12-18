using System;
using System.Collections.Generic;
using System.Text;

namespace QuantumCircuitTransformation.Exceptions
{
    /// <summary>
    ///     InvalidGatePartForRuleException
    ///         An exception for when an invalid gate has been tried
    ///         to input in a rule. 
    /// </summary>
    /// <remarks>
    ///     @author:   Louis Carpentier
    ///     @version:  1.0
    /// </remarks>
    public class InvalidGatePartForRuleException : Exception
    {
        public InvalidGatePartForRuleException() { }

        public InvalidGatePartForRuleException(string message) : base(message) { }
    }
}
