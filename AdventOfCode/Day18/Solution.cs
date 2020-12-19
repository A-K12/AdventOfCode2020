using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;

namespace AdventOfCode2020.Day18
{
    public class Solution:ITask
    {
        public void ExecuteTask()
        {
            const string path = @".\Day18\data.txt";
            string[] lines = File.ReadAllLines(path);

            int SamePriority(char c) => 1;

            int DifferentPriority(char c) => c switch
            {
                '+' => 2,
                '*' => 1,
                _   => 0,
            };

            var result = lines.Select(s => CovertToPostfixNotation(s, SamePriority));
            long answer1 = result.Select(SolvePostfixNotation).Sum();
            result = lines.Select(s => CovertToPostfixNotation(s, DifferentPriority));
            long answer2 = result.Select(SolvePostfixNotation).Sum();

            Console.Out.WriteLine("Answer1 = {0}\nAnswer 2 = {1}", answer1, answer2);
        }

        private readonly HashSet<char> operators = new HashSet<char>{'+','*','(',')'};

        private long SolvePostfixNotation(IEnumerable<char> input)
        {
            Stack<long> answer = new Stack<long>();
            foreach (char c in input)
            {
                if (operators.Contains(c))
                {
                    long p1 = answer.Pop(),
                        p2 = answer.Pop();
                    answer.Push(c.Equals('+')?p1 + p2:p1 * p2);
                }
                else
                {
                    answer.Push((int)char.GetNumericValue(c));
                }
            }

            return answer.Peek();
        }

        private char[] CovertToPostfixNotation(string line, Func<char,int> priorityOf)
        {
            List<char> result = new List<char>();
            Stack<char> stack = new Stack<char>();
            foreach (char c in line.Reverse().Where(c => !char.IsSeparator(c)))
            {
                if (operators.Contains(c))
                {
                    if (stack.Count>0&&!c.Equals(')'))
                    {
                        if (c.Equals('('))
                        {
                            char op = stack.Pop();
                            while (op != ')')
                            {
                                result.Add(op);
                                op = stack.Pop();
                            }

                        }
                        else if(priorityOf(c)>priorityOf(stack.Peek()))
                        {
                            stack.Push(c);
                        }
                        else
                        {
                            while (stack.Count > 0 && priorityOf(c) < priorityOf(stack.Peek()))
                                result.Add(stack.Pop());
                            stack.Push(c);
                        }
                    }
                    else
                    {
                        stack.Push(c);
                    }
                }
                else
                {
                    result.Add(c);
                }
            }
            if (stack.Count > 0)
            {
                result.AddRange(stack);
            }

            return result.ToArray();
        }
    }
}