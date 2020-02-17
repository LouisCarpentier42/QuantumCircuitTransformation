using QuantumCircuitTransformation.QuantumCircuitComponents.Gates;
using System;
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
    public sealed class LogicalCircuit : QuantumCircuit
    {

        public readonly List<int> CnotGatesID;
        public readonly int NbCnotGates;

        public LogicalCircuit(List<Gate> gates) : base(gates)
        {
            CnotGatesID = new List<int>();
            for (int i = 0; i < gates.Count; i++)
            {
                if (gates[i] is CNOT)
                    CnotGatesID.Add(i);
            }
            NbCnotGates = CnotGatesID.Count();
        }
    }
}
