using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> frequencies = new List<int>();
            frequencies.Add(0);
            int result = 0;
            string line;
            
            while (true)
            {

                System.IO.StreamReader file = new System.IO.StreamReader("../../input.txt");
                while ((line = file.ReadLine()) != null)
                {
                    if (line[0] == '+')
                        result += Convert.ToInt32(line.Substring(1));
                    else
                        result -= Convert.ToInt32(line.Substring(1));

                    if (frequencies.Contains((result)))
                    {
                        Console.WriteLine(result);
                        file.Close();
                        goto a; //yp, its goto
                    }
                    frequencies.Add(result);
                }

                file.Close();
            }
            a:

            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
