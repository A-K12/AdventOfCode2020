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
           // var test = (char?)tst.GetValue(-1);
            var solution = new Day13.Solution();
            solution.ExecuteTask();
        }
    }
}
