using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;

namespace Task6
{
    class Program
    {
        static void Main(string[] args)
        {
            System.IO.StreamReader file = new System.IO.StreamReader("../../input.txt");

            List<Dot> dots = new List<Dot>();

            string line;

            int dot_n = 1;
            while ((line = file.ReadLine()) != null)
            {
                string[] l = line.Split(',');
                dots.Add(new Dot { Number = dot_n++, X = Convert.ToInt32(l[0]), Y = Convert.ToInt32(l[1]) });
            }

            file.Close();

            int max_X = dots.Max(d => d.X);
            int max_Y = dots.Max(d => d.Y);

            int[,] field = new int[max_Y + 3, max_X + 3];

            int count = 0;

            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    for (int k = 0; k < dots.Count; k++)
                    {
                        field [i,j] += GetDistance(dots[k], i, j);
                    }

                    if (field[i, j] < 10000)
                        count++;
                }

            }
            Console.WriteLine(count);


            Console.ReadKey();
        }
        static void Main1(string[] args)
        {
            System.IO.StreamReader file = new System.IO.StreamReader("../../input.txt");

            List<Dot> dots = new List<Dot>();

            string line;

            int dot_n = 1;
            while ((line = file.ReadLine()) != null)
            {
                string[] l = line.Split(',');
                dots.Add(new Dot {Number = dot_n++, X = Convert.ToInt32(l[0]), Y = Convert.ToInt32(l[1])});
            }

            file.Close();

            int max_X = dots.Max(d => d.X);
            int max_Y = dots.Max(d => d.Y);

            int[,] field = new int[max_Y + 3, max_X + 3];

            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    int min_distance = max_X + max_Y;
                    int min_n = 0;
                    Dot dotWithMinDistance = dots[0];

                    for (int k = 0; k < dots.Count; k++)
                    {
                        int d = GetDistance(dots[k], i, j);
                        if (d < min_distance)
                        {
                            min_distance = d;
                            dotWithMinDistance = dots[k];
                            min_n = 0;
                        }
                        else if (d == min_distance)
                        {
                            min_n++;
                        }

                    }

                    if (min_n == 0)
                    {
                        field[i, j] = dotWithMinDistance.Number;
                    }

                }
            }

            //for (int j = 0; j < field.GetLength(1); j++)
            //{
            //    for (int i = 0; i < field.GetLength(0); i++)
            //    {
            //        Console.Write(field[i, j]);
            //    }

            //    Console.WriteLine();
            //}


            List<int> allnumbers = dots.Select(d => d.Number).ToList();
            for (int i = 0; i < field.GetLength(0); i++)
            {
                allnumbers.Remove(field[i, 0]);
                allnumbers.Remove(field[i, field.GetLength(1)-1]);
            }

            for (int j = 0; j < field.GetLength(1); j++)
            {
                allnumbers.Remove(field[0, j]);
                allnumbers.Remove(field[field.GetLength(0) - 1, j]);
            }

            int max = 0;

            int[]counts = new int[allnumbers.Count];
            
            for (int j = 0; j < field.GetLength(1); j++)
            {
                for (int i = 0; i < field.GetLength(0); i++)
                {
                    if (allnumbers.Contains(field[i, j]))
                    {
                        counts[allnumbers.IndexOf(field[i, j])]++;
                    }
                }
                
            }

            Console.WriteLine(counts.Max());


            Console.ReadKey();
        }

        public struct Dot
        {
            public int Number;
            public int X;
            public int Y;
        }

        public static int GetDistance(Dot dot1, Dot dot2)
        {
            return Math.Abs(dot1.X - dot2.X) + Math.Abs(dot2.Y - dot1.Y);
        }
        public static int GetDistance(Dot dot1, int X, int Y)
        {
            return Math.Abs(dot1.X - X) + Math.Abs(Y - dot1.Y);
        }
    }
}
