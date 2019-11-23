using System;
using System.Collections.Generic;
using System.Text;

namespace QuantumCircuitTransformation
{
    public static class ConsoleLayout
    {
        private const int HEADERSIZE = 80;

        public static void Header(string title)
        {
            Console.Clear();
            Console.WriteLine('╔' + new string('═', HEADERSIZE-2) + '╗');
            Console.WriteLine('║' + new string(' ',(HEADERSIZE-2-title.Length)/2) + title.ToUpper() + new string(' ', HEADERSIZE-2 - title.Length - (HEADERSIZE-2 - title.Length) / 2) + '║');
            Console.WriteLine('╚' + new string('═', HEADERSIZE-2) + '╝' + '\n');
        }

        public static void Footer()
        {
            Console.WriteLine();
            Console.WriteLine(new string('═',HEADERSIZE));
            Console.Write("PRESS ENTER TO CONTINUE ...");
            Console.ReadLine();
        }

        public static void PrintMenu(List<Tuple<string, Action>> menu)
        {
            for (int i =0; i < menu.Count; i++)
                Console.WriteLine("{0}: {1}", i + 1, menu[i].Item1);
            Console.WriteLine("0: Return");
        }

        public static void InvalidInput()
        {
            Console.WriteLine("Invalid input!");
        }
    }
}
