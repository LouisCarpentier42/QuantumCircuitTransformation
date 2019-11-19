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


            List<Tuple<int, int>> e = new List<Tuple<int, int>>
            {
                new Tuple<int, int>(0,1),
                new Tuple<int, int>(1,2),
                new Tuple<int, int>(3,2),
                new Tuple<int, int>(0,3),
                new Tuple<int, int>(1,3),
            };



            int NbNodes = 16;
            ArchitectureGraph a = QuantumDevices.IBM_QX5;
           
            
            string Result = ""; 
            for (int i = 0; i < NbNodes; i++)
            {
                Result += a.GetCNOTDistance(i, 0).ToString();
                for (int j = 1; j < NbNodes; j++)
                    Result += " - " + a.GetCNOTDistance(i, j);
                Result += "\n";
            }
            Console.WriteLine(Result);
            



            Console.ReadLine();
        } 

    }



    
}



