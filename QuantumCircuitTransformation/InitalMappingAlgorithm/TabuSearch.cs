using System;
using System.Collections.Generic;
using System.Text;
using QuantumCircuitTransformation.QuantumCircuitComponents;
using QuantumCircuitTransformation.QuantumCircuitComponents.Architecture;

namespace QuantumCircuitTransformation.InitalMappingAlgorithm
{
    public class TabuSearch : InitialMapping
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
        /// See <see cref="InitialMapping.Equals(InitialMapping)"/>
        /// </summary>
        /// <returns>
        /// True if the initial mapping equals according to <see cref="InitialMapping.Equals(InitialMapping)"/>
        /// and if the parameters are equal of the given tabu search and 
        /// this tabu search. 
        /// </returns>
        public override bool Equals(InitialMapping other)
        {
            if (base.Equals(other))
            {
                TabuSearch o = (TabuSearch)other;
                return NbNeighbours == o.NbNeighbours &&
                       NbTabus == o.NbTabus &&
                       MaxNbIterations == o.MaxNbIterations;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// See <see cref="InitialMapping.GetName"/>.
        /// </summary>
        public override string GetName()
        {
            return "Tabu Search";
        }

        /// <summary>
        /// See <see cref="InitialMapping.GetParameters"/>.
        /// </summary>
        public override string GetParameters()
        {
            return
                " > The number of neighbours: " + NbNeighbours + '\n' +
                " > The number of tabus: " + NbTabus + '\n' +
                " > The maximal number of iterations: " + MaxNbIterations;
        }





        /// <summary>
        /// Execute tabu search to find a mapping for the given circuit, which
        /// fits on the given architecture. 
        /// (See <see cref="InitialMapping.Execute(ArchitectureGraph, QuantumCircuit)"/>)
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
                int[] bestNeighbour = GetBestNeighbour(bestMapping, bestCost, architecture, circuit);
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

            for (int i = 0; i < NbNeighbours; i++) ;
                //neighbours.Add(PerturbatMapping(mapping));

            return neighbours;
        }


        private int[] GetBestNeighbour(int[] mapping, double cost, ArchitectureGraph architecture, QuantumCircuit circuit)
        {
            (int[] bestMapping, _, _) = PerturbatMapping(mapping);
            double bestCost = GetMappingCost(bestMapping, architecture, circuit);
            int[] newMapping;
            for (int i = 1; i < NbNeighbours; i++)
            {
                (newMapping, _, _) = PerturbatMapping(mapping);
                double newCost = GetMappingCost(newMapping, architecture, circuit);
                if (newCost < bestCost)
                {
                    bestMapping = newMapping;
                    bestCost = newCost;
                }
            }

            return bestMapping;
        }








        
    }
}
