using System;
using System.Collections.Generic;
using System.Text;
using QuantumCircuitTransformation.InitialMappingAlgorithm;
using QuantumCircuitTransformation.QuantumCircuitComponents;
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
        protected void ExecuteAllValidGates(Mapping mapping, PhysicalCircuit physicalCircuit, QuantumCircuit circuit)
        {
            physicalCircuit.AddGates(circuit.RemoveAllExecutableGates(mapping, physicalCircuit.ArchitectureGraph));
        }



        protected double MinChildCost(Mapping mapping, QuantumCircuit circuit)
        {
            List<CNOT> frontLayer = circuit.Layers[0]; // Niet aanpassen
            double minCost = double.MaxValue;
            
            
            throw new NotImplementedException();
        }


    }
}
