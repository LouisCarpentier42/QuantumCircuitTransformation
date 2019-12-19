using System;
using QuantumCircuitTransformation.Exceptions;
using System.Collections.Generic;
using System.Text;
using QuantumCircuitTransformation.QuantumCircuitComponents.ArchitectureGraph;
using QuantumCircuitTransformation.MappingPerturbation;

namespace QuantumCircuitTransformation.QuantumCircuitComponents.Gates
{
    /// <summary>
    ///     SingleQubitGate
    ///         A class for single qubit gates. These are gates which
    ///         only operate on 1 qubit. 
    /// </summary>
    /// <remarks>
    ///     @author:   Louis Carpentier
    ///     @version:  1.8
    /// </remarks>
    public sealed class SingleQubitGate : PhysicalGate
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
        /// Variable referring to the extra parameter of this single
        /// qubit gate. If this variable equals null, then there is 
        /// no extra parameter needed. 
        /// </summary>
        public readonly object ExtraParam;


        /// <summary>
        /// Initialise a new single qubit gate with given name and qubit
        /// to operate on and no extra parameter. 
        /// </summary>
        /// <param name="gateNameShort"> The short name of this gate. </param>
        /// <param name="qubit"> The qubit this gate operates on. </param>
        public SingleQubitGate(string gateNameShort, int qubit)
            : this(gateNameShort, qubit, null) { }

        /// <summary>
        /// Initialise a new single qubit gate with given name and qubit
        /// to operate on and an extra parameter. 
        /// </summary>
        /// <param name="gateNameShort"> The short name of this gate. </param>
        /// <param name="qubit"> The qubit this gate operates on. </param>
        /// <param name="extraParam"> The value of the extra parameter. </param>
        private SingleQubitGate(string gateNameShort, int qubit, object extraParam)
        {
            GateNameShort = gateNameShort;
            Qubit = qubit;
            ExtraParam = extraParam;
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

        /// <summary>
        /// Get a gate which rotates the given qubit around the given axis
        /// with a given angle. 
        /// </summary>
        /// <param name="qubit"> The qubit for the gate to return. </param>
        /// <param name="axis"> The axis for the gate to return. </param>
        /// <param name="angle"> The angle in radians to rotate. </param>
        /// <returns>
        /// A new single qubit gate with given for rotating the qubit around
        /// the given axis with the given angle. 
        /// </returns>
        /// <exception cref="InvalidParameterException"> If the given axis is not the x, y or z axis. </exception>
        /// <remarks>
        /// The angle is normalised to be in the interval [0,2PI).
        /// </remarks>
        public static SingleQubitGate GetRotationalGate(int qubit, char axis, double angle)
        {
            if (axis != 'x' && axis != 'y' && axis != 'z')
                throw new InvalidParameterException("The given axis is invalid!");
            return new SingleQubitGate("R" + axis, qubit, Math.Abs(angle) % (2 * Math.PI));
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
            string extra = "";
            if (ExtraParam != null)
                extra += ", " + ExtraParam.ToString();
            return GateNameShort + " q[" + Qubit + "]" + extra + ";";
        }

        /// <summary>
        /// See <see cref="PhysicalGate.GetQubits"/>.
        /// </summary>
        public List<int> GetQubits()
        {
            return new List<int> { Qubit };
        }

        /// <summary>
        /// See <see cref="PhysicalGate.GetGatePart(int)"/>. 
        /// </summary>
        public GatePart GetGatePart(int qubit)
        {   
            if (qubit == Qubit)
            {
                try
                {
                    return (GatePart)Enum.Parse(typeof(GatePart), GateNameShort);
                }
                catch (ArgumentException)
                {
                    return GatePart.None;
                }
            }
            throw new QubitIsNotPartOfGateException(qubit, this);
        }

        /// <summary>
        /// See <see cref="PhysicalGate.CanBeExecutedOn(Architecture)"/>.
        /// </summary>
        public bool CanBeExecutedOn(Architecture architecture)
        {
            return Qubit < architecture.NbNodes;
        }

        /// <summary>
        /// See <see cref="PhysicalGate.Map(Mapping)"/>.
        /// </summary>
        public PhysicalGate Map(Mapping mapping)
        {
            return new SingleQubitGate(GateNameShort, mapping.Map[Qubit], ExtraParam);
        }
    }
}