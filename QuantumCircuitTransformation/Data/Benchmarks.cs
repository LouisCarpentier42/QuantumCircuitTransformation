using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace QuantumCircuitTransformation.Data
{
    /// <summary>
    ///     Benchmarks
    ///         A class to keep track of all the loaded benchmarks
    /// </summary>
    /// <remarks>
    ///     @author:   Louis Carpentier
    ///     @version:  1.1
    /// </remarks> 
    public class Benchmarks
    {
        /// <summary>
        /// List with all the benchmarks which are loaded. 
        /// </summary>
        public static List<FileInfo> LoadedBenchmarks = new List<FileInfo>();

        /// <summary>
        /// Variable referring to the folder in which the benchmarks inside 
        /// this project. The folder should be inside this project with the 
        /// name "Benchmarks". 
        /// </summary>
        public static readonly string BenchmarkFolder =
            Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName) + "/BenchmarkFiles/";

        /// <summary>
        /// Variable referring to a list of all the benchmark files in this project.
        /// </summary>
        public static FileInfo[] AllBenchmarks { get => new DirectoryInfo(BenchmarkFolder).GetFiles(); }
    }
}
