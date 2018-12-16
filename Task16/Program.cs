using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task16
{
    class Program
    {
        static void Main(string[] args)
        {
            System.IO.StreamReader file = new System.IO.StreamReader("../../input.txt");
            string line;
            int N = File.ReadAllLines("../../input.txt").Length;
            int M = File.ReadAllLines("../../input.txt")[0].Length;

            int[,] Field = new int[N, M];

            int lineNumber = 0;
            while ((line = file.ReadLine()) != null)
            {
               

                lineNumber++;
            }
            file.Close();
        }
    }
}
