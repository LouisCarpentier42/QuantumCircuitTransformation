using System;
using System.Collections.Generic;
using System.Text;
using QuantumCircuitTransformation.MappingPerturbation;
using QuantumCircuitTransformation.Algorithms.InitialMappingAlgorithm;
using QuantumCircuitTransformation.QuantumCircuitComponents.Circuit;
using QuantumCircuitTransformation.QuantumCircuitComponents.ArchitectureGraph;

namespace QuantumCircuitTransformation.Algorithms.TransformationAlgorithm
{
    /// <summary>
    ///     Transformation:
    ///         An abstract class to be used as base for transformation algorithms.
    /// </summary>
    /// <remarks>
    ///     @author:   Louis Carpentier
    ///     @version:  1.1
    /// </remarks>
    public abstract class Transformation : Algorithm
    {
        /// <summary>
        /// See <see cref="Algorithm.Name"/>.
        /// </summary>
        public abstract string Name();

        /// <summary>
        /// See <see cref="Algorithm.Parameters"/>.
        /// </summary>
        public abstract string Parameters();




        /// <summary>
        /// Transform the given circuit for the given architecture with given 
        /// initial mapping. 
        /// </summary>
        /// <param name="circuit"> The logical circuit to transform. </param>
        /// <param name="architecture"> The architecture to be used. </param>
        /// <param name="mapping"> The initial mapping to use during transformation. </param>
        /// <returns>
        /// A physical circuit which is equivalent to the given logical circuit
        /// and fits on the given architecture using the given mapping.  
        /// </returns>
        public PhysicalCircuit Execute(LogicalCircuit circuit, Architecture architecture, Mapping mapping)
        {
            return null;
        }

        protected abstract void Execute();




        protected List<Swap> GetValidMoves(Architecture architecture)
        {
            List<Swap> swaps = new List<Swap>();
            for (int i = 0; i < architecture.NbNodes; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    swaps.Add(new Swap(i, j));
                }
            }
            return swaps;
        }



    }
}
