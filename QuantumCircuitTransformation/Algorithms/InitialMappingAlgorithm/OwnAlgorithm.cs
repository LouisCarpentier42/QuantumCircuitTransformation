using QuantumCircuitTransformation.Data;
using QuantumCircuitTransformation.Exceptions;
using QuantumCircuitTransformation.MappingPerturbation;
using QuantumCircuitTransformation.QuantumCircuitComponents.ArchitectureGraph;
using QuantumCircuitTransformation.QuantumCircuitComponents.Circuit;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuantumCircuitTransformation.Algorithms.InitialMappingAlgorithm
{
    /// <summary>
    ///     OwnAlgorithm
    ///         An algorithm to find an initial mapping to solve the
    ///         quantum circuit transformation problem. 
    ///         This algorithm is a combination of different local 
    ///         search algortihms. It uses a tabu list to send the 
    ///         search in a certain direction, where no solutions 
    ///         have been tested yet using a tabu list. It also uses
    ///         late acceptance to be able to do downhill moves so it 
    ///         can avoid local optima. To be sure that as much as 
    ///         possible of the searchspace has been checked, it 
    ///         uses a diversification mechanism. 
    /// </summary>
    /// <remarks>
    ///     @author:   Louis Carpentier
    ///     @version:  2.0
    /// </remarks>
    public class OwnAlgorithm : InitialMapping
    {
        /// <summary>
        /// Variable referring the late acceptance time. 
        /// </summary>
        public readonly int LateAcceptanceTime;
        /// <summary>
        /// Variable referring to the number of tabu moves. 
        /// </summary>
        public readonly int NbTabus;
        /// <summary>
        /// Variable referring to the maximal number of moves. 
        /// </summary>
        public readonly int MaxNbIterations;
        /// <summary>
        /// Variable referring the the number of times there needs 
        /// to be a diversification done. 
        /// </summary>
        public readonly int DiversificationRate;


        /// <summary>
        /// Initialise a new algorithm with given parameters. 
        /// </summary>
        /// <param name="lateAcceptanceTime"> The late acceptance time for this algorithm. </param>
        /// <param name="nbTabus"> The number of tabu moves for this algorithm. </param>
        /// <param name="maxNbIterations"> The maximum number of iterations. </param>
        /// <param name="diversificationRate"> The rate in which a diversification move must be done. </param>
        public OwnAlgorithm(int lateAcceptanceTime, int nbTabus, int maxNbIterations, int diversificationRate)
        {
            if (IsValidLateAcceptanceTime(lateAcceptanceTime))
                LateAcceptanceTime = lateAcceptanceTime;
            else throw new InvalidParameterException();

            if (IsValidNbTabus(nbTabus))
                NbTabus = nbTabus;
            else throw new InvalidParameterException();

            if (IsValidMaxNbIterations(maxNbIterations))
                MaxNbIterations = maxNbIterations;
            else throw new InvalidParameterException();

            if (IsValidDiversificationRate(diversificationRate))
                DiversificationRate = diversificationRate;
            else throw new InvalidParameterException();
        }

        /// <summary>
        /// Checks if the given late acceptance time is valid.
        /// </summary>
        /// <param name="lateAcceptanceTime"> The late acceptance time to check. </param>
        /// <returns>
        /// True if and only if the given late acceptance time is valid. 
        /// </returns>
        public static bool IsValidLateAcceptanceTime(int lateAcceptanceTime)
        {
            return lateAcceptanceTime > 0;
        }

        /// <summary>
        /// Checks if the given number of tabus is valid.
        /// </summary>
        /// <param name="nbTabus"> The number of tabus to check. </param>
        /// <returns>
        /// True if and only if the given number of tabus is greater then 0.
        /// </returns>
        public static bool IsValidNbTabus(int nbTabus)
        {
            return nbTabus > 0;
        }

        /// <summary>
        /// Checks if the given maximum number of iterations is valid.
        /// </summary>
        /// <param name="maxNbIterations"> The maximum numbers of iterations to check. </param>
        /// <returns>
        /// True if and only if the given maximum number of iterations is greater then 0.
        /// </returns>
        public static bool IsValidMaxNbIterations(int maxNbIterations)
        {
            return maxNbIterations > 0;
        }

        /// <summary>
        /// Checks if the given diversification rate is valid.
        /// </summary>
        /// <param name="diversificationRate"> The diversification rate to check. </param>
        /// <returns>
        /// True if and only if the given diversification rate is greater then 0.
        /// </returns>
        public static bool IsValidDiversificationRate(int diversificationRate)
        {
            return diversificationRate > 0;
        }
        
        /// <summary>
        /// See <see cref="Algorithm.Equals(object)"/>.
        /// </summary>
        public override bool Equals(object other)
        {
            if (other == null) return false;
            try
            {
                OwnAlgorithm o = (OwnAlgorithm)other;
                return LateAcceptanceTime == o.LateAcceptanceTime &&
                       NbTabus == o.NbTabus &&
                       MaxNbIterations == o.MaxNbIterations &&
                       DiversificationRate == o.DiversificationRate;
            }
            catch (InvalidCastException)
            {
                return false;
            }
        }

        /// <summary>
        /// See <see cref="Algorithm.GetHashCode"/>.
        /// </summary>
        public override int GetHashCode()
        {
            return
                (int)(Math.Pow(2, LateAcceptanceTime)) *
                (int)(Math.Pow(3, NbTabus)) *
                (int)(Math.Pow(5, MaxNbIterations)) *
                (int)(Math.Pow(7, DiversificationRate));
        }

        /// <summary>
        /// See <see cref="Algorithm.Name"/>.
        /// </summary>
        public override string Name()
        {
            return "Own algorithm";
        }

        /// <summary>
        /// See <see cref="Algorithm.Parameters"/>.
        /// </summary>
        public override string Parameters()
        {
            return
                " > The late acceptance size: " + LateAcceptanceTime + '\n' +
                " > The number of tabus: " + NbTabus + '\n' +
                " > The maximal number of iterations: " + MaxNbIterations + '\n' + 
                " > The diversification rate: " + DiversificationRate;
        }

        /// <summary>
        /// See <see cref="InitialMapping.GetFullShort"/>. 
        /// </summary>
        public override string GetFullShort()
        {
            return "OA(" + LateAcceptanceTime + ", " + NbTabus + ", " + MaxNbIterations + ", " + DiversificationRate + ")";
        }


        /// <summary>
        /// Variable referring to the late acceptance costs of solutions which
        /// are found during the execution of the algorithm. 
        /// </summary>
        private double[] LateAcceptanceList;
        /// <summary>
        /// Variable referring to the tabulist. 
        /// </summary>
        private Queue<Perturbation> TabuList;
        /// <summary>
        /// Variable referring to the adjusted number of tabus. This is the adjusted to 
        /// the given circuit to solve. If this isn't done, the tabu list could contain 
        /// all valid moves.
        /// </summary>
        private int AdjustedNbTabus;
        /// <summary>
        /// Variable referring to the best mapping found during the execution of the 
        /// algorithm. 
        /// </summary>
        private Mapping BestMapping;
        /// <summary>
        /// Variable referring to the cost of <see cref="BestMapping"/>.
        /// </summary>
        private double BestCost;
        /// <summary>
        /// Variable referring to the mapping currently being looked at during the 
        /// execution of the algorithm. 
        /// </summary>
        private Mapping CurrentMapping;
        /// <summary>
        /// Variable referring to the cost of <see cref="CurrentMapping"/>.
        /// </summary>
        private double CurrentCost;

        /// <summary>
        /// Execute this algorithm to find a mapping for the given circuit, which
        /// fits on the given architecture. 
        /// (See <see cref="InitialMapping.Execute(Architecture, LogicalCircuit)"/>)
        /// </summary>
        public override (Mapping, double) Execute(Architecture architecture, LogicalCircuit circuit)
        {
            SetUp(architecture, circuit);
            Perturbation perturbation;
            Mapping newMapping;
            for (int iteration = 0; iteration < MaxNbIterations; iteration++)
            {
                if (iteration % DiversificationRate == DiversificationRate - 1)
                    perturbation  = GetCyclePerturbation(CurrentMapping.NbQubits);
                else
                    perturbation = GetSwapPerturbation(CurrentMapping.NbQubits);

                newMapping = CurrentMapping.Clone();
                perturbation.Apply(newMapping);
                double newCost = GetMappingCost(newMapping, architecture, circuit);

                int LateAcceptanceID = iteration % LateAcceptanceTime;

                if (newCost < BestCost)
                {
                    BestMapping = newMapping;
                    BestCost = newCost;
                }
                if (newCost < CurrentCost || newCost <= LateAcceptanceList[LateAcceptanceID])
                {
                    CurrentMapping = newMapping;
                    CurrentCost = newCost;
                }
                LateAcceptanceList[LateAcceptanceID] = CurrentCost;
                RemoveAgedTabus();
                //Console.WriteLine("Best: {0} - Cost: {1} - newCost: {2}", BestCost, CurrentCost, newCost);
            }
            //Console.WriteLine("Best: {0}", bestCost);
            return (BestMapping, BestCost);
        }

        /// <summary>
        /// Setup all the parameters for the execution of the algorithm. 
        /// </summary>
        /// <param name="architecture"> The architecture this algorithm will be applied to. </param>
        /// <param name="circuit"> The circuit this algorithm will be applied to. </param>
        private void SetUp(Architecture architecture, LogicalCircuit circuit)
        {
            LateAcceptanceList = new double[LateAcceptanceTime];
            for (int i = 0; i < LateAcceptanceTime; i++)
                LateAcceptanceList[i] = CurrentCost;

            TabuList = new Queue<Perturbation>();
            int maxNbTabus = NbTabus; // There are at max NbQubits!/2 moves possible. 
            if (circuit.NbQubits == 3) maxNbTabus = 1;
            else if (circuit.NbQubits == 4) maxNbTabus = 6;
            else if (circuit.NbQubits == 5) maxNbTabus = 30;
            AdjustedNbTabus = Math.Min(NbTabus, maxNbTabus);

            BestMapping = GetRandomMapping(architecture.NbNodes);
            BestCost = GetMappingCost(BestMapping, architecture, circuit);

            CurrentMapping = BestMapping.Clone();
            CurrentCost = BestCost;
        }

        /// <summary>
        /// Removes the tabu moves from the tabu list which are 
        /// the oldest, untill the tabu list has a proper length. 
        /// </summary>
        private void RemoveAgedTabus()
        {
            while (TabuList.Count > AdjustedNbTabus) TabuList.Dequeue();
        }

        /// <summary>
        /// Returns a new swap perturbation for the given mapping with the
        /// given number of qubits, which is not in the tabu list. 
        /// </summary>
        /// <param name="mapping"> The number of qubits of the mapping for the swap. </param>
        /// <returns>
        /// A random swap operation for the given number of qubits which is not in 
        /// the tabu list. 
        /// </returns>
        private Swap GetNoneTabuSwapPerturbation(int nbQubits)
        {
            Swap swap;
            do swap = GetSwapPerturbation(nbQubits); while (TabuList.Contains(swap));
            return swap;
        }

        /// <summary>
        /// Returns a new cycle perturbation for a mapping with the
        /// given number of qubits.
        /// </summary>
        /// <param name="nbQubits"> The number of qubits of the mapping for the cycle perturbation. </param>
        /// <returns>
        /// A random cycle perturbation for a mapping with the given number 
        /// of qubits. 
        /// </returns>
        private Cycle GetCyclePerturbation(int nbQubits)
        {
            int[] permutation = Enumerable.Range(0, nbQubits).OrderBy(x => Globals.Random.Next()).ToArray();
            return new Cycle(permutation);
        }
    }
}