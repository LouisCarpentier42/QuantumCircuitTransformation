using QuantumCircuitTransformation.MappingPerturbation;
using QuantumCircuitTransformation.Data;
using QuantumCircuitTransformation.QuantumCircuitComponents.Circuit;
using QuantumCircuitTransformation.QuantumCircuitComponents.Architecture;
using System;
using System.Linq;
using QuantumCircuitTransformation.QuantumCircuitComponents.Gates;

namespace QuantumCircuitTransformation.Algorithms.InitialMappingAlgorithm
{
    /// <summary>
    /// 
    /// InitialMappingAlgorithm:
    ///    An abstract class which serves as a parent for any initial
    ///    mapping algorithm. This class offers different methods which
    ///    can serve to easily implement the desired algorithm. These 
    ///    can be overwritten if one wishes to do so. 
    ///    An initial mapping maps the qubits from a logical circuit on 
    ///    the physical qubits of an architecture. This can enormously 
    ///    improve the efficiency of a quantum circuit transformation 
    ///    algorithm.
    /// 
    /// @author:   Louis Carpentier
    /// @version:  1.9
    /// 
    /// </summary>
    public abstract class InitialMapping : Algorithm
    {
        /// <summary>
        /// Execute this inital mapping algorithm. 
        /// </summary>
        /// <param name="architecture"> The architecture to find a mapping for. </param>
        /// <param name="circuit"> The circuit containing the qubits to map. </param>
        /// <returns>
        /// The best <see cref="Mapping"/> which has been found by the initial mapping algorithm. 
        /// </returns>
        public abstract (Mapping,double) Execute(ArchitectureGraph architecture, QuantumCircuit circuit);

        /// <summary>
        /// See <see cref="Algorithm.Name"/>.
        /// </summary>
        public abstract string Name();

        /// <summary>
        /// See<see cref="Algorithm.Parameters"/>.
        /// </summary>
        public abstract string Parameters();


        /// <summary>
        /// Get a random mapping with the given number of nodes. 
        /// </summary>
        /// <param name="NbNodes"> The number of nodes to map. </param>
        /// <returns>
        /// A mapping in which each of the qubit ID's are mapped on a random ID. 
        /// </returns>
        public static Mapping GetRandomMapping(int NbNodes)
        {
            return new Mapping(Enumerable.Range(0, NbNodes).OrderBy(i => Globals.Random.Next()).ToArray());
        }

        /// <summary>
        /// Returns a new swap perturbation for a mapping with the 
        /// given number of qubits.
        /// </summary>
        /// <param name="nbQubits"> The number of qubits which can be swapped </param>
        /// <returns>
        /// A random swap operation for the given mapping.
        /// </returns>
        protected static Swap GetSwapPerturbation(int nbQubits)
        {
            int swapQubit1, swapQubit2;
            swapQubit1 = Globals.Random.Next(nbQubits);
            do swapQubit2 = Globals.Random.Next(nbQubits); while (swapQubit1 == swapQubit2);
            return new Swap(swapQubit1, swapQubit2);
        }



        /*
        private List<CNOT>[] ControlGates;
        private List<CNOT>[] TargetGates;

        protected void SetGates(ArchitectureGraph architecture, QuantumCircuit circuit)
        {
            ControlGates = new List<CNOT>[architecture.NbNodes];
            TargetGates = new List<CNOT>[architecture.NbNodes];
            for (int i = 0; i < architecture.NbNodes; i++)
            {
                ControlGates[i] = new List<CNOT>();
                TargetGates[i] = new List<CNOT>();
            }


            foreach (CNOT gate in circuit.Gates)
            {
                ControlGates[gate.ControlQubit].Add(gate);
                TargetGates[gate.TargetQubit].Add(gate);
            }
        }
        */

