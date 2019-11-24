using QuantumCircuitTransformation.InitalMappingAlgorithm;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuantumCircuitTransformation
{
    /// <summary>
    /// 
    /// LoadedAlgorithms:
    ///    A static class to keep track of the loaded algorithms. These 
    ///    are the ones preferred by the user. There are some default 
    ///    algorithms inserted. 
    ///    
    /// @author:   Louis Carpentier
    /// @version:  1.0
    /// 
    /// </summary>
    public static class LoadedAlgorithms
    {
        /// <summary>
        /// Variable to keep track of the loaded initial mapping algorithm. 
        /// </summary>
        public static InitialMapping InitialMapping = AllAlgorithms.InitialMappings[0];
    }
}
