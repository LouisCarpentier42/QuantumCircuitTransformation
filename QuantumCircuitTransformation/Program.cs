using System;
using System.Collections.Generic;
using QuantumCircuitTransformation.QuantumCircuitComponents.Architecture;
using System.Linq;

namespace QuantumCircuitTransformation
{
    class Program
    {
        static void Main(string[] args)
        {


            int NbNodes = 4;
            List<Tuple<int, int>> Edges = new List<Tuple<int, int>>
            {
                new Tuple<int, int>(0,1),
                new Tuple<int, int>(1,2),
                new Tuple<int, int>(2,3),
                new Tuple<int, int>(3,0),
                new Tuple<int, int>(0,5),
                new Tuple<int,int>(95,100)
            };



            Console.ReadLine();
        } 

    }



    
}



