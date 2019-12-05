using QuantumCircuitTransformation.QuantumCircuitComponents;
using QuantumCircuitTransformation.QuantumCircuitComponents.Gates;
using System;
using System.Linq;

namespace QuantumCircuitTransformation.MappingPerturbation 
{
    /// <summary>
    ///     Mapping:
    ///         A class to keep track of a mapping. A mapping is a 
    ///         bijection from [0..n] to [0..n] where n is the number
    ///         of elements to map.
    /// </summary>
    /// <remarks>
    ///     @author:   Louis Carpenier
    ///     @version:  1.4   
    ///  </remarks>
    public class Mapping : IEquatable<Mapping>
    {
        /// <summary>
        /// Variable referring to a mapping. 
        /// </summary>
        public int[] Map { get; private set; }
        /// <summary>
        /// Variable referring to the number of qubits of this mapping. 
        /// </summary>
        public readonly int NbQubits;

        
        /// <summary>
        /// Initialise a new mapping with given mapping array. 
        /// </summary>
        /// <param name="map"> The mapping array for this mapping. </param>
        public Mapping(int[] map)
        {
            Map = map;
            NbQubits = map.Count();
        }

        /// <summary>
        /// Swaps two elements in the mapping. 
        /// </summary>
        /// <param name="qubit1"> The first element in the swapping. </param>
        /// <param name="qubit2"> The second element in the swapping. </param>
        public void Swap(int qubit1, int qubit2)
        {
            int temp = Map[qubit1];
            Map[qubit1] = Map[qubit2];
            Map[qubit2] = temp; 
        }

        /// <summary>
        /// Returns a CNOT gate in which the control and target qubit are 
        /// mapped according to this mapping. 
        /// </summary>
        /// <param name="cnot"> The CNOT gate to map. </param>
        /// <returns>
        /// A new CNOT gate which is mapped according to this mapping. 
        /// </returns>
        public CNOT MapCNOT(CNOT cnot)
        {
            return new CNOT(Map[cnot.ControlQubit], Map[cnot.TargetQubit]);
        }

        /// <summary>
        /// Clones this mapping.
        /// </summary>
        /// <returns>
        /// A clone of this mapping with the same value. 
        /// </returns>
        public Mapping Clone()
        {
            int[] clonedMapArray = new int[Map.Length];
            Array.Copy(Map, clonedMapArray, Map.Length);
            return new Mapping(clonedMapArray);
        }

        /// <summary>
        /// Gives a string representation of this mapping. 
        /// </summary>
        /// <returns>
        /// A representation in which the mapping is represented as an array, 
        /// the element i is mapped onto the element at index i in the string
        /// representation. 
        /// </returns>
        public override string ToString()
        {
            string result = "[" + Map[0];
            for (int i = 1; i < Map.Length; i++)
                result += ", " + Map[i];
            return result + "]";
        }

        /// <summary>
        /// Checks if this mapping equals a given mapping. 
        /// </summary>
        /// <param name="other"> The mapping to compare. </param>
        /// <returns>
        /// True if and only if the mapping array and the number of qubits
        /// of this mapping are equal to those of the other mapping. 
        /// </returns>
        public bool Equals(Mapping other)
        {
            return Map.SequenceEqual(other.Map) &&
                   NbQubits == other.NbQubits;
        }
    }
}