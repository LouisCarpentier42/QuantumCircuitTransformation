using System;
using System.Collections.Generic;
using System.Text;

namespace QuantumCircuitTransformation.InitalMappingAlgorithm
{
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
    }
}
