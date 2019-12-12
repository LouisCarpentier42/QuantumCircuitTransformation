using System;
using System.Collections.Generic;
using System.Text;
using QuantumCircuitTransformation.MappingPerturbation;
using QuantumCircuitTransformation.InitialMappingAlgorithm;
using QuantumCircuitTransformation.QuantumCircuitComponents.Circuit;
using QuantumCircuitTransformation.QuantumCircuitComponents.Architecture;

namespace QuantumCircuitTransformation.TransformationAlgorithm
{
    /// <summary>
    /// 
    /// 
    /// @author:   Louis Carpentier
    /// @version:  1.0
    /// 
    /// </summary>
    public abstract class Transformation : Algorithm
    {
        /// <summary>
        /// Execute this inital mapping algorithm. 
        /// </summary>
        /// <param name="architecture"> The architecture to find a mapping for. </param>
        /// <param name="circuit"> The circuit containing the qubits to map. </param>
        /// <returns>
        /// The best <see cref="Mapping"/> which has been found by the initial mapping algorithm. 
        /// </returns>
        public abstract PhysicalCircuit Execute(Mapping mapping, QuantumCircuit circuit, ArchitectureGraph architecture);

        /// <summary>
        /// See <see cref="Algorithm.Name"/>.
        /// </summary>
        public abstract string Name();

        /// <summary>
        /// See <see cref="Algorithm.Parameters"/>.
        /// </summary>
        public abstract string Parameters();





        /// <summary>
        /// Moves all gates which are executable after mapping the ID's 
        /// from the quantum circuit to the physical circuit. 
        /// </summary>
        protected (PhysicalCircuit, QuantumCircuit) ExecuteAllValidGates(Mapping mapping, PhysicalCircuit physicalCircuit, QuantumCircuit quantumCircuit)
        {
            PhysicalCircuit physicalCircuitClone = physicalCircuit.Clone();
            QuantumCircuit quantumCircuitClone = quantumCircuit.Clone();

            // Dit zal wss nog anders moeten, maar voorlopig ok
            // physicalCircuitClone.AddGates(quantumCircuitClone.RemoveAllExecutableGates(mapping, physicalCircuitClone.ArchitectureGraph));
            throw new NotImplementedException("Uncomment");


            //return (physicalCircuitClone, quantumCircuitClone);
        }



        protected double MinChildCost(Mapping mapping, QuantumCircuit circuit, ArchitectureGraph architecture)
        {
            //double minCost = double.MaxValue;
            //minCost++;

            //List<Tuple<int, int>> touchingEdges = architecture.GetAllTouchingEdges(circuit.Layers[0]);
            //for (int i = 0; i < touchingEdges.Count; i++)
            //{
            //    double gCost = architecture.NbOfCnotGatesPerSwap();
            //    double hCost = GetHCost();


            //}
            
            
            throw new NotImplementedException();
        }


        private double GetHCost()
        {
            throw new NotImplementedException();
        }


    }
}
