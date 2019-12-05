using System;
using System.Collections.Generic;
using System.Text;
using QuantumCircuitTransformation.Data;

namespace QuantumCircuitTransformation.MappingPerturbation 
{
    /// <summary>
    ///     Swap:
    ///         A class for swap moves on a mapping. This move will take 
    ///         two qubit and change/swap their mapping. 
    /// </summary>
    /// <remarks>
    ///     @author:   Louis Carpentier
    ///     @version:  1.3
    /// </remarks>
    public class Swap : Perturbation
    {
        /// <summary>
        /// Variable referring to the first qubit of this move. 
        /// </summary>
        public readonly int Qubit1;
        /// <summary>
        /// Variable referring to the second qubit of this move. 
        /// </summary>
        public readonly int Qubit2;


        /// <summary>
        /// Initialise a new swap perturbation with given mapping and qubits. 
        /// </summary>
        /// <param name="qubit1"> The first qubit of this swap move. </param>
        /// <param name="qubit2"> The second qubit of this swap move. </param>
        public Swap(int qubit1, int qubit2)
        {
            Qubit1 = qubit1;
            Qubit2 = qubit2;
        }

        /// <summary>
        /// Apply this swap perturbation. 
        /// </summary>
        /// <param name="mapping"> The mapping to apply this move too. </param>
        public void Apply(Mapping mapping)
        {
            mapping.Swap(Qubit1, Qubit2);
        }

        /// <summary>
        /// See <see cref="Perturbation.Equals(object)"/>.
        /// </summary>
        /// <returns>
        /// True if and only if the given swap has the same qubits to swap. 
        /// </returns>
        /// <remarks>
        /// When a is swapped with b, than a swap which swap b with a is equal
        /// to this.
        /// </remarks>
        public override bool Equals(object other)
        {
            if (other == null) return false;
            try
            {
                Swap o = (Swap)other;
                return (Qubit1 == o.Qubit1 && Qubit2 == o.Qubit2) ||
                       (Qubit1 == o.Qubit2 && Qubit2 == o.Qubit1);
            } catch (InvalidCastException)
            {
                return false;
            }
        }

        /// <summary>
        /// See <see cref="Perturbation.GetHashCode"/>.
        /// </summary>
        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
