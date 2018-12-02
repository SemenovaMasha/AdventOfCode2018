using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    class Program
    {
        static bool OnlyOneDiffLetters(string s1, string s2)
        {
            int diffNumber = 0;

            for (int i = 0; i < s1.Length; i++)
            {
                if (s1[i] != s2[i])
                {
                    if (diffNumber == 1) return false;
                    diffNumber++;
                }
            }

            return diffNumber == 1;
        }

        static void Main(string[] args)
        {


            string line;

            List<int> frequencies = new List<int>();
            var lines = File.ReadAllLines("../../input.txt");

            System.IO.StreamReader file = new System.IO.StreamReader("../../input.txt");

            int lineNumber = 0;
            while ((line = file.ReadLine()) != null)
            {
                for (int i = lineNumber+1; i < lines.Length; i++)
                {
                    if (OnlyOneDiffLetters(line, lines[i]))
                    {
                        Console.WriteLine(line);
                        Console.WriteLine(lines[i]);



                        goto a;
                    }
                
                }

                lineNumber++;
            }
            a:

            file.Close();

            
            Console.ReadKey();
        }

        static void Main1(string[] args)
        {
            int exactly_twice = 0;
            int exactly_three = 0;

            string line;

            List<int> frequencies = new List<int>();
            System.IO.StreamReader file = new System.IO.StreamReader("../../input.txt");

            while ((line = file.ReadLine()) != null)
            {
               var k = line.GroupBy(c => c).Select(c => new { Char = c.Key, Count = c.Count() });


                bool twice_counts = true;
                bool three_counts = true;
                foreach (var c in k)
                {
                    if (c.Count == 2&& twice_counts)
                    {
                        exactly_twice++;
                        twice_counts = false;
                    }
                    if (c.Count == 3&& three_counts)
                    {
                        exactly_three++;
                        three_counts = false;
                    }

                    //Console.WriteLine("Char:{0} Count:{1}", c.Char, c.Count);
                }
            }

            file.Close();
            

            Console.WriteLine(exactly_twice*exactly_three);
            Console.ReadKey();
        }
    }
}
