using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Task13
{
    class Program
    {
        static void Main(string[] args)
        {
            System.IO.StreamReader file = new System.IO.StreamReader("../../input.txt");
            string line;
            int N = File.ReadAllLines("../../input.txt").Length;
            int M = File.ReadAllLines("../../input.txt")[0].Length;

            int [,] Field = new int[N+2,M+2];
            List<Cart> Carts = new List<Cart>();

            int lineNumber = 0;
            int CartN = 0;
            while ((line = file.ReadLine()) != null)
            {
                for (int j = 0; j < line.Length; j++)
                {
                    if (line[j] == '|' || line[j] == '-')
                    {
                        Field[lineNumber+1, j+1] = 1;
                    }
                    else if (line[j] == '/' )
                    {
                        Field[lineNumber+1, j+1] = 3;
                    }else if (line[j] == '\\' )
                    {
                        Field[lineNumber+1, j+1] = 4;
                    }else if (line[j] == '+')
                    {
                        Field[lineNumber + 1, j + 1] = 2;
                    }
                    else if (line[j] == '<' || line[j] == '>' || line[j] == '^' || line[j] == 'v')
                    {
                        Cart cart = new Cart(CartN++);
                        Field[lineNumber + 1, j + 1] = 1;

                        cart.X = j + 1;
                        cart.Y = lineNumber + 1;
                        switch (line[j])
                        {
                            case '<':
                                cart.Direction = Directions.Left;
                                break;
                            case '>':
                                cart.Direction = Directions.Right;
                                break;
                            case '^':
                                cart.Direction = Directions.Up;
                                break;
                            case 'v':
                                cart.Direction = Directions.Down;
                                break;
                        }
                        Carts.Add(cart);
                    }
                }

                lineNumber++;
            }
            file.Close();
            
            Print(Field,Carts);

            Mover mover = new Mover(Field);
            CartComparer cartComparer = new CartComparer();
            while (Carts.Count>1)
            {
                List<Cart> cartsSorted = new List<Cart>(Carts);
                cartsSorted.Sort(cartComparer);
                while (cartsSorted.Count>0)
                {
                    mover.MoveCart(cartsSorted[0]);

                    for(int i=0;i<Carts.Count;i++)
                    {
                        if (Carts[i] != cartsSorted[0]&& Carts[i].Equals(cartsSorted[0]))
                        {
                            cartsSorted.RemoveAll(item => object.ReferenceEquals(item, Carts[i]));
                            Carts.Remove(Carts[i]);
                            Carts.Remove(cartsSorted[0]);
                            break;
                        }
                    }
                    cartsSorted.RemoveAt(0);

                }
                //Print(Field, Carts);
            }
            //mover.MoveCart(Carts[0]);

            Console.WriteLine((Carts[0].X-1)+","+ (Carts[0].Y-1));

            Console.ReadKey();
        }

        static void Print(int[,] Array)
        {
            for (int i = 0; i < Array.GetLength(0); i++)
            {
                for (int j = 0; j < Array.GetLength(1); j++)
                {
                    Console.Write(Array[i,j]+" ");
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }
        static void Print(int[,] Array,List<Cart> Carts)
        {
            for (int i = 0; i < Array.GetLength(0); i++)
            {
                for (int j = 0; j < Array.GetLength(1); j++)
                {
                    Cart cart = Carts.FirstOrDefault(c => c.Y == i && c.X == j);
                    if (cart != null)
                    {
                        if(cart.Direction==Directions.Down) Console.Write("v");
                        if(cart.Direction==Directions.Right) Console.Write(">");
                        if(cart.Direction==Directions.Left) Console.Write("<");
                        if(cart.Direction==Directions.Up) Console.Write("^");
                    }
                    else
                    {
                        if(Array[i, j]==0)
                            Console.Write(" ");
                        if(Array[i, j]==1)
                            Console.Write("*");
                        if(Array[i, j]==2)
                            Console.Write("+");
                        if(Array[i, j]==3)
                            Console.Write("/");
                        if(Array[i, j]==4)
                            Console.Write("\\");
                    }
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }
    }
    public enum Directions { Up, Down, Left, Right }
    public enum ChooseDirection { Left,Right,Straight }

    public class CartComparer : IComparer<Cart>
    {
        public int Compare(Cart one, Cart two)
        {
            if (one.Y < two.Y) return -1;
            if (two.Y < one.Y) return 1;
            if (one.X < two.X) return -1;
            if (two.X < one.X) return 1;

            return 0;
        }

    }

    public class Cart
    {
        public int N { get; set; }
        public int X { get; set; }
        public int Y{ get; set; }
        public Directions Direction { get; set; }
        public ChooseDirection NextDirection { get; set; }

        public Cart(int N)
        {
            NextDirection = ChooseDirection.Left;
            this.N = N;
        }

        public void ChangeDirection()
        {
            if (NextDirection == ChooseDirection.Straight)
            {
                NextDirection = ChooseDirection.Right;
                return;
            }

            if (NextDirection == ChooseDirection.Right)
            {
                switch (Direction)
                {
                    case Directions.Up:
                        Direction = Directions.Right;
                        break;
                    case Directions.Right:
                        Direction = Directions.Down;
                        break;
                    case Directions.Down:
                        Direction = Directions.Left;
                        break;
                    case Directions.Left:
                        Direction = Directions.Up;
                        break;
                }

                NextDirection = ChooseDirection.Left;
                return;
            }
            if (NextDirection == ChooseDirection.Left)
            {
                switch (Direction)
                {
                    case Directions.Up:
                        Direction = Directions.Left;
                        break;
                    case Directions.Right:
                        Direction = Directions.Up;
                        break;
                    case Directions.Down:
                        Direction = Directions.Right;
                        break;
                    case Directions.Left:
                        Direction = Directions.Down;
                        break;
                }

                NextDirection = ChooseDirection.Straight;
                return;
            }
        }

        public (int X, int Y) GetNewPosition()
        {
            if (Direction == Directions.Down)
                return (X, Y+1);
            if (Direction == Directions.Up)
                return (X, Y-1);
            if (Direction == Directions.Left)
                return (X-1, Y);
            if (Direction == Directions.Right)
                return (X+1, Y);
            return (-1, -1);
        }

        public void Move()
        {
            if (Direction == Directions.Left) X--;
            else if(Direction == Directions.Right) X++;
            else if(Direction == Directions.Down) Y++;
            else Y--;
        }

        public override bool Equals(object obj)
        {
            Cart other = obj as Cart;
            return other.X == X && other.Y == Y;
        }
    }

    class Mover
    {
        private int[,] Field;

        public Mover(int[,] field)
        {
            Field = field;
        }

        public void MoveCart(Cart cart)
        {
            if (Field[cart.Y, cart.X] == 2)
            {
                cart.ChangeDirection();
            }else if (Field[cart.Y, cart.X] == 3 || Field[cart.Y, cart.X] == 4)
            {
                if (Field[cart.Y, cart.X] == 3)
                {
                    if (cart.Direction==Directions.Up)
                    {
                        cart.Direction = Directions.Right;
                    }else if (cart.Direction == Directions.Right)
                    {
                        cart.Direction = Directions.Up;
                    }
                    else if (cart.Direction == Directions.Left)
                    {
                        cart.Direction = Directions.Down;
                    }
                    else if (cart.Direction == Directions.Down)
                    {
                        cart.Direction = Directions.Left;
                    }
                }
                else
                {
                    if (cart.Direction == Directions.Up)
                    {
                        cart.Direction = Directions.Left;
                    }
                    else if (cart.Direction == Directions.Left)
                    {
                        cart.Direction = Directions.Up;
                    }
                    else if (cart.Direction == Directions.Right)
                    {
                        cart.Direction = Directions.Down;
                    }
                    else if (cart.Direction == Directions.Down)
                    {
                        cart.Direction = Directions.Right;
                    }
                }


                //(int nextX, int nextY) = cart.GetNewPosition();
                //if (nextX < 0 || nextY < 0 || Field[nextY, nextX] == 0)
                //{
                //    if (cart.Direction != Directions.Right && Field[cart.Y, cart.X - 1] != 0)
                //    {
                //        cart.Direction = Directions.Left;
                //    }
                //    else if (cart.Direction != Directions.Up && Field[cart.Y + 1, cart.X] != 0)
                //    {
                //        cart.Direction = Directions.Down;
                //    }
                //    else if (cart.Direction != Directions.Left && Field[cart.Y, cart.X + 1] != 0)
                //    {
                //        cart.Direction = Directions.Right;
                //    }
                //    else if (cart.Direction != Directions.Down && Field[cart.Y - 1, cart.X] != 0)
                //    {
                //        cart.Direction = Directions.Up;
                //    }
                //}
            }
            cart.Move();

        }
    }
}
