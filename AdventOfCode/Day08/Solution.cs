using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace AdventOfCode2020.Day8
{
    class Solution:ITask
    {
        public void ExecuteTask()
        {
            const string path = @".\Day08\data.txt";
            string[] lines = File.ReadAllLines(path);
            HashSet<int> check = new HashSet<int>();

            string[][] commands = lines.Select(s => s.Split(' ')).ToArray();

            int answer1 = ExecuteCommands(commands);

            Console.Out.WriteLine("Answer 1 = {0}", answer1);        


            List<int>[] executionMap = GetExecutionMap(commands);

            HashSet<int> executionCommands = GetHashSet(commands.Length - 1, executionMap);

            int index = 0;
            int acc = 0;
            bool findWrongLine = false;
            while (index < commands.Length)
            {
                string command = commands[index][0];
                char sign = commands[index][1][0];
                int num = int.Parse(commands[index][1].Substring(1));
                int tempIndex = 0;
                switch (command)
                {
                    case "nop":
                        tempIndex = sign == '+' ? index + num : index - num;
                        index++;
                        break;
                    case "acc":
                        acc += sign == '+' ? num : -num;
                        index++;
                        break;
                    case "jmp":
                        tempIndex = index + 1;
                        index += sign == '+' ? num : -num;
                        break;
                }
                if (executionCommands.Contains(tempIndex)&&!findWrongLine)
                {
                    index = tempIndex;
                    findWrongLine = true;
                }
            }


            Console.Out.WriteLine("Answer 2 = {0}", acc);
        }

        

        private List<int>[] GetExecutionMap(string[][] commands)
        {
            List<int>[] mask = new List<int>[commands.Length];
            for (int i = 0; i < mask.Length; i++)
            {
                mask[i] = new List<int>();
            }


            for (int i = 0; i < commands.Length; i++)
            {
                string command = commands[i][0];
                char character = commands[i][1][0];
                int num = int.Parse(commands[i][1].Substring(1));
                int index = command switch
                {
                    "nop" => i + 1,
                    "acc" => i + 1,
                    "jmp" => character == '+' ? i + num : i - num,
                    _ => 0
                };
                if (mask.Length > index)
                {
                    mask[index].Add(i);
                }
            }

            return mask;
        }

        private HashSet<int> GetHashSet(int index, List<int>[] mask)
        {
           HashSet<int> rez = new HashSet<int>();
           Stack<int> rez2 = new Stack<int>();
           
           rez2.Push(index);
           while (rez2.Count != 0)
           {
               
               var inde = rez2.Pop();
               if (rez.Contains(inde))
               {
                   break;
               }
               rez.Add(inde);
               var indexes = mask[inde];
               foreach (int i in indexes)
               {
                   rez2.Push(i);
               }
           }
           return rez;
        }


        public int ExecuteCommands(string[][] commands)
        {
            int index = 0;
            int acc = 0;
            HashSet<int> executedCommands = new HashSet<int>();
            while (!executedCommands.Contains(index))
            {
                string command = commands[index][0];
                char sign = commands[index][1][0];
                int num = int.Parse(commands[index][1].Substring(1));
                executedCommands.Add(index);
                
                switch (command)
                {
                    case "nop":
                        index++;
                        break;
                    case "acc":
                        acc += sign  == '+' ? num : -num;
                        index++;
                        break;
                    case "jmp":
                        index += sign  == '+' ? num : -num;
                        break;
                }

            }

            return acc;
        }
    }
}
