using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day5
{
    public class Solution:ITask
    {
        public void ExecuteTask()
        {
            string path = @".\Day05\data.txt";
            string input = File.ReadAllText(path);
            HashSet<int> ids = input.Replace('F', '0')
                .Replace('B', '1')
                .Replace('L', '0')
                .Replace('R', '1')
                .Split(new char[] { '\r', '\n'}, StringSplitOptions.RemoveEmptyEntries)
                .Select(index => Convert.ToInt32(index, 2))
                .ToHashSet();

            Console.Out.WriteLine("Max = {0}", ids.Max());
            Console.Out.WriteLine("Ticket = {0}", ids.Single(t => ids.Contains(t + 2) && !ids.Contains(t + 1)) + 1);
        }
    }
}