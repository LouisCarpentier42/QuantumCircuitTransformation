using System;
using System.Collections.Generic;
using System.Text;
using QuantumCircuitTransformation.MappingPerturbation;
using QuantumCircuitTransformation.Algorithms.InitialMappingAlgorithm;
using QuantumCircuitTransformation.QuantumCircuitComponents.Circuit;
using QuantumCircuitTransformation.QuantumCircuitComponents.ArchitectureGraph;
using QuantumCircuitTransformation.QuantumCircuitComponents.Gates;

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
            SetUp(circuit, architecture, mapping);
            Execute();
            return PhysicalCircuit;
        }

        /// <summary>
        /// Abstract method which implements the transformation algorithm, making 
        /// use of the data stored in this class. 
        /// </summary>
        protected abstract void Execute();

        /// <summary>
        /// Checks whether or not the circuit is fully transformed
        /// </summary>
        /// <returns>
        /// True if and only if there are no more gates who are no more
        /// gates to be resolved. 
        /// </returns>
        protected bool CircuitIsTransformed()
        {
            return GatesToResolve.Count == 0;
        }

        /// <summary>  
        /// Set up the data to execute the algorithm. 
        /// </summary>
        /// <param name="circuit"> The logical circuit to transform. </param>
        /// <param name="architecture"> The architecture to be used. </param>
        /// <param name="mapping"> The initial mapping to use during transformation. </param>
        private void SetUp(LogicalCircuit circuit, Architecture architecture, Mapping mapping)
        {
            PhysicalCircuit = new PhysicalCircuit(architecture);
            LogicalCircuit = circuit;
            Architecture = architecture;
            Mapping = mapping;
        }

        


        protected PhysicalCircuit PhysicalCircuit;
        protected LogicalCircuit LogicalCircuit;
        protected Architecture Architecture;
        protected Mapping Mapping;



        /// <summary>
        /// Variable referring to the gates which should be resolved, but only 
        /// those which are not blocking any other gate. 
        /// </summary>
        protected List<int> GatesToResolve;



        /// <summary>
        /// Returns the best swap move.
        /// </summary>
        /// <returns></returns>
        protected Swap GetBestMove()
        {
            List<Swap> swaps = new List<Swap>();
            for (int i = 0; i < Architecture.NbNodes; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    swaps.Add(new Swap(i, j));
                }
            }
            //return swaps;
            throw new NotImplementedException(); // TODO best move
        }


        /// <summary>
        /// Transforms as mucht as possible from the logical circuit 
        /// into the physical circuit.
        /// </summary>
        protected void Transform()
        {
            List<Gate> executableGates = new List<Gate>(); 
            while (executableGates.Count > 0)
            {
                // Normal execution
                // bridge execution
            }
            throw new NotImplementedException(); // TODO transform circuit
        }
    }
}
