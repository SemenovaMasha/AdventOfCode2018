using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Task20
{
    class Program
    {
        static void Main(string[] args)
        {
            string line;
            
            System.IO.StreamReader file = new System.IO.StreamReader("../../input.txt");
            line = file.ReadLine();
            int lineNumber = 0;
            
            file.Close();


            //int N = line.Count(c => c == 'N');
            //int S = line.Count(c => c == 'S');
            //int W = line.Count(c => c == 'W');
            //int E = line.Count(c => c == 'E');


            //Console.WriteLine(N + " " + S + " " + W + " " + E );

            int[,] Facility = new int[12000, 12000];
            //int[,] Facility = new int[30, 30];

            Point Current = new Point(){Y = Facility.GetLength(0) / 2 , X = Facility.GetLength(1) / 2 };

            Facility[Current.Y, Current.X] = '.';
            Stack<Point> PrevPoints = new Stack<Point>();
            Stack<int> PrevLenght = new Stack<int>();

            int count = 0;
            int max = 0;
            int currentLenght = 0;

            Dictionary<Point, int> distances = new Dictionary<Point, int>();
            distances.Add(Current,0);

            int prevChar = '.';

            foreach (char c in line)
            {
                switch (c)
                {
                    case 'W':
                        Facility[Current.Y, Current.X - 1] = '|';
                        Facility[Current.Y, Current.X - 2] = '.';
                        Current.X -= 2;
                        currentLenght++;
                        if (!distances.ContainsKey(Current))
                        {
                            distances.Add(Current, currentLenght);
                        }else if (distances[Current] > currentLenght) distances[Current] = currentLenght;
                        break;
                    case 'E':
                        Facility[Current.Y, Current.X + 1] = '|';
                        Facility[Current.Y, Current.X + 2] = '.';
                        Current.X += 2;
                        currentLenght++;
                        if (!distances.ContainsKey(Current))
                        {
                            distances.Add(Current, currentLenght);
                        }
                        else if (distances[Current] > currentLenght) distances[Current] = currentLenght;
                        break;
                    case 'S':
                        Facility[Current.Y + 1, Current.X] = '|';
                        Facility[Current.Y + 2, Current.X] = '.';
                        Current.Y += 2;
                        currentLenght++;
                        if (!distances.ContainsKey(Current))
                        {
                            distances.Add(Current, currentLenght);
                        }
                        else if (distances[Current] > currentLenght) distances[Current] = currentLenght;
                        break;
                    case 'N':
                        Facility[Current.Y - 1, Current.X] = '|';
                        Facility[Current.Y - 2, Current.X] = '.';
                        Current.Y -= 2;
                        currentLenght++;
                        if (!distances.ContainsKey(Current))
                        {
                            distances.Add(Current, currentLenght);
                        }
                        else if (distances[Current] > currentLenght) distances[Current] = currentLenght;
                        break;
                    case '(':
                        //Point prev = new Point(){X=Current.X,Y=Current.Y};
                        PrevPoints.Push(Current);
                        //PrevLenght.Push(currentLenght);
                        //record = true;

                        break;
                    case '|':
                        //Point prev1 = new Point() { X = PrevPoints.Peek().X, Y = PrevPoints.Peek().Y };
                        Current = PrevPoints.Peek();
                        currentLenght = distances[Current];
                        //currentLenght = PrevLenght.Peek();

                        break;
                    case ')':
                        if (prevChar == '|')
                        {
                            Current = PrevPoints.Pop();
                            currentLenght = distances[Current];
                        }
                        else
                        {
                            PrevPoints.Pop();
                            currentLenght = distances[Current];
                        }
                        //record = true;

                        break;
                    
                }

                if (currentLenght > max)
                {
                    max = currentLenght;
                }

                prevChar = c;

                //Print(Facility);

            }

            //Print(Facility);

            Console.WriteLine(distances.Max(d=>d.Value));
            Console.WriteLine(distances.Count(d=>d.Value>=1000));


            Console.ReadKey();
        }

        static void Print(int[,] A)
        {
            for (int i = 0; i < A.GetLength(0); i++)
            {
                for (int j = 0; j < A.GetLength(1); j++)
                {
                    if (i == A.GetLength(0)/2 && j == A.GetLength(1)/2) Console.Write("0");
                    else
                    {
                        if (A[i, j] == 0) Console.Write("#");
                        else Console.Write((char) A[i, j]);
                    }
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }

        struct Point
        {
            public int X { get; set; }
            public int Y { get; set; }
        }
    }
}
