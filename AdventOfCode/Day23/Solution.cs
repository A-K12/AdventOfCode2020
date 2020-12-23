using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day23
{
    public class Node
    {
        public Node(int value)
        {
            this.Value = value;
        }

        public int Value { get;}
        public Node Next { get; set; }
    }

    public class Solution:ITask
    {
        public void ExecuteTask()
        {
            const string path = @".\Day23\data.txt";
            var startCups = File.ReadAllText(path).Select(c => int.Parse(c.ToString())).ToArray();

            List<Node> cups = startCups.Select(i => new Node(i)).ToList();
            for (int i = 0; i < cups.Count; i++)
            {
                cups[i].Next = cups[(i + 1) % cups.Count];
            }

            List<Node> result = MoveCups(cups, 100);

            Console.Out.WriteLine("Answer 1 = ");
            Node one = result.Single(node => node.Value == 1).Next;
            while (one.Value != 1)
            {
                Console.Out.Write(one.Value);
                one = one.Next;
            } 


            cups = new List<Node>();
            int len = 1_000_000;
            foreach (int cup in startCups)
            {
                Node n = new Node(cup);
                cups.Add(n);
            }
            for (int i = startCups.Max()+1; i < len+1; i++)
            {
                Node n = new Node(i);
                cups.Add(n);
            }
            for (int i = 0; i < cups.Count; i++)
            {
                cups[i].Next = cups[(i + 1) % cups.Count];
            }

            result = MoveCups(cups, 10_000_000);

            Console.Out.WriteLine("Answer 2 = ");
            one = result.Single(node => node.Value == 1);
            Console.Out.WriteLine((long)one.Next.Value*(long)one.Next.Next.Value);
        }

        public List<Node> MoveCups(List<Node> list, int times)
        {
            int maxValue = list.Max(node => node.Value);

            Dictionary<int, Node> dict = list.ToDictionary(node => node.Value, node => node);

            Node currentNode = list[0];
            for (int i = 0; i < times; i++)
            {
                int[] removes = new int[3]
                {
                    currentNode.Next.Value,
                    currentNode.Next.Next.Value,
                    currentNode.Next.Next.Next.Value,
                };

                int nextNum = currentNode.Value == 1? dict.Keys.Max():currentNode.Value-1;
                while (removes.Contains(nextNum))
                {
                    nextNum--;
                    if (nextNum <= 0)
                    {
                        nextNum = maxValue;
                    }
                }

                Node lastNum = currentNode.Next.Next.Next.Next;
                Node last = dict[nextNum].Next;
                dict[nextNum].Next = currentNode.Next;
                currentNode.Next = lastNum;
                dict[removes[2]].Next = last;
                currentNode = currentNode.Next;
            }

            return list;
        }
    }
}