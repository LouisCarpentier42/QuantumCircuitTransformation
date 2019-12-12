using QuantumCircuitTransformation.Algorithms.InitialMappingAlgorithm;
using QuantumCircuitTransformation.Algorithms.TransformationAlgorithm;

namespace QuantumCircuitTransformation.Data
{
    /// <summary>
    ///     LoadedAlgorithms:
    ///         A static class to keep track of the loaded algorithms. These 
    ///         are the ones preferred by the user. By default are no algorithms
    ///         set to use. 
    /// </summary>
    /// <remarks>
    ///     @author:   Louis Carpentier
    ///     @version:  1.1
    /// </remarks>
    public static class LoadedAlgorithms
    {
        /// <summary>
        /// Variable to keep track of the loaded initial mapping algorithm. 
        /// </summary>
        public static InitialMapping InitialMapping;

        /// <summary>
        /// Variable to keep track of the loaded transformation algorithm. 
        /// </summary>
        public static Transformation Transformation;
    }
}
