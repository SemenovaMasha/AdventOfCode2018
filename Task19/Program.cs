using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task19
{
    class Command
    {
        public string Name { get; set; }
        public int[] args { get; set; }
    }
    class Program
    {
        private static string[] OpNames = new string[] { "addr", "addi", "mulr", "muli", "banr", "bani", "borr", "bori",
            "setr", "seti", "gtir", "gtri", "gtrr", "eqir", "eqri", "eqrr", };
        static long[] Registers = new long[6];

        static void Main(string[] args)
        {
            Registers[0] = 1;
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader("../../input.txt");
            line = file.ReadLine();

            int ip = Convert.ToInt32(line.Split(' ')[1]);
            
            int count = 0;
            int lineNumber = 0;

            List<Command> Commands = new List<Command>();

            while ((line = file.ReadLine()) != null)// \r\n
            {
                string command = line.Split(' ')[0];
                int[] arg = GetArgsFromString(line.Substring(line.IndexOf(' ') + 1));
                Commands.Add(new Command(){Name =  command,args = arg});
              
                lineNumber++;
            }

            file.Close();

            SetRegisters(new[] { 0, 1, 0, 10, 10551387, 10551387 });
            int step = 0;
            while (Registers[ip]>=0 && Registers[ip] <Commands.Count)
            {
                DoOperation(Commands[(int)Registers[ip]].Name,Commands[(int)Registers[ip]].args);
                Registers[ip]++;

                step++;
                if (step % 1_000_000 == 0)
                {
                    Console.WriteLine(Registers[0] + " " + Registers[1] + " " + Registers[2] +
                                      " " + Registers[3] + " " + Registers[4] + " " + Registers[5]);
                }

                if (step % 100_000_000 == 0)
                {
                    Console.WriteLine(Registers[0] + " " + Registers[1] + " " + Registers[2] +
                                      " " + Registers[3] + " " + Registers[4] + " " + Registers[5]);
                }
                if (step % 10_000 == 0)
                {

                }

                if (Registers[3] == 10551386)
                {

                }
            }

            Console.WriteLine(step);
            Console.WriteLine(Registers[0] + " " + Registers[1] + " " + Registers[2] +
                              " " + Registers[3] + " " + Registers[4] + " " + Registers[5]);

            Console.ReadKey();
        }

        static void SetRegisters(int[] registers)
        {
            for (int i = 0; i < registers.Length; i++)
            {
                Registers[i] = registers[i];
            }
        }

        static void DoOperation(string OpName, int[] args)
        {
            switch (OpName)
            {
                case "addr":
                    Registers[args[2]] = Registers[args[0]] + Registers[args[1]];
                    break;
                case "addi":
                    Registers[args[2]] = Registers[args[0]] + args[1];
                    break;
                case "mulr":
                    Registers[args[2]] = Registers[args[0]] * Registers[args[1]];
                    break;
                case "muli":
                    Registers[args[2]] = Registers[args[0]] * args[1];
                    break;
                case "banr":
                    Registers[args[2]] = Registers[args[0]] & Registers[args[1]];
                    break;
                case "bani":
                    Registers[args[2]] = Registers[args[0]] & args[1];
                    break;
                case "borr":
                    Registers[args[2]] = Registers[args[0]] | Registers[args[1]];
                    break;
                case "bori":
                    Registers[args[2]] = Registers[args[0]] | args[1];
                    break;
                case "setr":
                    Registers[args[2]] = Registers[args[0]];
                    break;
                case "seti":
                    Registers[args[2]] = args[0];
                    break;
                case "gtir":
                    Registers[args[2]] = args[0] > Registers[args[1]] ? 1 : 0;
                    break;
                case "gtri":
                    Registers[args[2]] = Registers[args[0]] > args[1] ? 1 : 0;
                    break;
                case "gtrr":
                    Registers[args[2]] = Registers[args[0]] > Registers[args[1]] ? 1 : 0;
                    break;
                case "eqir":
                    Registers[args[2]] = args[0] == Registers[args[1]] ? 1 : 0;
                    break;
                case "eqri":
                    Registers[args[2]] = Registers[args[0]] == args[1] ? 1 : 0;
                    break;
                case "eqrr":
                    Registers[args[2]] = Registers[args[0]] == Registers[args[1]] ? 1 : 0;
                    break;
            }
        }

        static bool IsOpAvailable(string OpName, int[] args)
        {
            if ((OpName == "addr" || OpName == "addi" || OpName == "mulr" || OpName == "muli" || OpName == "banr" || OpName == "bani"
                 || OpName == "borr" || OpName == "bori" || OpName == "setr" || OpName == "gtri" || OpName == "gtrr"
                 || OpName == "eqri" || OpName == "eqrr")
                && (args[0] > 3 || args[0] < 0))
            {
                return false;
            }
            if ((OpName == "addr" || OpName == "mulr" || OpName == "banr"
                 || OpName == "borr" || OpName == "gtir" || OpName == "gtrr"
                 || OpName == "eqir" || OpName == "eqrr")
                && (args[1] > 3 || args[1] < 0))
            {
                return false;
            }

            return true;

        }

        static bool RegistersEqual(int[] registers)
        {
            for (int i = 0; i < registers.Length; i++)
            {
                if (Registers[i] != registers[i])
                    return false;
            }

            return true;
        }

        static int[] GetRegistersFromString(string s)
        {
            int[] r = new int[4];
            string[] values = s.Split(',');
            for (int i = 0; i < values.Length; i++)
            {
                r[i] = Convert.ToInt32(values[i]);
            }

            return r;
        }
        static int[] GetArgsFromString(string s)
        {
            int[] r = new int[3];
            string[] values = s.Split(' ');
            for (int i = 0; i < values.Length; i++)
            {
                r[i] = Convert.ToInt32(values[i]);
            }

            return r;
        }
    }
}
