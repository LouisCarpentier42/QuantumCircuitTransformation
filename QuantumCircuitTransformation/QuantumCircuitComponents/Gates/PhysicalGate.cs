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
    ///         those of IBM. 
    ///         This is an empty interface and only used to keep hold of 
    ///         the different physical gates. 
    /// </summary>
    /// <remarks>
    ///     @author:   Louis Carpentier
    ///     @version:  1.7
    /// </remarks>
    public interface PhysicalGate : Gate
    { 
        
    }
}