using QuantumCircuitTransformation.MappingPerturbation;
using QuantumCircuitTransformation.QuantumCircuitComponents.ArchitectureGraph;
using QuantumCircuitTransformation.QuantumCircuitComponents.Circuit;

namespace QuantumCircuitTransformation.Algorithms.TransformationAlgorithm
{
    /// <summary>
    ///     Transformation:
    ///         An abstract class to be used as base for transformation algorithms.
    /// </summary>
    /// <remarks>
    ///     @author:   Louis Carpentier
    ///     @version:  2.0
    /// </remarks>
    public abstract class Transformation : Algorithm
    {
        /// <summary>
        /// Variables in regards of the algorithm. 
        /// </summary>
        protected LogicalCircuit LogicalCircuit;
        protected Architecture Architecture;
        protected Mapping Mapping;
        protected PhysicalCircuit PhysicalCircuit;

        /// <summary>
        /// Transform the given circuit for the given architecture with given mapping. 
        /// </summary>
        /// <param name="circuit"> The dependency graph of the circuit to transform. </param>
        /// <param name="architecture"> The architecture to be used. </param>
        /// <param name="mapping"> The initial mapping to use during transformation. </param>
        /// <returns>
        /// A physical circuit which is equivalent to the given logical circuit
        /// and fits on the given architecture using the given mapping.  
        /// </returns>
        public PhysicalCircuit Execute(LogicalCircuit circuit, Architecture architecture, Mapping mapping)
        {
            LogicalCircuit = circuit;
            Architecture = architecture;
            Mapping = mapping;
            PhysicalCircuit = new PhysicalCircuit(architecture);

            SetUp();
            Execute();
            return PhysicalCircuit;
        }

        /// <summary>
        /// Abstract method which implements the transformation algorithm, making 
        /// use of the data stored in this class. 
        /// </summary>
        protected abstract void Execute();

        /// <summary>
        /// A method for algorithm specific setup. 
        /// </summary>
        protected virtual void SetUp() { }

        /// <summary>
        /// See <see cref="Algorithm.Name"/>.
        /// </summary>
        public abstract string Name();

        /// <summary>
        /// See <see cref="Algorithm.Parameters"/>.
        /// </summary>
        public abstract string Parameters();

        /// <summary>
        /// Adds the given swap to the physical circuit, according to 
        /// the architecture. And adjusts the mapping accordingly.
        /// </summary>
        /// <param name="q1"> The first qubit in the swap. </param>
        /// <param name="q2"> The second qubit in the swap. </param>
        protected void AddSwapToCircuit(int q1, int q2)
        {
            Swap swap = new Swap(q1, q2);
            Architecture.AddSwapGates(PhysicalCircuit, swap);
            Mapping.Swap(q1, q2);
        }
    }
}
