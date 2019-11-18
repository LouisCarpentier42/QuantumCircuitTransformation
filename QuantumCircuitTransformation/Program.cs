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
            string Result = ""; 
            for (int i = 0; i < 20; i++)
            {
                Result += QuantumDevices.IBM_Q20.GetCNOTDistance(i, 0).ToString();
                for (int j = 1; j < 20; j++)
                    Result += " - " + QuantumDevices.IBM_Q20.GetCNOTDistance(i, j);
                Result += "\n";
            }
            Console.WriteLine(Result);



            Console.ReadLine();
        } 

    }



    
}



