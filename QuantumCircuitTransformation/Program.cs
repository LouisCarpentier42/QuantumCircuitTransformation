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
                new Tuple<int, int>(0,2),
            };


            (int a, string b) = Test();


            Console.WriteLine(a);
            Console.WriteLine(b);

            int[,] Length = ShortestPathFinder.Dijkstra(NbNodes, Edges);

            //for (int i = 0; i < NbNodes; i++)
            //{
            //    Console.Write(Length[i, 0]);
            //    for (int j = 1; j < NbNodes; j++)
            //    { 
            //        Console.Write(" - " + Length[i, j]);
            //    }
            //    Console.WriteLine();
            //}
            


            Console.ReadLine();
        } 




        private static (int, string) Test()
        {
            return (5, "test");
        }
    }



    
}



