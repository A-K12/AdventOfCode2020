using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2020
{

  internal class Program
    {
        private static void Main(string[] args)
        {
            //nums[0] | ((long)1 << bitIndex) : nums[0] & ~((long)1 << bitIndex);

            // var test = (char?)tst.GetValue(-1);
            var solution = new Day14.Solution();
            solution.ExecuteTask();
        }
    }
}
