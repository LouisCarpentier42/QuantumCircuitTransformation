using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using QuantumCircuitTransformation.Exceptions;
using QuantumCircuitTransformation.MappingPerturbation;
using QuantumCircuitTransformation.QuantumCircuitComponents.ArchitectureGraph;
using Architecture = QuantumCircuitTransformation.QuantumCircuitComponents.ArchitectureGraph.Architecture;

namespace QuantumCircuitTransformation.QuantumCircuitComponents.Gates
{
    /// <summary>
    ///     CNOT
    ///         A class to keep track of a CNOT gate. A CNOT gate works on 
    ///         two qubits, a control qubit and a target qubit. The control 
    ///         qubit remains the same. On the target qubit is a NOT gate 
    ///         applied, only if the control qubit equals to 1.
    /// </summary>
    /// <remarks>
    ///     @author:   Louis Carpentier
    ///     @version:  1.7
    /// </remarks>
    public class CNOT : Gate
    {
        /// <summary>
        /// Initialise a new CNOT gate with given control and target qubit.
        /// </summary>
        /// <param name="controlQubit"> The control qubit for this CNOT gate. </param>
        /// <param name="targetQubit"> The target qubit for this CNOT gate. </param>
        public CNOT(int controlQubit, int targetQubit) : 
            base(new List<int> { controlQubit, targetQubit }) { }

        /// <summary>
        /// Returns the control qubit of this CNOT gate.
        /// </summary>
        public int GetControlQubit()
        {
            return Qubits[0];
        }

        /// <summary>
        /// Returns the target qubit of this CNOT gate. 
        /// </summary>
        public int GetTargetQubit()
        {
            return Qubits[1];
        }

        /// <summary>
        /// See <see cref="Gate.ToString"/>.
        /// </summary>
        /// <returns>
        /// A CNOT gate is represented with a prefix "cx". Next is the ID
        /// of the control qubit given, followed by the ID of the target 
        /// qubit and seperseparatedated by a comma. 
        /// </returns>
        public override string ToString()
        {
            return "cx q[" + Qubits[0] + "], q[" + Qubits[1] + "];";
        }
        

        /// <summary>
        /// See <see cref="Gate.CanBeExecutedOn(Architecture)"/>.
        /// </summary>
        /// <returns> 
        /// True if and only if there exists a connection between the mapping of
        /// the control and target qubit in the given architecture. 
        /// </returns>
        public override bool CanBeExecutedOn(Architecture architecture, Mapping map)
        {
            return architecture.HasConnection(map.Map[Qubits[0]], map.Map[Qubits[1]]);
        }

        /// <summary>
        /// See <see cref="Gate.GetGatePart(int)"/>. 
        /// </summary>
        public override GatePart GetGatePart(int qubit)
        {
            if (qubit == Qubits[0])
                return GatePart.Control;
            if (qubit == Qubits[1])
                return GatePart.Target;
            throw new QubitIsNotPartOfGateException();
        }

        /// <summary>
        /// See <see cref="Gate.GetMaxQubit"/>.
        /// </summary>
        public override int GetMaxQubit()
        {
            return Math.Max(Qubits[0], Qubits[1]);
        }
    }
}