using System;
using System.Collections.Generic;
using System.Text;
using QuantumCircuitTransformation.QuantumCircuitComponents;
using QuantumCircuitTransformation.QuantumCircuitComponents.Architecture;

namespace QuantumCircuitTransformation.InitalMappingAlgorithm
{
    public class TabuSearch : InitalMapping
    {
        /// <summary>
        /// Variable referring to the number of neighbours to generate at
        /// each iteration. 
        /// </summary>
        public readonly int NbNeighbours;
        /// <summary>
        /// Variable referring to the number of tabus to keep track of. 
        /// </summary>
        public readonly int NbTabus;
        /// <summary>
        /// Variable referring to the number of iterations to execute. 
        /// </summary>
        public readonly int MaxNbIterations;

        /// <summary>
        /// Initialise a new tabu search algorithm with all it's parameters.
        /// </summary>
        /// <param name="nbNeighbours"> The variable for <see cref="NbNeighbours"/>. </param>
        /// <param name="nbTabus"> The variable for <see cref="NbTabus"/>. </param>
        /// <param name="maxNbIterations">  The variable for <see cref="MaxNbIterations"/>.</param>
        public TabuSearch(int nbNeighbours, int nbTabus, int maxNbIterations)
        {
            NbNeighbours = nbNeighbours;
            NbTabus = nbTabus;
            MaxNbIterations = maxNbIterations;
        }

        /// <summary>
        /// See <see cref="InitalMapping.ToString"/>.
        /// </summary>
        public override string ToString()
        {
            return
                "---TABU SEARCH---\n" +
                "> The number of neighbours: " + NbNeighbours +
                "> The number of tabus: " + NbTabus +
                "> The maximal number of iterations: " + MaxNbIterations;
        }

        /// <summary>
        /// Execute tabu search to find a mapping for the given circuit, which
        /// fits on the given architecture. 
        /// (See <see cref="InitalMapping.Execute(ArchitectureGraph, QuantumCircuit)"/>)
        /// </summary>
        public override Mapping Execute(ArchitectureGraph architecture, QuantumCircuit circuit)
        {
            int[] bestMapping = GetRandomMapping(architecture.NbNodes);
            double bestCost = GetMappingCost(bestMapping, architecture, circuit);
            

            Queue<int[]> tabus = new Queue<int[]>(NbTabus);
            for (int i = 0; i < NbTabus; i++)
                tabus.Enqueue(null);

            int NbIterations = 0;
            while (NbIterations++ <= MaxNbIterations)
            {

                List<int[]> neighbours = GetNeighbours(bestMapping);                
                int[] bestNeighbour = GetBestNeighbour(neighbours, architecture, circuit);
                double cost = GetMappingCost(bestNeighbour, architecture, circuit);
                //Console.WriteLine("Best: {0} - Cost: {1}", bestCost, cost);
                if (!tabus.Contains(bestNeighbour))
                {
                    tabus.Dequeue();
                    int[] newTabu = new int[bestMapping.Length];
                    Array.Copy(bestMapping, newTabu, bestMapping.Length);
                    tabus.Enqueue(newTabu);

                    if (cost < bestCost)
                    {
                        bestMapping = bestNeighbour;
                        bestCost = cost;
                    }

                }
            }
            Console.WriteLine("Best: {0}", bestCost);
            return new Mapping(bestMapping);
        }





        private List<int[]> GetNeighbours(int[] mapping)
        {
            List<int[]> neighbours = new List<int[]>();

            for (int i = 0; i < NbNeighbours; i++)
                neighbours.Add(PerturbatMapping(mapping));

            return neighbours;
        }


        private int[] GetBestNeighbour(List<int[]> neighbours, ArchitectureGraph architecture, QuantumCircuit circuit)
        {
            int[] bestMapping = neighbours[0];
            double bestCost = GetMappingCost(bestMapping, architecture, circuit);

            for (int i = 1; i < neighbours.Count; i++)
            {
                double newCost = GetMappingCost(neighbours[i], architecture, circuit);
                if (newCost < bestCost)
                {
                    bestMapping = neighbours[i];
                    bestCost = newCost;
                }
            }

            return bestMapping;
        }








        
    }
}
