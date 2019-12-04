using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuantumCircuitTransformation.QuantumCircuitComponents.Architecture;

namespace QuantumCircuitTransformation.QuantumCircuitComponents
{
    /// <summary>
    ///     PhysicalCircuit
    ///         A class to represent a physical quantum circuit. This is a
    ///         quantum circuit which can be executed on a physical device,
    ///         given by an architecture graph. 
    /// </summary>
    /// <remarks>
    ///     @author:   Louis Carpentier
    ///     @version:  1.3
    /// </remarks>
    public class PhysicalCircuit : QuantumCircuit
    {
        /// <summary>
        /// A variable to keep track of the architecture graph of the 
        /// physical device. 
        /// </summary>
        public readonly ArchitectureGraph ArchitectureGraph;


        /// <summary>
        /// Initialise a new physical circuit with given architecture graph.
        /// </summary>
        /// <param name="architectureGraph"> The architecture graph of the physical device. </param>
        public PhysicalCircuit(ArchitectureGraph architectureGraph)
        {
            ArchitectureGraph = architectureGraph;
        }

        /// <summary>
        /// Initialise a new physical circuit with given gates. 
        /// </summary>
        /// <param name="layers"> The gates of this physical circuit, divided in layers. </param>
        /// <param name="layerSize"> The size of each layer in this physical circuit.. </param>
        /// <param name="nbLayers"> The number of layers in this physical circuit.. </param>
        /// <param name="nbGates"> The number of gates in this physical circuit.. </param>
        /// <param name="nbQubits"> The number of qubits in this physical circuit. </param>
        /// <param name="architectureGraph"> The architecture for this physical circuit. </param>
        protected PhysicalCircuit(List<List<CNOT>> layers, List<int> layerSize, int nbLayers, int nbGates, int nbQubits, ArchitectureGraph architectureGraph) : 
            base(layers, layerSize, nbLayers, nbGates, nbQubits)
        {
            ArchitectureGraph = architectureGraph;
        }

        /// <summary>
        /// Adds the given CNOT gate only if it can be exucuted on the architecture of 
        /// the physical device. 
        /// </summary>
        /// <param name="cnot"> The CNOT gate to add to this circuit. </param>
        public override void AddGate(CNOT cnot)
        {
            if (ArchitectureGraph.CanExecuteCNOT(cnot))
                base.AddGate(cnot);
        }

        /// <summary>
        /// Clone this physical circuit. 
        /// </summary>
        /// <returns>
        /// A new physical circuit with the same properties of this circuit.
        /// </returns>
        public new PhysicalCircuit Clone()
        {
            (List<List<CNOT>> layersCloned, List<int> layerSizeCloned) = CopyProperties();
            return new PhysicalCircuit(layersCloned, layerSizeCloned, NbLayers, NbGates, NbQubits, ArchitectureGraph);
        }
    }
}