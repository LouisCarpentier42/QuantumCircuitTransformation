using QuantumCircuitTransformation.InitialMappingAlgorithm;
using QuantumCircuitTransformation.QuantumCircuitComponents.Architecture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuantumCircuitTransformation.QuantumCircuitComponents
{
    /// <summary>
    ///     QuantumCircuit
    ///         A class to represent any quantum circuit. In general this class 
    ///         represents logical circuits. See <see cref="PhysicalCircuit"/> 
    ///         for a class which can represent a physical circuit.
    /// </summary>
    /// <remarks>
    ///     @author:   Louis Carpentier
    ///     @version:  1.3
    /// </remarks>
    public class QuantumCircuit
    {
        /// <summary>
        /// Variable referring to all the gates in this quantum circuit. The
        /// gates are sorted in layers, such that the execution of a gate in
        /// some layer doesn't affect the outcome of any other gate in the 
        /// same layer. 
        /// </summary>
        public List<List<CNOT>> Layers { get; private set; }
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
            Layers = new List<List<CNOT>> { new List<CNOT>() };
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
        protected QuantumCircuit(List<List<CNOT>> layers, List<int> layerSize, int nbLayers, int nbGates, int nbQubits)
        {
            Layers = layers;
            LayerSize = layerSize;
            NbLayers = nbLayers;
            NbGates = nbGates;
            NbQubits = nbQubits;
        }


        /// <summary>
        /// Adds a CNOT gate at the end of this quantum circuit. 
        /// </summary>
        /// <param name="newGate"> The gate to add to this circuit. </param>
        public virtual void AddGate(CNOT newGate)
        {
            if (Layers[0].Any(cnot => cnot.TargetQubit == newGate.ControlQubit))
            {
                Layers.Add(new List<CNOT> { newGate });
                LayerSize.Add(1);
                NbLayers++;
            }
            else
            {
                Layers[NbLayers - 1].Add(newGate);
                LayerSize[NbLayers - 1]++;
            }
            NbGates++;
            NbQubits = Math.Max(Math.Max(newGate.ControlQubit, newGate.TargetQubit), NbQubits);
        }

        /// <summary>
        /// Adds a list of CNOT gates to this quantum circuit using the method
        /// <see cref="AddGate(CNOT)"/>.
        /// </summary>
        /// <param name="newGates"> The gates to add to this circuit. </param>
        public void AddGates(List<CNOT> newGates)
        {
            for (int i = 0; i < newGates.Count(); i++)
                AddGate(newGates[i]);
        }

        /// <summary>
        /// Removes all the CNOT gates which can be executed according to the 
        /// architecture graph with the given mapping. 
        /// </summary>
        /// <param name="mapping"> The mapping to take into account. </param>
        /// <param name="architecture"> The architecture graph to take into account. </param>
        /// <returns>
        /// A list of all the cnot gates which are removed. 
        /// </returns>
        public List<CNOT> RemoveAllExecutableGates(Mapping mapping, ArchitectureGraph architecture)
        {
            List<CNOT> executableGates = Layers[0].FindAll(cnot => architecture.CanExecuteCNOT(mapping.MapCNOT(cnot)));
            Layers[0].RemoveAll(cnot => architecture.CanExecuteCNOT(mapping.MapCNOT(cnot)));
            while (Layers[0].Count() == 0)
            {
                NbLayers--;
                Layers.RemoveAt(0);
                executableGates.AddRange(Layers[0].FindAll(cnot => architecture.CanExecuteCNOT(mapping.MapCNOT(cnot))));
                Layers[0].RemoveAll(cnot => architecture.CanExecuteCNOT(mapping.MapCNOT(cnot)));
            }
            NbGates -= executableGates.Count();
            return executableGates;
        }

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
            List<List<CNOT>> layersCloned = new List<List<CNOT>>(NbLayers);
            for (int i = 0; i < NbLayers; i++)
                layersCloned[i] = Layers[i].Select(cnot => cnot.Clone()).ToList();
            List<int> layerSizeCloned = LayerSize.GetRange(0, NbLayers);
            return new QuantumCircuit(layersCloned, layerSizeCloned, NbLayers, NbGates, NbQubits);
        }
    }
}