using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day15
{
    public class Solution:ITask
    {
        public void ExecuteTask()
        {
            const string path = @".\Day15\data.txt";
            int[] numbers = File.ReadAllText(path).Split(',').Select(int.Parse).ToArray();
                

            Console.Out.WriteLine("Answer 1 = {0}", FindNumberInPosition(numbers, 2020));
            Console.Out.WriteLine("Answer 1 = {0}", FindNumberInPosition(numbers, 30000000));
        }


        public int FindNumberInPosition(int[] startNumbers, int position)
        {
            Dictionary<int, int> map = startNumbers
                .Select((key, value) => (key, value))
                .ToDictionary(arg => arg.key, arg => arg.value);
            int lastNum = 0;
            for (int i = map.Count; i < position - 1; i++)
            {
                int newNum = map.ContainsKey(lastNum) ? i - map[lastNum] : 0;
                map[lastNum] = i;
                lastNum = newNum;
            }

            return lastNum;
        }
    }
}