using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Task8
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileString = File.ReadAllText("../../input.txt");

            int[] numbers = fileString.Split(' ').Select(s => Convert.ToInt32(s)).ToArray();

            List<Node> allNodes = new List<Node>();
            Stack<Node> nodes = new Stack<Node>();

            int currentNumberIndex = 0;

            int nodesNumber = numbers[currentNumberIndex++];
            int metadataSize = numbers[currentNumberIndex++];

            Node root = new Node {NodesLeft = nodesNumber, MetadataLeft = metadataSize, Metadata = new List<int>(), ChildNodes = new List<Node>() };
            nodes.Push(root);
            allNodes.Add(root);

            while (nodes.Count > 0)
            {
                Node current = nodes.Peek();
                if (current.NodesLeft > 0)
                {
                    current.NodesLeft--;
                    
                    Node newNode = new Node
                    {
                        NodesLeft = numbers[currentNumberIndex++], MetadataLeft = numbers[currentNumberIndex++],
                        Metadata = new List<int>(),ChildNodes = new List<Node>()
                        
                    };

                    current.ChildNodes.Add(newNode);
                    nodes.Push(newNode);
                    allNodes.Add(newNode);
                }
                else if (current.NodesLeft == 0 && current.MetadataLeft > 0)
                {
                    current.MetadataLeft--;
                    current.Metadata.Add(numbers[currentNumberIndex++]);
                }
                else
                {
                    nodes.Pop();
                }
            }

            Console.WriteLine(root.GetValue());

            Console.ReadKey();
        }

        class Node
        {
            public int NodesLeft { get; set; }
            public List<int> Metadata { get; set; }
            public int MetadataLeft { get; set; }

            public int GetSum()
            {
                return Metadata.Sum();
            }

            public List<Node> ChildNodes { get; set; }

            public int Value
            {
                get { return GetValue();} }

            public int GetValue()
            {
                if (ChildNodes.Count == 0)
                    return GetSum();

                int sum = 0;
                foreach (int meta in Metadata)
                {
                    if (meta != 0 && meta-1 < ChildNodes.Count)
                        sum += ChildNodes[meta - 1].GetValue();
                }

                return sum;
            }
        }
    }
}
