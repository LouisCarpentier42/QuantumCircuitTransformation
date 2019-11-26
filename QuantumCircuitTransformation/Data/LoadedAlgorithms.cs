using QuantumCircuitTransformation.InitalMappingAlgorithm;
using QuantumCircuitTransformation.TransformationAlgorithm;

namespace QuantumCircuitTransformation.Data
{
    /// <summary>
    /// 
    /// LoadedAlgorithms:
    ///    A static class to keep track of the loaded algorithms. These 
    ///    are the ones preferred by the user. By default are no algorithms
    ///    set to use. 
    ///    
    /// @author:   Louis Carpentier
    /// @version:  1.1
    /// 
    /// </summary>
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
