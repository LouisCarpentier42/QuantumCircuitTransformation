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
    /// @version:  1.0
    /// 
    /// </summary>
    public interface Algorithm
    {
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