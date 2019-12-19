using System;
using System.Collections.Generic;
using System.Text;
using QuantumCircuitTransformation.QuantumCircuitComponents.ArchitectureGraph;

namespace QuantumCircuitTransformation.QuantumCircuitComponents.Gates.PhysicalGates
{
    public class U3 : SingleQubitGate, PhysicalGate
    {
        public readonly double Theta;
        public readonly double Phi;
        public readonly double Lambda;


        public U3(int qubit, double theta, double phi, double lambda) : base("U3", qubit)
        {
            Theta = theta;
            Phi = phi;
            Lambda = lambda;
        }

        
        protected override string GetGateParameters()
        {
            return "(" + Theta + ", " + Phi + ", " + Lambda + ")";
        }


        public bool CanBeExecutedOn(Architecture architecture)
        {
            return true;
        }

    }
}
