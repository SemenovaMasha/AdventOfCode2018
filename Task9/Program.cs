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
            Stopwatch sw = new Stopwatch();
            sw.Start();

            int playersNum = 424;
            int marblesNum = 2300;

            Player[] Players = new Player[playersNum];
            for (int i = 0; i < playersNum; i++)
            {
                Players[i] = new Player();
            }

            List<int> Marbles = new List<int>();
            
            //Dictionary<int, int> Pairs = new Dictionary<int, int>();

            Marbles.Add(0);
            //Pairs.Add(0,0);

            int currentPosition = 0;
            for (int i = 1; i <= marblesNum; i++)
            {
                if (i % 23 != 0||i==0)
                {
                    currentPosition = AddCurrentPosition(Marbles, currentPosition, 2);
                    Marbles.Insert(currentPosition, i);
                    //MoveAllPairsRight(Pairs, currentPosition);
                    //Pairs.Add(i, currentPosition);
                }
                else
                {
                    int playerIndex = i % playersNum;

                    int removeIndex = GetPositionCounterClockwise(Marbles, currentPosition, 7);
                    
                    Console.WriteLine("remove index = {0} - {3}, remove value = {1}, player index = {2}",removeIndex, Marbles[removeIndex],playerIndex, currentPosition - 7 % Marbles.Count);
                    Players[playerIndex].Score += i + Marbles[removeIndex];

                    Marbles.RemoveAt(removeIndex);
                    //RemoveWithPosition(Pairs, removeIndex);
                    //Pairs.Remove(item.Key);
                    //MoveAllPairsLeft(Pairs, removeIndex+1);

                    currentPosition = removeIndex;
                    if (currentPosition >= Marbles.Count)
                        currentPosition = 0;


                }

                //Console.Write(i + ": ");
                //foreach (KeyValuePair<int, int> pair in Pairs)
                //{
                //    //if (j == currentPosition)
                //    //    Console.Write("!");
                //    Console.Write(pair + " ");
                //}

                //Console.WriteLine();

                if (i % 100_000 == 0)
                {
                    sw.Stop();

                    Console.WriteLine(i+": time={0}", sw.Elapsed);
                    sw.Start();
                }
            }

            //foreach (int marble in Marbles)
            //{
            //    Console.Write(marble+" ");
            //}

            Console.WriteLine(Players.Max(p=>p.Score));

            Console.ReadKey();
        }

        static void MoveAllPairsRight(Dictionary<int, int> pairs, int firstIndex)
        {

            foreach (KeyValuePair<int, int> pair in pairs.ToList())
            {
                if (pair.Value >= firstIndex)
                    pairs[pair.Key]++;
            }
        }
        static void MoveAllPairsLeft(Dictionary<int, int> pairs, int firstIndex)
        {

            foreach (KeyValuePair<int, int> pair in pairs.ToList())
            {
                if (pair.Value >= firstIndex)
                    pairs[pair.Key]--;
            }
        }
        static int AddCurrentPosition(List<int> Marbles, int currentPosition, int addNum)
        {
            currentPosition += addNum;
            currentPosition = currentPosition % Marbles.Count;

            //for (int i = 0; i < addNum; i++)
            //{
            //    currentPosition++;
            //    if (currentPosition >= Marbles.Count)
            //        currentPosition = 0;
            //}
            if (currentPosition == 0)
                currentPosition = Marbles.Count;

            return currentPosition;
        }
        static int AddCurrentPosition(Dictionary<int,int> Marbles, int currentPosition, int addNum)
        {
            currentPosition += addNum;
            currentPosition = currentPosition % Marbles.Count;

            //for (int i = 0; i < addNum; i++)
            //{
            //    currentPosition++;
            //    if (currentPosition >= Marbles.Count)
            //        currentPosition = 0;
            //}
            if (currentPosition == 0)
                currentPosition = Marbles.Count;

            return currentPosition;
        }

        static int GetPositionCounterClockwise(List<int> Marbles, int currentPosition, int shiftNum)
        {
            currentPosition -= shiftNum % Marbles.Count;

            if (currentPosition < 0)
                currentPosition = Marbles.Count + currentPosition;

 
            return currentPosition;
        }

        static int GetPositionCounterClockwise(Dictionary<int,int> Marbles, int currentPosition, int shiftNum)
        {
            currentPosition -= shiftNum % Marbles.Count;

            if (currentPosition < 0)
                currentPosition = Marbles.Count + currentPosition;

            //    for (int i = 0; i < shiftNum; i++)
            //{
            //    currentPosition--;
            //    if (currentPosition < 0)
            //        currentPosition = Marbles.Count - 1;
            //}

            return currentPosition;
        }

        static void RemoveWithPosition(Dictionary<int, int> Marbles, int Position)
        {
            var item = Marbles.First(kvp => kvp.Value == Position);

            Marbles.Remove(item.Key);
        }
    }

    class Player
    {
        public int Score { get; set; }
    }
}
