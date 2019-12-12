using QuantumCircuitTransformation.MappingPerturbation;
using QuantumCircuitTransformation.QuantumCircuitComponents.Gates;
using QuantumCircuitTransformation.Algorithms.InitialMappingAlgorithm;
using QuantumCircuitTransformation.QuantumCircuitComponents.Architecture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuantumCircuitTransformation.QuantumCircuitComponents.Circuit
{
    /// <summary>
    ///     QuantumCircuit
    ///         A class to represent any quantum circuit. In general this class 
    ///         represents logical circuits. See <see cref="PhysicalCircuit"/> 
    ///         for a class which can represent a physical circuit.
    /// </summary>
    /// <remarks>
    ///     @author:   Louis Carpentier
    ///     @version:  1.4
    /// </remarks>
    public class QuantumCircuit
    {
        /// <summary>
        /// Variable referring to all the gates in this quantum circuit. The
        /// gates are sorted in layers, such that the execution of a gate in
        /// some layer doesn't affect the outcome of any other gate in the 
        /// same layer. 
        /// </summary>
        public List<List<PhysicalGate>> Layers { get; private set; }
        /// <summary>
        /// Variable referring to the size of each layer. At index i is the number
        /// of gates in layer i of <see cref="Layers"/>.
        /// </summary>
        public List<int> LayerSize { get; private set; }
        /// <summary>
        /// Variable referring to the number of layers this quantum circuit has. 
        /// </summary>
        public int NbLayers { get; private set; }
        /// <summary>
        /// Variable referring to the number of gates in this quantum circuit. 
        /// </summary>
        public int NbGates { get; private set; }
        /// <summary>
        /// A variable to keep track of the number of qubits used in this circuit.
        /// </summary>
        public int NbQubits { get; private set; }


        /// <summary>
        /// Initialise a new Quantum circuit without any gates. 
        /// </summary>
        public QuantumCircuit()
        {
            Layers = new List<List<PhysicalGate>> { new List<PhysicalGate>() };
            LayerSize = new List<int> { 0 };
            NbLayers = 1;
            NbGates = 0;
            NbQubits = 0;
        }

        /// <summary>
        /// Initialise a new quantum circuit with given gates. 
        /// </summary>
        /// <param name="layers"> The gates of this quantum circuit, divided in layers. </param>
        /// <param name="layerSize"> The size of each layer. </param>
        /// <param name="nbLayers"> The number of layers. </param>
        /// <param name="nbGates"> The number of gates. </param>
        /// <param name="nbQubits"> The number of qubits in this </param>
        protected QuantumCircuit(List<List<PhysicalGate>> layers, List<int> layerSize, int nbLayers, int nbGates, int nbQubits)
        {
            Layers = layers;
            LayerSize = layerSize;
            NbLayers = nbLayers;
            NbGates = nbGates;
            NbQubits = nbQubits;
        }

        //public void AddGate(PhysicalGate newGate)
        //{
        //    throw new NotImplementedException();
        //}

        /// <summary>
        /// Adds a CNOT gate at the end of this quantum circuit. 
        /// </summary>
        /// <param name="newCNOT"> The gate to add to this circuit. </param>
        public virtual void AddGate(CNOT newCNOT)
        {
            if (Layers[0].Any(gate => ((CNOT)gate).TargetQubit == newCNOT.ControlQubit))
            {
                Layers.Add(new List<PhysicalGate> { newCNOT });
                LayerSize.Add(1);
                NbLayers++;
            }
            else
            {
                Layers[NbLayers - 1].Add(newCNOT);
                LayerSize[NbLayers - 1]++;
            }
            NbGates++;
            NbQubits = Math.Max(Math.Max(newCNOT.ControlQubit, newCNOT.TargetQubit), NbQubits);
        }

        ///// <summary>
        ///// Adds a list of CNOT gates to this quantum circuit using the method
        ///// <see cref="AddGate(PhysicalGate)"/>.
        ///// </summary>
        ///// <param name="newGates"> The gates to add to this circuit. </param>
        //public void AddGates(List<PhysicalGate> newGates)
        //{
        //    for (int i = 0; i < newGates.Count(); i++)
        //        AddGate(newGates[i]);
        //}


        /// <summary>
        /// Gives a string representation of this quantum circuit.  
        /// </summary>
        /// <returns>
        /// Initially is the command qreg given with the number of qubits
        /// used in this quantum circuit. Next are all the gates given in 
        /// this circuit. 
        /// </returns>
        public override string ToString()
        {
            string codeRepresenation =
                "OPENQASM 2.0;\n" +
                "include \"qelib1.inc\";\n\n" +
                "qreg q[" + NbQubits + "];\n" +
                "creg q[" + NbQubits + "];\n"; ;
            for (int i = 0; i < NbLayers; i++)
                for (int j = 0; j < Layers[i].Count; j++)
                    codeRepresenation += "\n" + Layers[i][j];
            return codeRepresenation;
        }
    
        /// <summary>
        /// Clone this quantum circuit. 
        /// </summary>
        /// <returns>
        /// A new quantum circuit with the properties of this quantum circuit. 
        /// </returns>
        public QuantumCircuit Clone()
        {
            (List<List<PhysicalGate>> layersCloned, List<int> layerSizeCloned) = CopyProperties();
            return new QuantumCircuit(layersCloned, layerSizeCloned, NbLayers, NbGates, NbQubits);
        }

        /// <summary>
        /// Copy the properties of this quantum circuit. 
        /// </summary>
        /// <returns>
        /// A copy of the CNOT gates and of the sizes of each layer. 
        /// </returns>
        protected (List<List<PhysicalGate>>, List<int>) CopyProperties()
        {
            List<List<PhysicalGate>> layersCloned = new List<List<PhysicalGate>>(NbLayers);
            for (int i = 0; i < NbLayers; i++)
                layersCloned[i] = Layers[i].Select(gate => gate.Clone()).ToList();
            List<int> layerSizeCloned = LayerSize.GetRange(0, NbLayers);
            return (layersCloned, layerSizeCloned);
        }
    }
}