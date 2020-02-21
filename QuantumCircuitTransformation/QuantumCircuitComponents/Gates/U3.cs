using System;
using System.Collections.Generic;
using System.Text;
using QuantumCircuitTransformation.Exceptions;
using QuantumCircuitTransformation.MappingPerturbation;
using QuantumCircuitTransformation.QuantumCircuitComponents.ArchitectureGraph;

namespace QuantumCircuitTransformation.QuantumCircuitComponents.Gates
{
    /// <summary>
    ///     U3
    ///         A class for U3 gates. This is a gate which operates
    ///         on a single qubit and can be physically implemented. 
    /// </summary>
    /// <remarks>
    ///     @author:   Louis Carpentier
    ///     @version:  1.2
    /// </remarks>
    public class U3 : Gate
    {
        /// <summary>
        /// The three angles for this U3 gate. 
        /// </summary>
        private readonly double Theta;
        private readonly double Phi;
        private readonly double Lambda;
        /// <summary>
        /// The gatepart, representing which gate this is
        /// </summary>
        private readonly GatePart GatePart;

        /// <summary>
        /// Initialise a new U3 gate.
        /// </summary>
        /// <param name="qubit"> The qubit on which this gate should operate. </param>
        /// <param name="theta"> The first angle of this U3 gate. /param>
        /// <param name="phi"> The second angle of this U3 gate. </param>
        /// <param name="lambda"> The third angle of this U3 gate. </param>
        /// <param name="gatePart"> The gatepart this U3 gate represents. </param>
        public U3(int qubit, double theta, double phi, double lambda, GatePart gatePart)
            : base(new List<int> { qubit })
        {
            Theta = theta;
            Phi = phi;
            Lambda = lambda;
            GatePart = gatePart;
        }

        /// <summary>
        /// Gives a string representation of this gate. 
        /// </summary>
        public override string ToString()
        {
            return "U(" + Theta + ", " + Phi + ", " + Lambda + ") q[" + Qubits[0]+ "];";
        }


        /// <summary>
        /// See <see cref="Gate.Map(Mapping)"/>.
        /// </summary>
        public override Gate Map(Mapping mapping)
        {
            return new U3(mapping.Map[Qubits[0]], Theta, Phi, Lambda, GatePart);
        }

        /// <summary>
        /// See <see cref="Gate.CanBeExecutedOn(Architecture, Mapping)"/>.
        /// </summary>
        /// <returns>
        /// Always true. There are no restrictions on U3 gates to be executable
        /// or not on any device. 
        /// </returns>
        /// <remarks>
        /// The given architecture should be big enough so that the qubit this
        /// U3 gate operates on is a part of the device. 
        /// </remarks>
        public override bool CanBeExecutedOn(Architecture architecture, Mapping map)
        {
            return true;
        }

        /// <summary>
        /// See <see cref="Gate.GetGatePart(int)"/>
        /// </summary>
        public override GatePart GetGatePart(int qubit)
        {
            if (Qubits[0] == qubit)
                return GatePart;
            throw new QubitIsNotPartOfGateException();
        }

        /// <summary>
        /// See <see cref="Gate.GetMaxQubit"/>.
        /// </summary>
        public override int GetMaxQubit()
        {
            return Qubits[0];
        }
    }
}
