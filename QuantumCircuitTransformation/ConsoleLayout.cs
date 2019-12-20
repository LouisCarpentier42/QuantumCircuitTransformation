using System;
using System.Collections.Generic;
using System.Text;

namespace QuantumCircuitTransformation
{
    public static class ConsoleLayout
    {
        private const int HEADERSIZE = 110;
        private const int SUBHEADERSIZE = 80;

        public static void Header(string title)
        {
            Console.Clear();
            Console.WriteLine('╔' + new string('═', HEADERSIZE-2) + '╗');
            Console.WriteLine('║' + new string(' ',(HEADERSIZE-2-title.Length)/2) + title.ToUpper() + new string(' ', HEADERSIZE-2 - title.Length - (HEADERSIZE-2 - title.Length) / 2) + '║');
            Console.WriteLine('╚' + new string('═', HEADERSIZE-2) + '╝' + '\n');
        }

        public static void SubHeader(string subTitle)
        {
            Console.WriteLine();
            string title = "";
            foreach (string s in subTitle.Split())
                title += s.Substring(0, 1).ToUpper() + s.Substring(1).ToLower() + ' ';
            Console.WriteLine('┌' + new string('─', SUBHEADERSIZE - 2) + '┐');
            Console.WriteLine('│' + new string(' ', (SUBHEADERSIZE - 2 - title.Length) / 2) + title + new string(' ', SUBHEADERSIZE - 2 - title.Length - (SUBHEADERSIZE - 2 - title.Length) / 2) + '│');
            Console.WriteLine('└' + new string('─', SUBHEADERSIZE - 2) + '┘');
        }

        public static void Footer()
        {
            Console.WriteLine();
            Console.WriteLine(new string('═',HEADERSIZE));
            Console.Write("PRESS ENTER TO CONTINUE ...");
            Console.ReadLine();
        }

        public static int SelectFromMenu(List<Tuple<string, Action>> menu)
        {
            for (int i = 0; i < menu.Count; i++)
                Console.WriteLine("{0}: {1}", i+1, menu[i].Item1);
            Console.WriteLine("0: Return");

            Console.Write("\nChoice: ");
            try
            {
                int index = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();
                return index;
            }
            catch (FormatException)
            {
                Console.WriteLine();
                return -1;
            }
        }

        public static void Error(string message = "An error has occured!")
        {
            Console.WriteLine(message);
        }
    }
}
