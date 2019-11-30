using System;
using System.Collections.Generic;
using System.Text;
using QuantumCircuitTransformation.InitialMappingAlgorithm;
using QuantumCircuitTransformation.QuantumCircuitComponents;
using QuantumCircuitTransformation.QuantumCircuitComponents.Architecture;

namespace QuantumCircuitTransformation.TransformationAlgorithm
{
    public class HeuristicSearch : Transformation
    {

        public HeuristicSearch()
        {

        }

        /// <summary>
        /// See <see cref="Algorithm.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// See <see cref="Algorithm.GetHashCode"/>.
        /// </summary>
        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// See <see cref="Algorithm.Name"/>.
        /// </summary>
        public override string Name()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// See <see cref="Algorithm.Parameters"/>.
        /// </summary>
        public override string Parameters()
        {
            throw new NotImplementedException();
        }




        public override PhysicalCircuit Execute(Mapping mapping, QuantumCircuit circuit, ArchitectureGraph architecture)
        {
            PhysicalCircuit physicalCircuit = new PhysicalCircuit(architecture);
            ExecuteAllValidGates(mapping, physicalCircuit, circuit);

            while (circuit.NbLayers > 0)
            {

            }


            throw new NotImplementedException();
        }




    
       
    }
}
