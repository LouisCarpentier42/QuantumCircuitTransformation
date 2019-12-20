using System;
using System.Collections.Generic;
using System.Text;
using QuantumCircuitTransformation.QuantumCircuitComponents.ArchitectureGraph;

namespace QuantumCircuitTransformation.QuantumCircuitComponents.Gates.PhysicalGates
{
    public class U3 : SingleQubitGate, PhysicalGate
    {
        /// <summary>
        /// The three angles for this U3 gate. 
        /// </summary>
        public readonly double Theta;
        public readonly double Phi;
        public readonly double Lambda;


        /// <summary>
        /// Initialise a new U3 gate.
        /// </summary>
        /// <param name="qubit"> The qubit on which this gate should operate. </param>
        /// <param name="theta"> The first angle of this U3 gate. /param>
        /// <param name="phi"> The second angle of this U3 gate. </param>
        /// <param name="lambda"> The third angle of this U3 gate. </param>
        public U3(int qubit, double theta, double phi, double lambda) : base("U3", qubit)
        {
            Theta = theta;
            Phi = phi;
            Lambda = lambda;
        }


        /// <summary>
        /// See <see cref="SingleQubitGate.GetGateParameters"/>.
        /// </summary>
        protected override string GetGateParameters()
        {
            return "(" + Theta + ", " + Phi + ", " + Lambda + ")";
        }

        /// <summary>
        /// See <see cref="Gate.CompileToPhysical"/>.
        /// </summary>
        public override List<PhysicalGate> CompileToPhysical()
        {
            return new List<PhysicalGate> { this };
        }
    }
}
