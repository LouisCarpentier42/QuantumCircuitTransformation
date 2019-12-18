using QuantumCircuitTransformation.QuantumCircuitComponents.Gates;
using System;
using System.Collections.Generic;
using System.Linq;
using QuantumCircuitTransformation.Exceptions;

namespace QuantumCircuitTransformation.DependencyGraphs
{
    /// <summary>
    ///     DependencyRule:
    ///         A struct to keep track of a dependency rule. If two gates
    ///         have an overlapping qubit, then a rule can say if it is 
    ///         no problem that the qubits may be switched.
    /// </summary>
    /// <remarks>
    ///     @author:   Louis Carpentier
    ///     @version:  1.3
    /// </remarks>
    public class DependencyRule
    {
        /// <summary>
        /// The gate parts which may overlap. These are sorted. 
        /// </summary>
        public readonly List<GatePart> GateParts;


        /// <summary>
        /// Initialise a new dependency rule with given gateparts.
        /// </summary>
        /// <param name="gateParts"> The gate parts for this dependency rule. </param>
        /// <exception cref="InvalidGatePartForRuleException"> If the given gateparts contain an invalid gatepart. </exception>
        public DependencyRule(List<GatePart> gateParts)
        {
            if (gateParts.Contains(GatePart.None))
                throw new InvalidGatePartForRuleException();
            GateParts = gateParts;
            GateParts.Sort();
        }

        /// <summary>
        /// Initialise a new dependency rule with two gate parts. 
        /// </summary>
        /// <param name="gatePart1"> The first gate part for this dependency rule. </param>
        /// <param name="gatePart2"> The second gate part for this dependency rule. </param>
        public DependencyRule(GatePart gatePart1, GatePart gatePart2)
            : this(new List<GatePart> { gatePart1, gatePart2 }) { }

        /// <summary>
        /// Checks if two gates with given overlapping gate parts may be
        /// switched by this rule. 
        /// </summary>
        /// <param name="overlappingGateParts"> The gate parts that overlap. </param>
        /// <returns>
        /// True if and only if the given overlapping gate parts equal 
        /// to the gate parts of this rule.
        /// </returns>
        /// <remarks>
        /// The given gate parts must be sorted. 
        /// </remarks>
        public bool CanBeSwitched(List<GatePart> overlappingGateParts)
        {
            return GateParts.SequenceEqual(overlappingGateParts);
        }
    }
}