using System;
using System.Collections.Generic;
using System.Text;
using QuantumCircuitTransformation.MappingPerturbation;
using QuantumCircuitTransformation.QuantumCircuitComponents.ArchitectureGraph;
using QuantumCircuitTransformation.QuantumCircuitComponents.Circuit;

namespace QuantumCircuitTransformation.Algorithms.InitialMappingAlgorithm
{
    /// <summary>
    ///     RandomMapping
    ///         A class for a generating a random mapping. 
    /// </summary>
    /// <remarks>
    ///     @author:   Louis Carpentier
    ///     @version:  1.0
    /// </remarks>
    public class RandomMapping : InitialMapping
    {
        /// <summary>
        /// Generate a random mapping for the given architecture. 
        /// (See <see cref="InitialMapping.Execute(Architecture, LogicalCircuit)"/>)
        /// </summary>
        public override (Mapping, double) Execute(Architecture architecture, LogicalCircuit circuit)
        {
            Mapping mapping = GetRandomMapping(architecture.NbNodes);
            double cost = GetMappingCost(mapping, architecture, circuit);
            return (mapping, cost);
        }

        /// <summary>
        /// See <see cref="InitialMapping.GetFullShort"/>. 
        /// </summary>
        public override string GetFullShort()
        {
            return "Random";
        }

        /// <summary>
        /// See <see cref="Algorithm.Name"/>.
        /// </summary>
        public override string Name()
        {
            return "Random mapping";
        }

        /// <summary>
        /// See <see cref="Algorithm.Parameters"/>.
        /// </summary>
        public override string Parameters()
        {
            return "";
        }

        /// <summary>
        /// See <see cref="Algorithm.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object other)
        {
            return other is RandomMapping;
        }

        /// <summary>
        /// See <see cref="Algorithm.GetHashCode"/>.
        /// </summary>
        public override int GetHashCode()
        {
            return 1;
        }
    }
}
