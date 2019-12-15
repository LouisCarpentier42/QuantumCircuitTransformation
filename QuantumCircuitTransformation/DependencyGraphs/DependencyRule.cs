using QuantumCircuitTransformation.QuantumCircuitComponents.Gates;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuantumCircuitTransformation.DependencyGraphs
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    ///     @author:   Louis Carpentier
    ///     @version:  1.1
    /// </remarks>
    public class DependencyRule
    {


        public bool MayBeSwitched(PhysicalGate gate1, PhysicalGate gate2)
        {
            throw new NotImplementedException();
        }

        private List<GatePart> GetOverlappingGateParts(PhysicalGate gate1, PhysicalGate gate2)
        {
            List<int> overlappingQubits = gate1.GetQubits().Intersect(gate2.GetQubits()).ToList();
            throw new NotImplementedException();
        }


    }
}