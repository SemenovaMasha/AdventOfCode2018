using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            System.IO.StreamReader file = new System.IO.StreamReader("../../input.txt");

            string line;

            List<int>[,] fabric = new List<int>[1000,1000];
            for (int i = 0; i < fabric.GetLength(0); i++)
            {
                for (int j = 0; j < fabric.GetLength(1); j++)
                {
                    fabric[i,j]= new List<int>();
                }
            }

            List<int> notOverpaled = new List<int>();

            int line_number = 0;
            while ((line = file.ReadLine()) != null)
            {
                int claimId = Convert.ToInt32(line.Split('@')[0].Substring(1));
                notOverpaled.Add(claimId);

                line = line.Split('@')[1];
                string[] parts = line.Split(':');
                string[] values = parts[0].Split(',');

                int start_i = Convert.ToInt32(values[1]);
                int start_j = Convert.ToInt32(values[0]);

                values = parts[1].Split('x');
                int last_i = start_i+Convert.ToInt32(values[1]);
                int last_j = start_j+ Convert.ToInt32(values[0]);
                

                for (int i = start_i; i < last_i; i++)
                {
                    for (int j = start_j; j < last_j; j++)
                    {
                        if (fabric[i, j].Count > 0)
                        {
                            foreach (int Id in fabric[i,j])
                            {
                                notOverpaled.Remove(Id);
                            }
                            notOverpaled.Remove(claimId);
                        }
                        fabric[i, j].Add(claimId);
                    }
                }

                if (line_number++ % 100 == 0)
                {
                    Console.WriteLine(line_number);
                }
            }

            file.Close();

            //Part1
            //int count = 0;

            //for (int i = 0; i < fabric.GetLength(0); i++)
            //{
            //    for (int j = 0; j < fabric.GetLength(1); j++)
            //    {
            //        if (fabric[i, j].Count > 1)
            //            count++;
            //    }
            //}
            //Console.WriteLine(count);

            foreach (int Id in notOverpaled)
            {
                Console.WriteLine(Id);
            }


            Console.ReadKey();
        }
    }
}
