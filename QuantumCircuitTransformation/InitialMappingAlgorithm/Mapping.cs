using QuantumCircuitTransformation.QuantumCircuitComponents;

namespace QuantumCircuitTransformation.InitialMappingAlgorithm
{
    /// <summary>
    /// 
    /// Mapping:
    ///     A class to keep track of a mapping. A mapping is a 
    ///     bijection from [0..n] to [0..n].
    /// 
    /// @author:   Louis Carpenier
    /// @version:  1.3
    /// 
    /// </summary>
    public class Mapping
    {
        /// <summary>
        /// Variable referring to a mapping. 
        /// </summary>
        public int[] Map { get; private set; }

        /// <summary>
        /// Initialise a new mapping with given mapping array. 
        /// </summary>
        /// <param name="map"> The mapping array for this mapping. </param>
        public Mapping(int[] map)
        {
            Map = map;
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
    }
}