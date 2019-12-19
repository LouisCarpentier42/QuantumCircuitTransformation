﻿using QuantumCircuitTransformation.QuantumCircuitComponents.Gates;
using System.Collections.Generic;
using System.Linq;

namespace QuantumCircuitTransformation.QuantumCircuitComponents.Circuit
{
    /// <summary>
    ///     LogicalCircuit
    ///         A class for logical circuits. This is a circuit which
    ///         is independent from any physical device. 
    /// </summary>
    /// <remarks>
    ///     @author:   Louis Carpentier
    ///     @version:  2.0
    /// </remarks>
    public sealed class LogicalCircuit : QuantumCircuit<Gate>
    {
        // TODO implement only dependency graph => these variables should be removed, also in constructor!!!
        public readonly List<List<Gate>> Layers;
        public readonly List<int> LayerSize;
        public readonly int NbLayers;


        public LogicalCircuit(List<Gate> gates) : base(gates)
        {
            Layers = new List<List<Gate>> { new List<Gate>() };
            LayerSize = new List<int>();
            NbLayers = 0;

            for (int currentGate = 0; currentGate < gates.Count; currentGate++)
            {
                if (!Layers[NbLayers].Any(gate => gate.GetQubits().Any(gates[currentGate].GetQubits().Contains)))
                {
                    Layers.Add(new List<Gate> { gates[currentGate] });
                    LayerSize.Add(1);
                    NbLayers++;
                }
                else
                {
                    Layers[NbLayers].Add(gates[currentGate]);
                    LayerSize[NbLayers - 1]++;
                }
            }
        }
    }
}
