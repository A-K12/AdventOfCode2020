using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day10
{
    public class Solution:ITask
    {
        public void ExecuteTask()
        {
            const string path = @".\Day10\data.txt";
            int[] numbers = File.ReadAllLines(path).Select(s => int.Parse(s)).ToArray();

            Array.Sort(numbers);
            int startJoint = 0;
            int[] differents = new int[4];

            for (int i = 0; i < numbers.Length; i++)
            {
                int dif =  i-startJoint;
                differents[dif]++;
                startJoint = i;
            }

            differents[3]++;
            Console.Out.WriteLine("Answer 1 = {0}", differents[1]*differents[3]);

            
            long[] map = new long[numbers.Max()+1];
            map[0]++;
            foreach (int number in numbers)
            {
                int first = number - 3;
                first = first < 0 ? 0 : first;
                map[number] = map[first..number].Sum();
            }

            Console.Out.WriteLine("Answer 2 = {0}", map.Last());
        }
    }
}