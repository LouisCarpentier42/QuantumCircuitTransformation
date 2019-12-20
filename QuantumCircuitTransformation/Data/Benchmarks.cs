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
    ///     @version:  1.0
    /// </remarks> 
    public class Benchmarks
    {
        /// <summary>
        /// List with all the benchmarks which are loaded. 
        /// </summary>
        public static List<FileInfo> LoadedBenchmarks = new List<FileInfo>();
    }
}
