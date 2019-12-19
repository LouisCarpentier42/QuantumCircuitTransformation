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
        /// Variable referring to the folder in which the benchmarks inside 
        /// this project. The folder should be inside this project with the 
        /// name "Benchmarks". 
        /// </summary>
        public static readonly string BenchmarkFolder = 
            Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName) + "/Benchmarks/";

        /// <summary>
        /// Variable referring to a list of all the benchmark files in this project.
        /// </summary>
        public static readonly FileInfo[] Benchmarks = 
            new DirectoryInfo(BenchmarkFolder).GetFiles();

        /// <summary>
        /// Variable to easily time all algorithms durations. 
        /// </summary>
        public static readonly Stopwatch Timer = new Stopwatch();
    }
}