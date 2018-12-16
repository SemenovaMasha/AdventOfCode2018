using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task9
{
    class Program
    {
        static void Main(string[] args)
        {

            int playersNum = 424;
            int marblesNum = 7_114_400;

            Player[] Players = new Player[playersNum];
            for (int i = 0; i < playersNum; i++)
            {
                Players[i] = new Player();
            }
            

            Node First = new Node(0);
            Node Last = First;

            First.Next = First;
            First.Prev = First;

            int currentPosition = 0;

            Node Current = First;
            for (int i = 1; i <= marblesNum; i++)
            {
                if (i % 23 != 0||i==0)
                {

                    Current = Current.Next;
                    Node newNode = new Node(i);
                    newNode.Next = Current.Next;
                    newNode.Prev = Current;
                    Current.Next = newNode;
                    newNode.Next.Prev = newNode;

                    Current = newNode;
                }
                else
                {
                    int playerIndex = i % playersNum;

                    Node Removed = Current;
                    for (int j = 0; j < 7; j++)
                    {
                        Removed = Removed.Prev;
                    }

                    Removed.Prev.Next = Removed.Next;
                    Removed.Next.Prev = Removed.Prev;
                    Current = Removed.Next;

                    Players[playerIndex].Score += i + Removed.Value;

                }

                //if (i % 100_000 == 0)
                //{
                //    Console.WriteLine(i+" "+ Players.Max(p => p.Score));

                //}
            }
            

            Console.WriteLine(Players.Max(p=>p.Score));

            Console.ReadKey();
        }
        
    }

    class Player
    {
        public long Score { get; set; }
    }

    class Node
    {
        public Node(int value)
        {
            Value = value;
        }

        public int Value { get; set; }
        public Node Prev { get; set; }
        public Node Next { get; set; }
    }
}
