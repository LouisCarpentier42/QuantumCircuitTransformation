using QuantumCircuitTransformation.InitalMappingAlgorithm;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuantumCircuitTransformation
{
    /// <summary>
    /// 
    /// LoadedAlgorithms:
    ///    A static class to keep track of the different algorithms 
    ///    which can be used. 
    /// 
    /// @author:   Louis Carpentier
    /// @version:  1.1
    /// 
    /// </summary>
    public static class AllAlgorithms
    {
        /// <summary>
        /// A variable for the loaded initial mapping algorithms. 
        /// </summary>
        public static List<InitialMapping> InitialMappings = new List<InitialMapping>
        {
            new SimulatedAnnealing(100, 1, 0.95, 100),
            new TabuSearch(50, 5, 1000),
        };
    }
}