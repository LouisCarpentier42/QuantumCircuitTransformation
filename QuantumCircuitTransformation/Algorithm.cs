using System;
using System.Collections.Generic;
using System.Text;

namespace QuantumCircuitTransformation
{
    /// <summary>
    /// 
    /// Algorithm:
    ///    An interface for all the algorithms which will be implemented. 
    /// 
    /// @author:   Louis Carpentier
    /// @version:  1.1
    /// 
    /// </summary>
    public interface Algorithm
    {
        /// <summary>
        /// Checks if other object equals to this object. 
        /// </summary>
        /// <param name="obj"> The object to compare. </param>
        /// <returns>
        /// True if and only if this and the given object is equal to this object. 
        /// </returns>
        bool Equals(object obj);

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