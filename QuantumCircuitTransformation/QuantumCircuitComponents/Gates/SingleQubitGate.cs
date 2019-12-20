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
    ///     @version:  2.0
    /// </remarks>
    public abstract class SingleQubitGate : Gate
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
        /// to operate on and no extra parameter. 
        /// </summary>
        /// <param name="gateNameShort"> The short name of this gate. </param>
        /// <param name="qubit"> The qubit this gate operates on. </param>
        public SingleQubitGate(string gateNameShort, int qubit)
        {
            GateNameShort = gateNameShort;
            Qubit = qubit;
        }

 
        /// <summary>
        /// See <see cref="Gate.ToString"/>.
        /// </summary>
        /// <returns>
        /// First the gatename, followed by the qubit on which it
        /// executes in squared brackets.
        /// </returns>
        public override string ToString()
        {
            return GateNameShort + " " + GetGateParameters() + " q[" + Qubit + "]" + ";";
        }

        /// <summary>
        /// Return the parameters of the gate. 
        /// </summary>
        /// <returns>
        /// The gate specific parameters. These should be between parentheses
        /// and each parameter should be seperated with a comma. 
        /// </returns>
        /// <remarks>
        /// The default implementation is no extra parameters. 
        /// </remarks>
        protected virtual string GetGateParameters()
        {
            return "";
        }

        /// <summary>
        /// See <see cref="Gate.GetQubits"/>.
        /// </summary>
        public List<int> GetQubits()
        {
            return new List<int> { Qubit };
        }

        /// <summary>
        /// See <see cref="Gate.GetGatePart(int)"/>. 
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
            throw new QubitIsNotPartOfGateException();
        }

        /// <summary>
        /// See <see cref="Gate.CompileToPhysical"/>.
        /// </summary>
        public abstract List<PhysicalGate> CompileToPhysical();

        /// <summary>
        /// See <see cref="Gate.CanBeExecutedOn(Architecture, Mapping)"/>.
        /// </summary>
        public bool CanBeExecutedOn(Architecture architecture, Mapping map)
        {
            return Qubit < architecture.NbNodes;
        }
    }
}