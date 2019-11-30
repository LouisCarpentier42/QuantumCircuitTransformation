using System;
using System.Collections.Generic;
using System.Text;

namespace QuantumCircuitTransformation.TransformationAlgorithm
{
    public abstract class Transformation : Algorithm
    {
        public virtual bool Equals(Algorithm other)
        {
            if (other == null || GetType() != other.GetType())
                return false;
            else
                return true;
        }

        public abstract string Name();

        /// <summary>
        /// See <see cref="Algorithm.Parameters"/>.
        /// </summary>
        public abstract string Parameters();

    }
}
