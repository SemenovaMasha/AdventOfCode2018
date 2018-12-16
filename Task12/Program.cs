using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task12
{
    class Program
    {
        static void Main(string[] args)
        {

            System.IO.StreamReader file = new System.IO.StreamReader("../../input.txt");

            int ZeroPosition = 0;
            string line;
            string state = file.ReadLine().Split(' ')[2];
            StringBuilder stateBuilder = new StringBuilder(state);

            line = file.ReadLine();

            //while (!state.StartsWith("...."))
            //{
            //    state = "." + state;
            //}

            //while (!state.EndsWith("....."))
            //{
            //    state += ".";
            //}

            List<string> TrueRules = new List<string>();

            int lineNumber = 0;
            while ((line = file.ReadLine()) != null)
            {
                string[] split = line.Split(' ');

                if (split[2] == "#")
                    TrueRules.Add(split[0]);
            }
            file.Close();
            int i = 0;
            int oldValue = 0;
            for (i = 0; i < 1000; i++)
            {
                while (!state.StartsWith("...."))
                {
                    state = "." + state;
                    ZeroPosition++;
                }

                while (!state.EndsWith("....."))
                {
                    state += ".";
                }

                stateBuilder = new StringBuilder(state);

                for (int j = 2; j < stateBuilder.Length - 2; j++)
                {
                    if (TrueRules.Contains(state.Substring(j - 2, 5)))
                    {
                        stateBuilder[j] = '#';
                    }
                    else
                    {
                        stateBuilder[j] = '.';
                    }
                }

                state = stateBuilder.ToString();

                //if (i % 1_000 == 0)
                {
                    //Console.WriteLine(i);
                    int value = 0;
                    for (int index = 0; index < state.Length; index++)
                    {
                        if (state[index] == '#')
                            value += index - ZeroPosition;
                    }

                    //Console.WriteLine(value- oldValue);
                    oldValue = value;
                }


                //if (i % 10_000 == 0 && i != 0)
                //{
                //    break;
                //}

                //Console.WriteLine(state);
            }

            Console.WriteLine(oldValue + (50_000_000_000 - 1000) * 53);


            Console.ReadKey();
        }
    }
}