        /*
        protected double CalculateCostDifference(double prevCost, int swap1, int swap2, ArchitectureGraph architecture, QuantumCircuit circuit)
        {
            double cost = prevCost;


            foreach(CNOT gate in circuit.Gates)
            {
                if (gate.ControlQubit == swap1 && gate.TargetQubit == swap2)
                    cost += architecture.CNOTDistance[swap2, swap1]
                        - architecture.CNOTDistance[swap1, swap2];
                else if (gate.ControlQubit == swap2 && gate.TargetQubit == swap1)
                    cost += architecture.CNOTDistance[swap1, swap2]
                        - architecture.CNOTDistance[swap2, swap1];
                else if (gate.ControlQubit == swap1)
                    cost += architecture.CNOTDistance[swap1, gate.TargetQubit]
                        - architecture.CNOTDistance[swap2, gate.TargetQubit];
                else if (gate.TargetQubit == swap1)
                    cost += architecture.CNOTDistance[gate.ControlQubit, swap2]
                        - architecture.CNOTDistance[gate.ControlQubit, swap1];
                else if (gate.ControlQubit == swap2)
                    cost += architecture.CNOTDistance[swap1, gate.TargetQubit]
                        - architecture.CNOTDistance[swap2, gate.TargetQubit];
                else if (gate.TargetQubit == swap2)
                    cost += architecture.CNOTDistance[gate.ControlQubit, swap1]
                        - architecture.CNOTDistance[gate.ControlQubit, swap2];
            }
            
            foreach(CNOT gate in ControlGates[swap1])
            {
                cost -= architecture.CNOTDistance[swap1, gate.TargetQubit];
                cost += architecture.CNOTDistance[swap2, gate.TargetQubit];
            }

            foreach (CNOT gate in TargetGates[swap1])
            {
                cost -= architecture.CNOTDistance[gate.ControlQubit, swap1];
                cost += architecture.CNOTDistance[gate.ControlQubit, swap2];
            }

            foreach (CNOT gate in ControlGates[swap2])
            {
                cost -= architecture.CNOTDistance[swap2, gate.TargetQubit];
                cost += architecture.CNOTDistance[swap1, gate.TargetQubit];
            }

            foreach (CNOT gate in TargetGates[swap2])
            {
                cost -= architecture.CNOTDistance[gate.ControlQubit, swap2];
                cost += architecture.CNOTDistance[gate.ControlQubit, swap1];
            }

            return cost;
        }
        */







        private const int MAX_NB_GATES_IN_COST = 10000;


        public static double GetMappingCost(Mapping mapping, ArchitectureGraph architecture, QuantumCircuit circuit)
        {

            int NbGatesAllowed = Math.Min(circuit.NbGates, MAX_NB_GATES_IN_COST);
            double cost = 0;
            double weight;
            // p1 could mayb be better, p2 is just so there can be divided by 0
            double p1 = circuit.NbGates * circuit.NbLayers / NbGatesAllowed;
            double p2 = 0.1;

            //double param = 1;
            //weight = (-param / circuit.NbGates) * i + param;
            //cost += (weight*architecture.CNOTDistance[mapping[circuit.Gates[i].ControlQubit], mapping[circuit.Gates[i].TargetQubit]]);

            int NbOfGatesLookedAt = 0;
            int CurrentLayer = 0;
            while (NbOfGatesLookedAt < NbGatesAllowed)
            {
                weight = 1 / (CurrentLayer / p1 + p2);
                foreach (CNOT gate in circuit.Layers[CurrentLayer])
                {
                    cost += architecture.CNOTDistance[mapping.Map[gate.ControlQubit], mapping.Map[gate.TargetQubit]];
                }
                NbOfGatesLookedAt += circuit.LayerSize[CurrentLayer++];
            }

            return cost;
        }






        //public static double GetMappingCost(double originalCost, int swap1, int swap2, ArchitectureGraph architecture, QuantumCircuit circuit)
        //{
        //    double cost = originalCost;
        //    double weight;
        //    double param = 1;

        //    for (int i = 0; i < circuit.NbGates; i++)
        //    {
        //        weight = (-param / circuit.NbGates) * i + param;
        //        CNOT cnot = circuit.Gates[i];
        //        if (cnot.ControlQubit == swap1)
        //        {
        //            cost -= weight * architecture.CNOTDistance[cnot.ControlQubit, cnot.TargetQubit];
        //            cost += weight * architecture.CNOTDistance[swap1, cnot.TargetQubit];
        //        } else if (cnot.TargetQubit == swap1)
        //        {
        //            cost -= weight * architecture.CNOTDistance[cnot.ControlQubit, cnot.TargetQubit];
        //            cost += weight * architecture.CNOTDistance[cnot.ControlQubit, swap1];
        //        }

        //        if (cnot.ControlQubit == swap1)
        //        {
        //            cost -= weight * architecture.CNOTDistance[cnot.ControlQubit, cnot.TargetQubit];
        //            cost += weight * architecture.CNOTDistance[swap2, cnot.TargetQubit];
        //        }
        //        else if (cnot.TargetQubit == swap2)
        //        {
        //            cost -= weight * architecture.CNOTDistance[cnot.ControlQubit, cnot.TargetQubit];
        //            cost += weight * architecture.CNOTDistance[cnot.ControlQubit, swap2];
        //        }
        //    }

        //    return cost; 
        //}

    }
}
