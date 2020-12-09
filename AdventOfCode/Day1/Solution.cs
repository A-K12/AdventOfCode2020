using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day1
{
    public class Solution:ITask
    {
        public void ExecuteTask()
        {
            string path = @".\Day1\nums.txt";
            long[] numbers = File.ReadAllLines(path).Select(long.Parse).ToArray();
            long findNumber = 2020;
            long[] result = NumberSearcher.Find2Numbers(numbers, findNumber);

            Console.Out.WriteLine("Answer 1 = {0}", result.Aggregate((a, b) => a * b));

            result = NumberSearcher.Find3Numbers(numbers, findNumber);

            Console.Out.WriteLine("Answer 2 = {0}", result.Aggregate((a, b) => a * b));

        }
    }
}