using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace QuantumCircuitTransformation.MappingPerturbation
{
    /// <summary>
    ///     Cycle
    ///         A class for cycle perturbations. This kind of
    ///         perturbation will change all the elements in 
    ///         the mapping according to a permutation. The 
    ///         element at index i will be swapped with the 
    ///         element at index i + 1. This will create some
    ///         shifting in the mapping.
    /// </summary>
    /// <remarks>
    ///      @author:   Louis Carpentier
    ///      @version:  1.2
    /// </remarks>
    public sealed class Cycle : Perturbation
    {
        /// <summary>
        /// Variable to keep track of the permutation for 
        /// this cycle perturbation. 
        /// </summary>
        public readonly int[] Permutation;


        /// <summary>
        /// Initialise a new cycle perturbation with given permutation. 
        /// </summary>
        /// <param name="permutation"> The permutation for this cycle. </param>
        public Cycle(int[] permutation)
        {
            Permutation = permutation;
        }


        /// <summary>
        /// Apply this cycle perturbation. 
        /// </summary>
        /// <param name="mapping"> The mapping to apply this cycle on. </param>
        public void Apply(Mapping mapping)
        {
            for (int i = 0; i < Permutation.Length - 1; i++)
                mapping.Swap(Permutation[i + 1], Permutation[i]);
            mapping.Swap(Permutation[0], Permutation[Permutation.Length - 1]);
        }

        /// <summary>
        /// See <see cref="Perturbation.Equals(object)"/>.
        /// </summary>
        /// <returns>
        /// True if and only if the permutation for this cycle equals
        /// to the permutation of the given cycle. 
        /// </returns>
        public override bool Equals(object other)
        {
            if (other == null) return false;
            try
            {
                Cycle o = (Cycle)other;
                return Permutation.SequenceEqual(o.Permutation);
            }
            catch (InvalidCastException)
            {
                return false;
            }
        }

        /// <summary>
        /// See <see cref="Perturbation.GetHashCode"/>.
        /// </summary>
        public override int GetHashCode()
        {
            return Permutation.GetHashCode();
        }
    }
}