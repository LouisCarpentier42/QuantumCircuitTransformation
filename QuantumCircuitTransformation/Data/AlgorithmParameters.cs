using QuantumCircuitTransformation.Algorithms.InitialMappingAlgorithm;
using QuantumCircuitTransformation.Algorithms.TransformationAlgorithm;
using QuantumCircuitTransformation.DependencyGraphs;
using QuantumCircuitTransformation.QuantumCircuitComponents.Gates;
using System.Collections.Generic;

namespace QuantumCircuitTransformation.Data
{
    /// <summary>
    ///     AlgorithmParameters
    ///         A static class to keep track of all the different parameters
    ///         which can be used during the execution of any algorithm. 
    /// </summary>
    /// <remarks>
    ///     @author:   Louis Carpentier
    ///     @version:  2.0
    /// </remarks>
    public static class AlgorithmParameters
    {
        /// <summary>
        /// Variable to keep track of the loaded initial mapping algorithm. 
        /// </summary>
        public static InitialMapping InitialMapping;

        /// <summary>
        /// Variable to keep track of the loaded transformation algorithm. 
        /// </summary>
        public static Transformation Transformation;

        /// <summary>
        /// Variable to keep track of all the loaded dependency rules. 
        /// </summary>
        public static List<DependencyRule> DependencyRules = new List<DependencyRule>
        {
            new DependencyRule(GatePart.Control, GatePart.Control),
            new DependencyRule(GatePart.Target,  GatePart.Target),
            new DependencyRule(GatePart.Rz,      GatePart.Control),
            new DependencyRule(GatePart.Rx,      GatePart.Target)
        };


        /// <summary>
        /// A list of all the available initial mapping algorithms. 
        /// </summary>
        public static List<InitialMapping> AvailableInitialMappings = new List<InitialMapping>
        {
            new OwnAlgorithm(100, 10, 8500, 500),
            new SimulatedAnnealing(100, 1, 0.95, 100),
        };

        /// <summary>
        /// A list of all the available transformation algorithms. 
        /// </summary>
        public static List<Transformation> AvailableTransformationAlgorithms = new List<Transformation>();

        /// <summary>
        /// A list of all the available dependency rules to generate a dependency graph.
        /// </summary>
        public static List<DependencyRule> AvailableDependencyRules = new List<DependencyRule>()
        { 
            new DependencyRule(GatePart.Control, GatePart.Control),
            new DependencyRule(GatePart.Target,  GatePart.Target),
            new DependencyRule(GatePart.Rz,      GatePart.Control),
            new DependencyRule(GatePart.Rx,      GatePart.Target)
        };
    }
}