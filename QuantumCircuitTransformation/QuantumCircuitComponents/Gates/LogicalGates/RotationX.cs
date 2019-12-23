using QuantumCircuitTransformation.QuantumCircuitComponents.Gates.PhysicalGates;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuantumCircuitTransformation.QuantumCircuitComponents.Gates.LogicalGates
{
    public class RotationX : RotationGate
    {
        /// <summary>
        /// Initialise a new Rx gate. 
        /// </summary>
        /// <param name="qubit"> The qubit this gate should operate on. </param>
        /// <param name="angle"> The angle to rotate over. </param>
        public RotationX(int qubit, double angle) : base(qubit, 'x', angle) { }

        /// <summary>
        /// See <see cref="Gate.CompileToPhysicalGate"/>.
        /// </summary>
        public override PhysicalGate CompileToPhysicalGate()
        {
            return new U3(Qubit, Angle, -Math.PI / 2, Math.PI / 2);
        }
    }
}
