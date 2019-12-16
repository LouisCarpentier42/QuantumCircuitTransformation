﻿using System;
using System.Collections.Generic;
using System.Text;
using QuantumCircuitTransformation.Exceptions;

namespace QuantumCircuitTransformation.QuantumCircuitComponents.Gates
{
    /// <summary>
    ///     CNOT:
    ///         A class to keep track of a CNOT gate. A CNOT gate works on 
    ///         two qubits, a control qubit and a target qubit. The control 
    ///         qubit remains the same. On the target qubit is a NOT gate 
    ///         applied, only if the control qubit equals to 1.
    /// </summary>
    /// <remarks>
    ///     @author:   Louis Carpentier
    ///     @version:  1.6
    /// </remarks>
    public class CNOT : PhysicalGate
    {
        /// <summary>
        /// Variable to keep track of the control qubit of this CNOT gate.
        /// </summary>
        public readonly int ControlQubit;
        /// <summary>
        /// Variable to keep track of the target qubit of this CNOT gate.
        /// </summary>
        public readonly int TargetQubit;


        /// <summary>
        /// Initialise a new CNOT gate with given control and target qubit.
        /// </summary>
        /// <param name="controlQubit"> The control qubit for this CNOT gate. </param>
        /// <param name="targetQubit"> The target qubit for this CNOT gate. </param>
        public CNOT(int controlQubit, int targetQubit)
        {
            ControlQubit = controlQubit;
            TargetQubit = targetQubit;
        }


        /// <summary>
        /// See <see cref="PhysicalGate.ToString"/>.
        /// </summary>
        /// <returns>
        /// A CNOT gate is represented with a prefix "cx". Next is the ID
        /// of the control qubit given, followed by the ID of the target 
        /// qubit and seperseparatedated by a comma. 
        /// </returns>
        public override string ToString()
        {
            return "cx q[" + ControlQubit + "], q[" + TargetQubit + "];";
        }

        /// <summary>
        /// See <see cref="PhysicalGate.GetQubits"/>.
        /// </summary>
        public List<int> GetQubits()
        {
            return new List<int> { ControlQubit, TargetQubit };
        }

        /// <summary>
        /// See <see cref="PhysicalGate.GetGatePart(int)"/>. 
        /// </summary>
        public GatePart GetGatePart(int qubit)
        {
            if (qubit == ControlQubit)
                return GatePart.Control;
            if (qubit == TargetQubit)
                return GatePart.Target;
            throw new QubitIsNotPartOfGateException(qubit, this);
        }
    }
}