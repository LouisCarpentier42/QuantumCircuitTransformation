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
                //new Tuple<int,int>(99,100)
            };


            HashSet<int> nodes = new HashSet<int>();
            foreach (Tuple<int, int> t in Edges)
            {
                nodes.Add(t.Item1);
                nodes.Add(t.Item2);
            }

            // (i,j) -> i moet vervangen worden door j
            Dictionary<int, int> replacements = new Dictionary<int, int>();
            for (int i = 0; i < nodes.Count(); i++)
            {
                if (!nodes.Contains(i))
                {
                    replacements.Add(i, nodes.Max() - replacements.Count());
                }
            }

            for (int i = 0; i < Edges.Count(); i++)
            {
                if (replacements.ContainsKey(Edges[i].Item1))
                {
                    Edges[i] = new Tuple<int, int>(replacements[i],Edges[i].Item2);
                }
                if (replacements.ContainsKey(Edges[i].Item2))
                {
                    Edges[i] = new Tuple<int, int>(Edges[i].Item1, replacements[i]);
                }
            }

            foreach (var t in Edges)
                Console.WriteLine(t);
            // Werkt niet



 


            Console.ReadLine();
        } 

    }



    
}



