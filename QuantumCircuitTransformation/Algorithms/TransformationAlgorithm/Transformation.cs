using System;
using System.Collections.Generic;
using System.Text;
using QuantumCircuitTransformation.MappingPerturbation;
using QuantumCircuitTransformation.Algorithms.InitialMappingAlgorithm;
using QuantumCircuitTransformation.QuantumCircuitComponents.Circuit;
using QuantumCircuitTransformation.QuantumCircuitComponents.ArchitectureGraph;
using QuantumCircuitTransformation.QuantumCircuitComponents.Gates;
using QuantumCircuitTransformation.DependencyGraphs;

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
        /// Transform the given circuit for the given architecture with given 
        /// initial mapping. 
        /// </summary>
        /// <param name="dependencyGraph"> The dependency graph of the circuit to transform. </param>
        /// <param name="architecture"> The architecture to be used. </param>
        /// <param name="mapping"> The initial mapping to use during transformation. </param>
        /// <returns>
        /// A physical circuit which is equivalent to the given logical circuit
        /// and fits on the given architecture using the given mapping.  
        /// </returns>
        public PhysicalCircuit Execute(DependencyGraph dependencyGraph, Architecture architecture, Mapping mapping)
        {
            SetUp(dependencyGraph, architecture, mapping);
            Execute();
            return PhysicalCircuit;
        }

        /// <summary>
        /// Abstract method which implements the transformation algorithm, making 
        /// use of the data stored in this class. 
        /// </summary>
        protected abstract void Execute();

        /// <summary>  
        /// Set up the data to execute the algorithm. 
        /// </summary>
        /// <param name="circuit"> The dependency graph of the circuit to transform. </param>
        /// <param name="architecture"> The architecture to be used. </param>
        /// <param name="mapping"> The initial mapping to use during transformation. </param>
        private void SetUp(DependencyGraph dependencyGraph, Architecture architecture, Mapping mapping)
        {
            DependencyGraph = dependencyGraph;
            Architecture = architecture;
            Mapping = mapping;

            PhysicalCircuit = new PhysicalCircuit(architecture);

            FirstLayer = new List<int>();
            for (int i = 0; i < dependencyGraph.Circuit.NbGates; i++)
                if (dependencyGraph.CanBeExecuted(i))
                    FirstLayer.Add(i);

            ExecuteAllPossibleGates();
        }


        /// <summary>
        /// See <see cref="Algorithm.Name"/>.
        /// </summary>
        public abstract string Name();

        /// <summary>
        /// See <see cref="Algorithm.Parameters"/>.
        /// </summary>
        public abstract string Parameters();


        // in
        protected DependencyGraph DependencyGraph;
        protected Architecture Architecture;
        protected Mapping Mapping;

        // out
        protected PhysicalCircuit PhysicalCircuit;


        /// <summary>
        /// Variable referring to the gates in the first layer of the circuit 
        /// to be transformed. 
        /// </summary>
        protected List<int> FirstLayer;




        /// <summary>
        /// Checks whether or not the circuit is fully transformed
        /// </summary>
        /// <returns>
        /// True if and only if there are no more gates who are no more
        /// gates to be resolved, thus no gates in the first layer.  
        /// </returns>
        protected bool CircuitIsTransformed()
        {
            return FirstLayer.Count == 0;
        }

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
            return swaps[0]; // TODO best move
        }

        /// <summary>
        /// Adds the given swap to the physical circuit, according to 
        /// the architecture.
        /// </summary>
        /// <param name="swap"> The swap to add to the physical circuit. </param>
        protected void AddMoveToCircuit(Swap swap)
        {
            Architecture.AddSwapGates(PhysicalCircuit, swap);
        }

        /// <summary>
        /// Transforms as mucht as possible from the logical circuit 
        /// into the physical circuit.
        /// </summary>
        protected void ExecuteAllPossibleGates()
        {
            // TODO optimise: only get gates from first layer on which the swap has been applied. 
            int gateID = 0;
            List<int> gatesToCheck = new List<int>(FirstLayer);
            while (gatesToCheck.Count > gateID)
            {
                Gate gate = DependencyGraph.Circuit.Gates[gatesToCheck[gateID]];
                gatesToCheck.RemoveAt(gateID);

                if (gate.CanBeExecutedOn(Architecture, Mapping))
                {
                    PhysicalCircuit.AddGate(gate.Map(Mapping));
                    DependencyGraph.SimulateExecution(gatesToCheck[gateID]);
                    foreach (int newGateToCheck in DependencyGraph.ExecuteAfter[gateID])
                    {
                        if (!gatesToCheck.Contains(newGateToCheck) && DependencyGraph.CanBeExecuted(newGateToCheck))
                            gatesToCheck.Add(newGateToCheck);
                    }
                }
                // TODO maybe bridge gates.
            }
            FirstLayer = new List<int>(gatesToCheck);
        }
    }
}
