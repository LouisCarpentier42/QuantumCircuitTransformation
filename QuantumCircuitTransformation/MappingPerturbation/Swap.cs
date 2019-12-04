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
    ///     @version:  1.1
    /// </remarks>
    public class Swap : Perturbation, IEquatable<Swap>
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
        /// <param name="mapping"> The mapping to apply this move to. </param>
        /// <param name="qubit1"> The first qubit of this swap move. </param>
        /// <param name="qubit2"> The second qubit of this swap move. </param>
        public Swap(Mapping mapping, int qubit1, int qubit2) : base(mapping)
        {
            Qubit1 = qubit1;
            Qubit2 = qubit2;
        }

        /// <summary>
        /// Apply this swap perturbation. 
        /// </summary>
        public override void Apply()
        {
            Mapping.Swap(Qubit1, Qubit2);
        }

        /// <summary>
        /// Checks if this swap is equal to the given swap.
        /// </summary>
        /// <param name="other"> The swap to compare. </param>
        /// <returns>
        /// True if and only if the given swap has the same mapping as the
        /// mapping of this swap and the qubits in the given swap are equal 
        /// to the qubits in this swap.
        /// </returns>
        /// <remarks>
        /// When a is swapped with b, than a swap which swap b with a is equal
        /// to this.
        /// </remarks>
        public bool Equals(Swap other)
        {
            return Mapping.Equals(other.Mapping) &&
                   ((Qubit1 == other.Qubit1 && Qubit2 == other.Qubit2) ||
                   (Qubit1 == other.Qubit2 && Qubit2 == other.Qubit1));
        }
    }
}
