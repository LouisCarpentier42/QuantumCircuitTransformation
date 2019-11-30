using System;
using System.Collections.Generic;
using System.Text;

namespace QuantumCircuitTransformation.Exceptions
{
    /// <summary>
    ///     InvalidParameterException:
    ///         An exception which can be used if an invalid parameter
    ///         has been assigned.  
    /// </summary>
    /// <remarks>
    ///     @author:   Louis Carpentier
    ///     @version:  1.0
    /// </remarks>
    public class InvalidParameterException : Exception
    {
        public InvalidParameterException() { }

        public InvalidParameterException(string message) : base(message) { }
    }
}
