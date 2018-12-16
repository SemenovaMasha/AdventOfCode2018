using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task15
{
    class Program
    {
        private static string[] OpNames = new string[] { "addr", "addi", "mulr", "muli", "banr", "bani", "borr", "bori",
            "setr", "seti", "gtir", "gtri", "gtrr", "eqir", "eqri", "eqrr", };
        static int [] Registers = new int[4];

        static void Main(string[] args)
        {
            string line;

            List<int>[] guesses = new List<int>[16];
            for (int i = 0; i < guesses.Length; i++)
            {
                guesses[i] = new List<int>();
            }
            
            System.IO.StreamReader file = new System.IO.StreamReader("../../input.txt");

            int count = 0;
            int lineNumber = 0;
            while ((line = file.ReadLine()) != null)// \r\n
            {
                string beforeS = file.ReadLine();
                beforeS = beforeS.Split('[')[1].Split(']')[0];
                int[] before = GetRegistersFromString(beforeS);

                string command = file.ReadLine();
                int[] arg = GetArgsFromString(command.Substring(command.IndexOf(' ')+1));
                int GuessedOpCode = Convert.ToInt32(command.Split(' ')[0]);

                string afterS = file.ReadLine();
                afterS = afterS.Split('[')[1].Split(']')[0];
                int [] after = GetRegistersFromString(afterS);

                int matches = 0;
                for (int i = 0; i < OpNames.Length; i++)
                {
                    SetRegisters(before);
                    DoOperation(OpNames[i], arg);
                    if (RegistersEqual(after))
                    {
                        matches++;
                        if(!guesses[GuessedOpCode].Contains(i))
                            guesses[GuessedOpCode].Add(i);
                    }
                    
                }

                if (matches >= 3)
                {
                    count++;
                }
                lineNumber++;
            }
            a:

            file.Close();

            int [] OpCodes = new int[16];

            for (int i = 0; i < OpCodes.Length; i++)
            {
                OpCodes[i] = -1;
            }

            while (OpCodes.Contains(-1))
            {
                var guess = guesses.FirstOrDefault(g => g.Count == 1);
                int guessN = guess[0];

                OpCodes[Array.IndexOf(guesses, guess)] = guessN;

                foreach (var list in guesses)
                {
                    list.Remove(guessN);
                }


            }

            SetRegisters(new int[]{0,0,0,0});

            file = new System.IO.StreamReader("../../input2.txt");
            
            while ((line = file.ReadLine()) != null)
            {
                int[] arg = GetArgsFromString(line.Substring(line.IndexOf(' ') + 1));
                int OpCode = Convert.ToInt32(line.Split(' ')[0]);

                DoOperation(OpNames[OpCodes[OpCode]], arg);

            }

            file.Close();
            

            Console.WriteLine(Registers[0]);

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
                    Registers[args[2]] = args[0]>Registers[args[1]]?1:0;
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
                 || OpName == "borr" || OpName == "gtir"|| OpName == "gtrr"
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
            int [] r = new int[4];
            string[] values = s.Split(',');
            for (int i = 0; i < values.Length; i++)
            {
                r[i] = Convert.ToInt32(values[i]);
            }

            return r;
        }
        static int[] GetArgsFromString(string s)
        {
            int [] r = new int[3];
            string[] values = s.Split(' ');
            for (int i = 0; i < values.Length; i++)
            {
                r[i] = Convert.ToInt32(values[i]);
            }

            return r;
        }
    }
}
