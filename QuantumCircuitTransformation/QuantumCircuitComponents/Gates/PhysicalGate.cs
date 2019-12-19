using System;
using System.Collections.Generic;
using QuantumCircuitTransformation.Exceptions;
using QuantumCircuitTransformation.QuantumCircuitComponents.ArchitectureGraph;

namespace QuantumCircuitTransformation.QuantumCircuitComponents.Gates
{
    /// <summary>
    ///     PhysicalGate
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
    ///     @version:  1.6
    /// </remarks>
    public interface PhysicalGate : Gate
    { 
        /// <summary>
        /// Checks whether or not this physical gate can be executed
        /// on the given architecture. 
        /// </summary>
        /// <param name="architecture"> The architecture to check if this gate can be executed on it. </param>
        /// <returns>
        /// True if and only if this gate can be executed on the given architecture graph. 
        /// </returns>
        bool CanBeExecutedOn(ArchitectureGraph.Architecture architecture);
    }
}