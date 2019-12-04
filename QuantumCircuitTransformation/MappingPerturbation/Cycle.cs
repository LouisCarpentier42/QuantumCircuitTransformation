using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace QuantumCircuitTransformation.MappingPerturbation
{
    /// <summary>
    ///     Cycle:
    ///         A class for cycle perturbations. This kind of
    ///         perturbation will change all the elements in 
    ///         the mapping according to a permutation. The 
    ///         element at index i will be swapped with the 
    ///         element at index i + 1. This will create some
    ///         shifting in the mapping.
    /// </summary>
    /// <remarks>
    ///      @author:   Louis Carpentier
    ///      @version:  1.0
    /// </remarks>
    public class Cycle : Perturbation, IEquatable<Cycle>
    {
        /// <summary>
        /// Variable to keep track of the permutation for 
        /// this cycle perturbation. 
        /// </summary>
        public readonly int[] Permutation;


        /// <summary>
        /// Initialise a new cycle perturbation with given mapping
        /// and permutation. 
        /// </summary>
        /// <param name="mapping"> The mapping for this cycle. </param>
        /// <param name="permutation"> The permutation for this cycle. </param>
        public Cycle(Mapping mapping, int[] permutation) : base(mapping)
        {
            Permutation = permutation;
        }

        /// <summary>
        /// Apply this cycle perturbation. 
        /// </summary>
        public override void Apply()
        {
            for (int i = 0; i < Permutation.Length - 1; i++)
                Mapping.Swap(Permutation[i + 1], Permutation[i]);
            Mapping.Swap(Permutation[0], Permutation[Permutation.Length - 1]);
        }

        /// <summary>
        /// Checks if this cycle equals to the given cycle. 
        /// </summary>
        /// <param name="other"> The cycle to compare. </param>
        /// <returns>
        /// True if and only if the given cycle has the same mapping 
        /// as this cycle and the same permutation. 
        /// </returns>
        public bool Equals(Cycle other)
        {
            return Mapping.Equals(other.Mapping) &&
                   Permutation.SequenceEqual(other.Permutation);
        }
    }
}