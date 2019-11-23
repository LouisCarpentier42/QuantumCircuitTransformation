using QuantumCircuitTransformation.QuantumCircuitComponents;
using QuantumCircuitTransformation.QuantumCircuitComponents.Architecture;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuantumCircuitTransformation.InitalMappingAlgorithm
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
    /// @version:  1.0
    /// 
    /// </summary>
    public abstract class InitialMapping
    {
        /// <summary>
        /// Execute this inital mapping algorithm. 
        /// </summary>
        /// <param name="architecture"> The architecture to find a mapping for. </param>
        /// <param name="circuit"> The circuit containing the qubits to map. </param>
        /// <returns>
        /// The best <see cref="Mapping"/> which has been found by the algorithm. 
        /// </returns>
        public abstract Mapping Execute(ArchitectureGraph architecture, QuantumCircuit circuit);

        /// <summary>
        /// Get a random mapping with the given number of nodes. 
        /// </summary>
        /// <param name="NbNodes"> The number of nodes to map. </param>
        /// <returns>
        /// A mapping in which each of the qubit ID's are mapped on a random ID. 
        /// </returns>
        public static int[] GetRandomMapping(int NbNodes)
        {
            return Enumerable.Range(0, NbNodes).OrderBy(i => Guid.NewGuid()).ToArray();
        }

        /// <summary>
        /// Give the name of the initial mapping algorithm. 
        /// </summary>
        public abstract string GetName();

        /// <summary>
        /// Give an overview of the parameters of this initial mapping algorithm. 
        /// </summary>
        public abstract string GetParameters();




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


            
            /*
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
            */

            return cost;
        }



        protected (int[],int,int) PerturbatMapping(int[] mapping)
        {
            int[] perturbation = new int[mapping.Length];
            Array.Copy(mapping, perturbation, mapping.Length);

            int x = Globals.Random.Next(mapping.Length);
            int y;
            do y = Globals.Random.Next(mapping.Length); while (x == y);

            int temp = perturbation[x];
            perturbation[x] = perturbation[y];
            perturbation[y] = temp;
            return (perturbation,x,y);
        }


        public static double GetMappingCost(int[] mapping, ArchitectureGraph architecture, QuantumCircuit circuit)
        {
            double cost = 0;
            double weight;
            double param = 1;

            for (int i = 0; i < circuit.NbGates; i++)
            {
                weight = (-param / circuit.NbGates) * i + param;
                cost += (weight*architecture.CNOTDistance[mapping[circuit.Gates[i].ControlQubit], mapping[circuit.Gates[i].TargetQubit]]);
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
