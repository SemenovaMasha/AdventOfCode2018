using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task18
{
    class Program
    {
        private static int[,] Field;
        private static int N;
        private static int M;
        static void Main(string[] args)
        {
            System.IO.StreamReader file = new System.IO.StreamReader("../../input.txt");
            string line;
            N = File.ReadAllLines("../../input.txt").Length+2;
            M = File.ReadAllLines("../../input.txt")[0].Length+2;

            Field = new int[N, M];

            int lineNumber = 1;
            while ((line = file.ReadLine()) != null)
            {
                for (int j = 0; j < line.Length; j++)
                {
                    Field[lineNumber, j + 1] = line[j];
                }
                
                lineNumber++;
            }
            file.Close();

            int[,] NewField = new int[N, M];

            List<int> tmpT = new List<int>();
            List<int> tmpL = new List<int>();

            for (int round = 0; round < 10_000; round++)
            {
                for (int i = 1; i < N-1; i++)
                {
                    for (int j = 1; j < M-1; j++)
                    {
                        if (Field[i, j] == '.')
                        {
                            if (OpenToTree(j, i))
                                NewField[i, j] = '|';
                            else
                                NewField[i, j] = '.';
                        }
                        else if (Field[i, j] == '|')
                        {
                            if (TreeToLumber(j, i))
                                NewField[i, j] = '#';
                            else
                                NewField[i, j] = '|';
                        }
                        else
                        {
                            if (!LumberToLumber(j, i))
                                NewField[i, j] = '.';
                            else
                                NewField[i, j] = '#';

                        }
                    }
                }

                Field = NewField;

                NewField = new int[N, M];

                if(round%1_000_000==0)
                    Console.WriteLine(round);

                //if (round % 10_000 == 0)
                //{
                int t = 0;
                int l = 0;
                for (int i = 1; i < N - 1; i++)
                {
                    for (int j = 1; j < M - 1; j++)
                    {
                        if (Field[i, j] == '|')
                            t++;
                        if (Field[i, j] == '#')
                            l++;
                    }
                }
                //    //Console.WriteLine("Round: "+round);
                //    Console.WriteLine("Tree: " + t);
                //    Console.WriteLine("Lumber: " + l);
                //    //Console.WriteLine("*: "+(t*l));
                //    Console.WriteLine();
                //}


                if (tmpT.Contains(t))
                {
                        int k = 20;
                    if (tmpT.Count > 1000)
                    {
                        //bool pat = true;
                        

                        if (ContainsSequence(tmpT.GetRange(0, tmpT.Count - k), tmpT.GetRange(tmpT.Count - k, k)))
                        {
                            Console.WriteLine();
                        }
                    }
                }

                tmpT.Add(t);
                tmpL.Add(l);

                //Print();;
            }

            int tree_count = 0;
            int lumb_count = 0;
            for (int i = 1; i < N - 1; i++)
            {
                for (int j = 1; j < M - 1; j++)
                {
                    if (Field[i, j] == '|')
                        tree_count++;
                    if (Field[i, j] == '#')
                        lumb_count++;
                }
            }
            Console.WriteLine(tree_count* lumb_count);


            Console.ReadKey();
        }
        public static bool ContainsSequence<T>(IEnumerable<T> source,
            IEnumerable<T> other)
        {
            int count = other.Count();

            while (source.Any())
            {
                if (source.Take(count).SequenceEqual(other))
                    return true;
                source = source.Skip(1);
            }
            return false;
        }

        static void Print()
        {
            for (int i = 1; i < N - 1; i++)
            {
                for (int j = 1; j < M - 1; j++)
                {
                    Console.Write((char)Field[i,j]);
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }

        static bool OpenToTree(int x, int y)
        {
            int trees_count = 0;
            if (Field[y - 1, x - 1] == '|') trees_count++; if (Field[y - 1, x] == '|') trees_count++; if (Field[y - 1, x + 1] == '|') trees_count++;
            if (Field[y, x - 1] == '|') trees_count++; if (Field[y, x + 1] == '|') trees_count++; 
            if (Field[y + 1, x - 1] == '|') trees_count++; if (Field[y + 1, x] == '|') trees_count++; if (Field[y + 1, x + 1] == '|') trees_count++;

            return trees_count >= 3;
        }

        static bool TreeToLumber(int x, int y)
        {
            int lumber_count = 0;
            if (Field[y - 1, x - 1] == '#') lumber_count++; if (Field[y - 1, x] == '#') lumber_count++; if (Field[y - 1, x + 1] == '#') lumber_count++; 
            if (Field[y, x - 1] == '#') lumber_count++; if (Field[y, x + 1] == '#') lumber_count++; 
            if (Field[y + 1, x - 1] == '#') lumber_count++; if (Field[y + 1, x] == '#') lumber_count++; if (Field[y + 1, x + 1] == '#') lumber_count++;

            return lumber_count >= 3;
        }
        static bool LumberToLumber(int x, int y)
        {
            int trees_count = 0;
            if (Field[y - 1, x - 1] == '|') trees_count++; if (Field[y - 1, x] == '|') trees_count++; if (Field[y - 1, x + 1] == '|') trees_count++;
            if (Field[y, x - 1] == '|') trees_count++; if (Field[y, x + 1] == '|') trees_count++;
            if (Field[y + 1, x - 1] == '|') trees_count++; if (Field[y + 1, x] == '|') trees_count++; if (Field[y + 1, x + 1] == '|') trees_count++;

            int lumber_count = 0;
            if (Field[y - 1, x - 1] == '#') lumber_count++; if (Field[y - 1, x] == '#') lumber_count++; if (Field[y - 1, x + 1] == '#') lumber_count++;
            if (Field[y, x - 1] == '#') lumber_count++; if (Field[y, x + 1] == '#') lumber_count++;
            if (Field[y + 1, x - 1] == '#') lumber_count++; if (Field[y + 1, x] == '#') lumber_count++; if (Field[y + 1, x + 1] == '#') lumber_count++;

            return trees_count >=1 && lumber_count>=1;
        }
    }
}
