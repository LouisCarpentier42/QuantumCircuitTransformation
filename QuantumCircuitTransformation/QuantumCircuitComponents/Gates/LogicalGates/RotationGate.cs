using System;
using System.Collections.Generic;
using System.Text;

namespace QuantumCircuitTransformation.QuantumCircuitComponents.Gates.LogicalGates
{
    public abstract class RotationGate : SingleQubitGate
    {
        /// <summary>
        /// The angle this rotation gate rotates around. 
        /// </summary>
        public readonly double Angle;

        /// <summary>
        /// Initialise a new rotation gate. 
        /// </summary>
        /// <param name="qubit"> The qubit this gate should operate on. </param>
        /// <param name="axis"> The axis to rotate around. </param>
        /// <param name="angle"> The angle to rotate over. </param>
        public RotationGate(int qubit, char axis, double angle) : base("R" + axis, qubit)
        {
            Angle = angle;
        }

        /// <summary>
        /// See <see cref="SingleQubitGate.GetGateParameters"/>.
        /// </summary>
        protected override string GetGateParameters()
        {
            return "(" + Angle + ")";
        }
    }
}
