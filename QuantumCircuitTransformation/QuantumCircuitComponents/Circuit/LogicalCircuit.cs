﻿using QuantumCircuitTransformation.QuantumCircuitComponents.Gates;
using QuantumCircuitTransformation.QuantumCircuitComponents.Gates.PhysicalGates;
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

        public readonly List<CNOT> CnotGates;
        public readonly int NbCnotGates;

        public LogicalCircuit(List<Gate> gates) : base(gates)
        {
            CnotGates = new List<CNOT>();
            foreach (CNOT cnot in gates)
                CnotGates.Add(cnot);
            NbCnotGates = CnotGates.Count();
        }
    }
}
