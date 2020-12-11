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

            int[] differences = new int[3];
            int currentJoltage = 0;
            foreach (int number in numbers)
            {
                int difference =  number-currentJoltage;
                currentJoltage = number;
                differences[difference-1]++;
            }
            differences[2]++;

            Console.Out.WriteLine("Answer 1 = {0}", differences[0]*differences[2]);

            
            long[] map = new long[numbers.Max()+1];
            map[0]=1;
            foreach (int lastNum in numbers)
            {
                int firstNum = lastNum - 3;
                firstNum = firstNum < 0 ? 0 : firstNum;
                map[lastNum] = map[firstNum..lastNum].Sum();
            }

            Console.Out.WriteLine("Answer 2 = {0}", map.Last());
        }



    }
}