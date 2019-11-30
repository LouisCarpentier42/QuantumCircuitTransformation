using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuantumCircuitTransformation.Exceptions;
using QuantumCircuitTransformation.Data;
using QuantumCircuitTransformation.QuantumCircuitComponents;
using QuantumCircuitTransformation.QuantumCircuitComponents.Architecture;

namespace QuantumCircuitTransformation.InitialMappingAlgorithm
{
    /// <summary>
    /// TODO
    /// </summary>
    /// <remarks>
    ///     @author:   Louis Carpentier
    ///     @version:  2.0
    /// </remarks>
    public class LAHC : InitialMapping
    {
        /// <summary>
        /// Variable referring the late acceptance time. 
        /// </summary>
        public readonly int LateAcceptanceSize;
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


        public LAHC(int lateAcceptanceSize, int nbTabus, int maxNbIterations, int diversificationRate)
        {
            if (IsValidLateAcceptanceSize(lateAcceptanceSize))
                LateAcceptanceSize = lateAcceptanceSize;
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

        public static bool IsValidLateAcceptanceSize(int lateAcceptanceSize)
        {
            return lateAcceptanceSize > 0;
        }

        public static bool IsValidNbTabus(int nbTabus)
        {
            return nbTabus > 0;
        }

        public static bool IsValidMaxNbIterations(int maxNbIterations)
        {
            return maxNbIterations > 0;
        }
        

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
                LAHC o = (LAHC)other;
                return LateAcceptanceSize == o.LateAcceptanceSize &&
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
                (int)(Math.Pow(2, LateAcceptanceSize)) *
                (int)(Math.Pow(3, NbTabus)) *
                (int)(Math.Pow(5, MaxNbIterations)) *
                (int)(Math.Pow(7, DiversificationRate));
        }

        /// <summary>
        /// See <see cref="Algorithm.Name"/>.
        /// </summary>
        public override string Name()
        {
            return "LAHC";
        }

        /// <summary>
        /// See <see cref="Algorithm.Parameters"/>.
        /// </summary>
        public override string Parameters()
        {
            return
                " > The late acceptance size: " + LateAcceptanceSize + '\n' +
                " > The number of tabus: " + NbTabus + '\n' +
                " > The maximal number of iterations: " + MaxNbIterations;
        }








        private double[] LateAcceptanceList;

        private Queue<int[]> TabuList;

        private int[] BestMapping;

        private double BestCost;

        private int[] CurrentMapping;

        private double CurrentCost;


        private void SetUp(ArchitectureGraph architecture, QuantumCircuit circuit)
        {
            BestMapping = GetRandomMapping(architecture.NbNodes);
            BestCost = GetMappingCost(BestMapping, architecture, circuit);

            CurrentMapping = new int[BestMapping.Length];
            Array.Copy(BestMapping, CurrentMapping, BestMapping.Length);
            CurrentCost = BestCost;

            LateAcceptanceList = new double[LateAcceptanceSize];
            for (int i = 0; i < LateAcceptanceSize; i++)
                LateAcceptanceList[i] = CurrentCost;

            TabuList = new Queue<int[]>();
        }

        


        public override (Mapping, double) Execute(ArchitectureGraph architecture, QuantumCircuit circuit)
        {
            SetUp(architecture, circuit);
            int[] newMapping;
            for (int iteration = 0; iteration < MaxNbIterations; iteration++)
            {
                if (iteration % 500 == 499)
                    newMapping = Diversificate(CurrentMapping);
                else
                    newMapping = PerturbatMapping(CurrentMapping);
                double newCost = GetMappingCost(newMapping, architecture, circuit);
                int LateAcceptanceID = iteration % LateAcceptanceSize;

                if (!TabuList.Contains(newMapping))
                {
                    TabuList.Enqueue(newMapping);
                    if (newCost < BestCost)
                    {
                        Array.Copy(newMapping, BestMapping, architecture.NbNodes);
                        BestCost = newCost;
                    }
                    if (newCost < CurrentCost || newCost <= LateAcceptanceList[LateAcceptanceID])
                    {
                        Array.Copy(newMapping, CurrentMapping, architecture.NbNodes);
                        CurrentCost = newCost;
                    }
                    LateAcceptanceList[LateAcceptanceID] = CurrentCost;
                }
                while (TabuList.Count > NbTabus) TabuList.Dequeue();

                //Console.WriteLine("Best: {0} - Cost: {1} - newCost: {2} - fitness = {3}", bestCost, cost, newCost, fitnessArray[v]);
            }
            //Console.WriteLine("Best: {0}", bestCost);
            return (new Mapping(BestMapping), BestCost);
        }

        private int[] Intensificate(int[] mapping)
        {
            throw new NotImplementedException();
        }

        private int[] Diversificate(int[] mapping)
        {
            int[] permutation = Enumerable.Range(0, mapping.Length).OrderBy(x => Globals.Random.Next()).ToArray();

            int[] diversification = new int[mapping.Length];
            Array.Copy(mapping, diversification, mapping.Length);
            for (int i = 0; i < permutation.Length - 1; i++)
                diversification[permutation[i + 1]] = mapping[permutation[i]];
            diversification[permutation[0]] = mapping[permutation[permutation.Length - 1]];

            return diversification;
        }



    }
}
