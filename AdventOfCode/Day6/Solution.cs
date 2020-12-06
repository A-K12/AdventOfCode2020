using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AdventOfCode;

namespace AdventOfCode.Day6
{
    class Solution:ITask
    {
        public void ExecuteTask()
        {
            const string path = @".\Day6\data.txt";
            string input = File.ReadAllText(path);

            IEnumerable<string[]> answers = input.Split("\r\n\r\n")
                .Select(s => s.Split("\r\n"));


            int firstTask = answers.Sum(strings => strings.SelectMany(s => s).Distinct().Count());

            int secondTask = answers.Sum(strings => strings.Select(s => s.ToCharArray())
                .Aggregate((s, s1) => s.Intersect(s1).ToArray()).Count());


            Console.Out.WriteLine("First task = {0}\nSecond task = {1}", firstTask, secondTask);
        }
    }
}
