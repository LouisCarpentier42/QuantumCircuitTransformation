using QuantumCircuitTransformation.QuantumCircuitComponents.Gates;
using System.Collections.Generic;
using System.Linq;

namespace QuantumCircuitTransformation.QuantumCircuitComponents.Circuit
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    ///     @author:   Louis Carpentier
    ///     @version:  2.0
    /// </remarks>
    public class LogicalCircuit : QuantumCircuit
    {
        /// <summary>
        /// Variable referring to all the gates in this quantum circuit. The
        /// gates are sorted in layers, such that the execution of a gate in
        /// some layer doesn't affect the outcome of any other gate in the 
        /// same layer. 
        /// </summary>
        public readonly List<List<PhysicalGate>> Layers;
        /// <summary>
        /// Variable referring to the size of each layer. At index i is the number
        /// of gates in layer i of <see cref="Layers"/>.
        /// </summary>
        public readonly List<int> LayerSize;
        /// <summary>
        /// Variable referring to the number of layers this quantum circuit has. 
        /// </summary>
        public readonly int NbLayers;



        public LogicalCircuit(List<PhysicalGate> gates) : base(gates)
        {
            Layers = new List<List<PhysicalGate>> { new List<PhysicalGate>() };
            LayerSize = new List<int>();
            NbLayers = 0;

            for (int currentGate = 0; currentGate < gates.Count; currentGate++)
            {
                if (Layers[NbLayers].Any(gate => gate.GetQubits().Any(gates[currentGate].GetQubits().Contains)))
                {
                    Layers.Add(new List<PhysicalGate> { gates[currentGate] });
                    LayerSize.Add(1);
                    NbLayers++;
                } else
                {
                    Layers[NbLayers].Add(gates[currentGate]);
                    LayerSize[NbLayers]++;
                }
            }
        }
    }
}
