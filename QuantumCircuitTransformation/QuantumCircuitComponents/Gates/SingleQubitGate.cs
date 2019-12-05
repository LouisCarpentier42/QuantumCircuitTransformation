using System;
using System.Collections.Generic;
using System.Text;

namespace QuantumCircuitTransformation.QuantumCircuitComponents.Gates
{
    /// <summary>
    ///     SingleQubitGate:
    ///         A class for single qubit gates. These are gates which
    ///         only operate on 1 qubit. 
    /// </summary>
    /// <remarks>
    ///     @author:   Louis Carpentier
    ///     @version:  1.1
    /// </remarks>
    public class SingleQubitGate : PhysicalGate
    {
        /// <summary>
        /// The short name for this single qubit gate.
        /// </summary>
        /// <example>
        /// The short name of the Hadamerd gate is 'H'.
        /// </example>
        public readonly string GateNameShort;
        /// <summary>
        /// The qubit this single qubit gate operates on. 
        /// </summary>
        public readonly int Qubit;


        /// <summary>
        /// Initialise a new single qubit gate with given name and qubit
        /// to operate on. 
        /// </summary>
        /// <param name="gateNameShort"> The short name of this gate. </param>
        /// <param name="qubit"> The qubit this gate operates on. </param>
        private SingleQubitGate(string gateNameShort, int qubit)
        {
            GateNameShort = gateNameShort;
            Qubit = qubit;
        }

        /// <summary>
        /// See <see cref="PhysicalGate.Clone"/>.
        /// </summary>
        public PhysicalGate Clone()
        {
            return new SingleQubitGate(GateNameShort, Qubit);
        }

        /// <summary>
        /// See <see cref="PhysicalGate.ToString"/>.
        /// </summary>
        /// <returns>
        /// First the gatename, followed by the qubit on which it
        /// executes in squared brackets.
        /// </returns>
        public override string ToString()
        {
            return GateNameShort + " q[" + Qubit + "];";
        }


        /// <summary>
        /// Return a Hadamard gate. 
        /// </summary>
        /// <param name="qubit"> The qubit on which this hadamard gate should operate. </param>
        /// <returns>
        /// A new single qubit gate which operates on the given qubit and 
        /// has the short gate name 'H'.
        /// </returns>
        public static SingleQubitGate GetHadamardGate(int qubit)
        {
            return new SingleQubitGate("H", qubit);
        }
    }
}