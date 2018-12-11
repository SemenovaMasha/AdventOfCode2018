using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Task11
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] Cells = new int[301,301];
            int SerialNumber = 9435;

            for (int i = 1; i < Cells.GetLength(0); i++)
            {
                for (int j = 1; j < Cells.GetLength(1); j++)
                {
                    int RackID = j + 10;
                    int result = RackID * i+SerialNumber;
                    result *= RackID;
                    result %= 1000;
                    result /= 100;
                    result -= 5;
                    Cells[j, i] = result;

                }
            }

            List<int[,]> SquareSums = new List<int[,]>();
            SquareSums.Add(new int[1,1]);
            SquareSums.Add(new int[301,301]);
            for (int i = 1; i < Cells.GetLength(0); i++)
            {
                for (int j = 1; j < Cells.GetLength(1); j++)
                {
                    SquareSums[1][i, j] = Cells[i, j];
                }
            }

            int maxSum = GetSum(Cells, 2, 2,1);
            int X = 1, Y = 1,Size = 1;

            //PrintSquare(100, 100, 10, SquareSums[1]);
            //Console.WriteLine();
            for (int k = 2; k <= 300; k++)
            {
                SquareSums.Add(new int[302-k,302-k]);
                for (int i = 1; i < Cells.GetLength(0)-k+1; i++)
                {
                    for (int j = 1; j < Cells.GetLength(1) - k + 1; j++)
                    {
                        int sum = GetSum(Cells, j, i,k,SquareSums[k-1]);
                        if (sum > maxSum)
                        {
                            maxSum = sum;
                            X = j;
                            Y = i;
                            Size = k;
                        }

                        SquareSums[k][i, j] = sum;
                    }
                }

                //PrintSquare(100, 100, 10, SquareSums[k]);
                //Console.WriteLine();


                if (k%10==0)
                    Console.WriteLine(k);
            }

            
            Console.WriteLine(X+" "+Y);
            Console.WriteLine("Size: "+Size+" Sum: "+maxSum);

            Console.WriteLine(Y+","+X+","+Size);

            Console.ReadKey();
        }

        static int GetSum(int[,] Cells, int LeftX, int LeftY, int Size)
        {
            int sum = 0;
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    sum += Cells[LeftY + i, LeftX + j];
                }
            }

            return sum;
        }

        static void PrintSquare(int X, int Y, int Size, int[,] Cells)
        {
            for (int i = Y; i < Y+Size; i++)
            {
                for (int j = X; j < X+Size; j++)
                {
                    Console.Write((Cells[i,j]+" ").PadLeft(4));
                }

                Console.WriteLine();
            }
        }

        static int GetSum(int[,] Cells, int LeftX, int LeftY, int Size, int[,] CurrentSizeSums)
        {
            if (LeftX == 100 && LeftY == 100)
            {

            }
            int sum = CurrentSizeSums[LeftY, LeftX];
            for (int i = LeftY; i < LeftY+Size; i++)
            {
                sum += Cells[i, LeftX+Size-1];
            }

            for (int j = LeftX; j < LeftX+Size-1; j++)
            {
                sum += Cells[LeftY+Size-1, j];
            }

            return sum;
        }
    }
}
