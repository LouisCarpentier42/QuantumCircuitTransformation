using QuantumCircuitTransformation.Algorithms.InitialMappingAlgorithm;
using QuantumCircuitTransformation.Algorithms.TransformationAlgorithm;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuantumCircuitTransformation.Data
{
    /// <summary>
    ///     LoadedAlgorithms:
    ///         A static class to keep track of the different algorithms 
    ///         which can be used. 
    /// </summary>
    /// <remarks>
    ///     @author:   Louis Carpentier
    ///     @version:  1.2
    /// </remarks>
    public static class AllAlgorithms
    {
        /// <summary>
        /// A variable referring to all the initial mapping algorithms. 
        /// </summary>
        public static List<InitialMapping> InitialMappings = new List<InitialMapping>
        {
            new OwnAlgorithm(100, 10, 8500, 500),
            new SimulatedAnnealing(100, 1, 0.95, 100),
        };

        /// <summary>
        /// A variable referring to all the transformation algorithms. 
        /// </summary>
        public static List<Transformation> Transformations = new List<Transformation>();
    }
}