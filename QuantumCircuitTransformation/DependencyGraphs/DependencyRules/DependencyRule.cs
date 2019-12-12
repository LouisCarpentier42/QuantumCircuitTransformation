using QuantumCircuitTransformation.QuantumCircuitComponents.Gates;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuantumCircuitTransformation.DependencyGraphs.DependencyRules
{

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    ///     @author:   Louis Carpentier
    ///     @version:  1.0
    /// </remarks>
    public abstract class DependencyRule
    {

        public bool MustBeExecutedBefore(PhysicalGate gate1, PhysicalGate gate2)
        {
            try
            {
                return DependsOn(gate1, gate2);
            }
            catch (InvalidCastException)
            {
                return false;
            }
        }


        protected abstract bool DependsOn(PhysicalGate gate1, PhysicalGate gate2);
    }
}
