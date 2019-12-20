﻿using System;
using System.Collections.Generic;
using System.Text;

namespace QuantumCircuitTransformation.QuantumCircuitComponents.Gates.LogicalGates
{
    public class Hadamard : SingleQubitGate
    {
        /// <summary>
        /// Initialise a new Hadamard gate. 
        /// </summary>
        /// <param name="qubit"> The qubit on which this gate should operate. </param>
        public Hadamard(int qubit) : base("H", qubit) { }

        /// <summary>
        /// See <see cref="Gate.CompileToPhysical"/>.
        /// </summary>
        public override List<PhysicalGate> CompileToPhysical()
        {
            throw new NotImplementedException();
        }
    }
}
