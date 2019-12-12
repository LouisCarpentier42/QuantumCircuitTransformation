using System;
using System.Collections.Generic;
using System.Text;

namespace QuantumCircuitTransformation.QuantumCircuitComponents.Gates
{
    /// <summary>
    ///     PhysicalGate:
    ///         An interface for physical gates. These are gates which
    ///         can be executed on an physical quantum device such as
    ///         those of IBM. In this project, there is no need to be
    ///         able to represent other gates cause these ar not used
    ///         in circuit transformation. 
    ///         This is a simplified representation of gates, since 
    ///         these represent quantum gates on a classical computer. 
    /// </summary>
    /// <remarks>
    ///     @author:   Louis Carpentier
    ///     @version:  1.1
    /// </remarks>
    public interface PhysicalGate
    {
        /// <summary>
        /// Return a clone of this physical gate.
        /// </summary>
        PhysicalGate Clone();

        /// <summary>
        /// Checks if the given gate depends on the this physical gate. 
        /// </summary>
        /// <param name="gate"> The gate to check if it depends on this gate. </param>
        /// <returns> 
        /// True if and only if the given gate depends on this gate.  
        /// </returns>
        bool DependsOn(PhysicalGate gate);

        /// <summary>
        /// Return a string representation of this executable gate. 
        /// </summary>
        string ToString();
    }
}
