using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = File.ReadAllText("../../input.txt");
            List<char> chars = input.ToCharArray().ToList();

            for (int i = 0; i < chars.Count-1; i++)
            {
                if (char.ToUpper(chars[i]) == char.ToUpper(chars[i + 1]) && chars[i] != chars[i + 1])
                {
                    chars.RemoveAt(i);
                    chars.RemoveAt(i);
                    i -= 2;
                    if (i < -1) i = -1;
                }
            }

            char[] distinct = chars.Select(char.ToUpper).Distinct().ToArray();

            int min = input.Length;
            for (int i = 0; i < distinct.Length; i++)
            {
                List<char> charsNew = new List<char>(chars);
                charsNew.RemoveAll(d => d == distinct[i]|| char.ToUpper(d) == distinct[i]);

                for (int index = 0; index < charsNew.Count - 1; index++)
                {
                    if (char.ToUpper(charsNew[index]) == char.ToUpper(charsNew[index + 1]) && charsNew[index] != charsNew[index + 1])
                    {
                        charsNew.RemoveAt(index);
                        charsNew.RemoveAt(index);
                        index -= 2;
                        if (index < -1) index = -1;
                    }
                }

                if (min > charsNew.Count)
                {
                    min = charsNew.Count;
                }
            }


            //Console.WriteLine(new string(chars.ToArray()));
            Console.WriteLine(min);

            Console.ReadKey();
        }
    }
}
