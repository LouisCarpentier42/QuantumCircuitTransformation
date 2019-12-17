using QuantumCircuitTransformation.DependencyGraphs;
using QuantumCircuitTransformation.QuantumCircuitComponents.Gates;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuantumCircuitTransformation.QuantumCircuitComponents.Circuit
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    ///     @author:   Louis Carpentier
    ///     @version:  2.0
    /// </remarks>
    public class LogicalCircuit : QuantumCircuit
    {

        public readonly DependencyGraph DependencyGraph;


        public LogicalCircuit(List<PhysicalGate> gates) : base(gates)
        {
            DependencyGraph = DependencyGraphGenerator.Generate(gates);
            throw new NotImplementedException("use rules");
        }


    }
}
