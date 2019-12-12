using System;
using System.Collections.Generic;
using System.Text;

namespace QuantumCircuitTransformation.Algorithms
{
    /// <summary>
    ///     Algorithm:
    ///         An interface for all the algorithms which will be implemented. 
    /// </summary>
    /// <remarks>
    ///     @author:   Louis Carpentier
    ///     @version:  1.1
    /// </remarks>
    public interface Algorithm
    {
        /// <summary>
        /// Checks if other object equals to this object. 
        /// </summary>
        /// <param name="other"> The object to compare. </param>
        /// <returns>
        /// True if and only if this and the given object is equal to this object. 
        /// </returns>
        bool Equals(object other);

        /// <summary>
        /// Returns a hashcode for this algorithm. 
        /// </summary>
        /// <returns>
        /// A prime factorisation based on the parameters of this algorithm. 
        /// </returns>
        int GetHashCode();

        /// <summary>
        /// Get the name of this algorithm. 
        /// </summary>
        string Name();

        /// <summary>
        /// Get the parameters of this algorithm.
        /// </summary>
        string Parameters();
    }
}