using System;
using System.Collections.Generic;
using System.Text;

namespace QuantumCircuitTransformation.QuantumCircuitComponents.Gates.LogicalGates
{
    public class Hadamard : SingleQubitGate
    {

        public Hadamard(int qubit) : base("H", qubit) { }

    }
}
