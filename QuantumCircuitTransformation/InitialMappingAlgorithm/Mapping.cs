using System;
using System.Collections.Generic;
using System.Text;

namespace QuantumCircuitTransformation.InitialMappingAlgorithm
{
    /// <summary>
    /// 
    /// @author:   Louis Carpenier
    /// @version:  1.1
    /// 
    /// </summary>
    public class Mapping
    {
        private readonly int[] Map;


        public Mapping(int[] map)
        {
            Map = map;
        }


        public int Get(int i)
        {
            return Map[i];
        }

        public override string ToString()
        {
            string result = "[" + Map[0];
            for (int i = 1; i < Map.Length; i++)
                result += ", " + Map[i];
            return result + "]";
        }
    }
}