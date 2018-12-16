using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task14
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> Recipes = new List<int>();
            Recipes.Add(3);
            Recipes.Add(7);

            int Position1 = 0;
            int Position2 = 1;

            //string input = "59414";
            int[] input = new[] { 0,7,7,2,0,1 };
            int i = 0;
            while (true)
            {
                int NewRecipe = Recipes[Position1] + Recipes[Position2];
                if(NewRecipe>=10)
                    Recipes.Add(NewRecipe/10);

                bool end = true;
                for (int j = 0; j < input.Length; j++)
                {
                    if (Recipes[Recipes.Count - 1 - j] != input[input.Length - 1 - j])
                    {
                        end = false;
                        break;
                    }
                }
                if(end)
                    break;

                //if(NewRecipe / 10==input[5]&& Recipes[Recipes.Count - 2] == input[4] &&
                //   Recipes[Recipes.Count - 3] == input[3] && Recipes[Recipes.Count - 4] == input[2] 
                //   && Recipes[Recipes.Count - 5] == input[1] && Recipes[Recipes.Count - 6] == input[0])
                //    break;

                Recipes.Add(NewRecipe%10);

                Position1 = GetNextPosition(Position1, Recipes, 1+Recipes[Position1]);
                Position2 = GetNextPosition(Position2, Recipes, 1+Recipes[Position2]);

                 end = true;
                for (int j = 0; j < input.Length; j++)
                {
                    if (Recipes[Recipes.Count - 1 - j] != input[input.Length - 1 - j])
                    {
                        end = false;
                        break;
                    }
                }
                if (end)
                    break;

                if (++i%10_000_000==0)
                    Console.WriteLine(i);
            }
            a:
            //Console.WriteLine(String.Join(" ", Recipes));
            Console.WriteLine(String.Join("", Recipes.Skip(Recipes.Count - 10).Take(10)));

            Console.WriteLine(Recipes.Count - 5);

            Console.ReadKey();
        }

        static int GetNextPosition(int Position,List<int> Recipes,int Shift)
        {
            //for (int i = 0; i < Shift; i++)
            //{
            //    Position++;
            //    if (Position >= Recipes.Count)
            //        Position = 0;
            //}

            Position = (Position + Shift) % Recipes.Count;

            return Position;
        }
    }
}
