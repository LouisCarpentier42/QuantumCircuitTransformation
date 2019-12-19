namespace QuantumCircuitTransformation.MappingPerturbation
{
    /// <summary>
    ///     Perturbation
    ///         An interface for perturbations to apply on a mapping
    /// </summary>
    /// <remarks>
    ///     @author:   Louis Carpentier
    ///     @version:  1.4 
    /// </remarks>
    public interface Perturbation
    {
        /// <summary>
        /// Apply this perturbation on the given mapping.
        /// </summary>
        /// <param name="mapping"></param>
        void Apply(Mapping mapping);

        /// <summary>
        /// Checks if the given object is equal to this perturbation
        /// </summary>
        /// <param name="other"> The object to compare too. </param>
        /// <returns>
        /// True if and only if the given object is not null, has the 
        /// same type and equal properties.
        /// </returns>
        bool Equals(object other);
        
        /// <summary>
        /// Gives the hashcode of this perturbation. 
        /// </summary>
        /// <returns>
        /// A prime factorisation based on the properties of this
        /// perturbation. 
        /// </returns>
        int GetHashCode();
    }
}