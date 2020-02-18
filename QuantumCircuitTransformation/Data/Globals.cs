using QuantumCircuitTransformation.Algorithms.InitialMappingAlgorithm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace QuantumCircuitTransformation.Data
{
    /// <summary>
    ///     Globals
    ///         A static class to keep track of all the global variables which 
    ///         could be needed in the project. 
    /// </summary>
    /// <remarks>
    ///     @author:   Louis Carpentier
    ///     @version:  1.2
    /// </remarks>
    public static class Globals
    {
        /// <summary>
        /// Variable referring to a random object, which can generate random
        /// numbers. This is to make sure that all random numbers are as much
        /// random as possible. If a random object would be instantiated 
        /// everytime a random number is needed, then there could be some bias
        /// which doesn't make it as random as possible.  
        /// </summary>
        public static readonly Random Random = new Random();

        /// <summary>
        /// Variable to easily time all algorithms durations. 
        /// </summary>
        public static readonly Stopwatch Timer = new Stopwatch();
    }
}