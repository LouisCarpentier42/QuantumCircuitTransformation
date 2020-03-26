using QuantumCircuitTransformation.QuantumCircuitComponents.Gates;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuantumCircuitTransformation.Algorithms.TransformationAlgorithm
{
    /// <summary>
    ///     NaiveTransformation:
    ///         A transformation algorithm which naively adds swap gates to make each
    ///         cnot gate in the circuit possible to execute.  
    /// </summary>
    /// <remarks>
    ///     @author:   Louis Carpentier
    ///     @version:  1.0
    /// </remarks>
    public class NaiveTransformation : Transformation
    {
        /// <summary>
        /// See <see cref="Algorithm.Name"/>.
        /// </summary>
        public override string Name()
        {
            return "Naive transformation";
        }

        /// <summary>
        /// See <see cref="Algorithm.Parameters"/>.
        /// </summary>
        public override string Parameters()
        {
            return "";
        }

        /// <summary>
        /// Execute this naive transformation algorithm. 
        /// </summary>
        protected override void Execute()
        {
            // TODO naive transformation
            foreach (Gate g in LogicalCircuit.Gates)
            {
                if (!g.CanBeExecutedOn(Architecture, Mapping))
                {
                    List<int> path = Architecture.GetShortestPath(g.GetQubits()[0], g.GetQubits()[1]); // Is always a cnot gate normally
                    for (int i = path.Count - 1; i >= 1; i--)
                    {
                        AddSwapToCircuit(Mapping.Map[path[i]], Mapping.Map[path[i - 1]]);
                    }
                }
                PhysicalCircuit.AddGate(g.Map(Mapping));
            }
        }
    }
}
