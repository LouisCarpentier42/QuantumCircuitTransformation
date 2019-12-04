using System;
using System.Collections.Generic;
using System.Text;

namespace QuantumCircuitTransformation.MappingPerturbation
{
    /// <summary>
    ///     Move:
    ///         An abstract class for moves to apply on a mapping
    /// </summary>
    /// <remarks>
    ///     @author:   Louis Carpentier
    ///     @version:  1.0 
    /// </remarks>
    public abstract class Perturbation
    {
        /// <summary>
        /// The mapping to apply the this move to.
        /// </summary>
        public readonly Mapping Mapping;


        /// <summary>
        /// Initialise a new move with given mapping.
        /// </summary>
        /// <param name="mapping"> The map for this move. </param>
        public Perturbation(Mapping mapping)
        {
            Mapping = mapping;
        }

        /// <summary>
        /// Apply this move on the map of this move. 
        /// </summary>
        public abstract void Apply();

        
    }
}
