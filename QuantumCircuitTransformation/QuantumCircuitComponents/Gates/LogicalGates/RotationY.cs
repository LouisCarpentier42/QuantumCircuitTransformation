using QuantumCircuitTransformation.QuantumCircuitComponents.Gates.PhysicalGates;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuantumCircuitTransformation.QuantumCircuitComponents.Gates.LogicalGates
{
    public class RotationY : RotationGate
    {
        /// <summary>
        /// Initialise a new Ry gate. 
        /// </summary>
        /// <param name="qubit"> The qubit this gate should operate on. </param>
        /// <param name="angle"> The angle to rotate over. </param>
        public RotationY(int qubit, double angle) : base(qubit, 'y', angle) { }

        /// <summary>
        /// See <see cref="Gate.CompileToPhysicalGate"/>.
        /// </summary>
        public override PhysicalGate CompileToPhysicalGate()
        {
            return  new U3(Qubit, Angle, 0, 0);
        }
    }
}
