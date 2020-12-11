using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2020
{

  internal class Program
    {
        private static void Main(string[] args)
        {
            char[] tst = new[] {'.', 'L', '#'};
            var solution = new Day11.Solution();
            solution.ExecuteTask();
        }
    }
}
