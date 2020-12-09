using System;
using System.IO;
using System.Linq;
using AdventOfCode2020.Day1;

namespace AdventOfCode2020.Day9
{
    public class Solution:ITask
    {
        public void ExecuteTask()
        {
            const string path = @".\Day9\data.txt";
            long[] numbers = File.ReadAllLines(path).Select(s => long.Parse(s)).ToArray();

            int index = FindIndex(25, numbers);

            Console.Out.WriteLine("Answer 1= {0}", numbers[index]);

            long[] result = FindSubArray(numbers, numbers[index]);
            long max = result.Max();
            long min = result.Min();

            Console.Out.WriteLine("Answer 2 = {0}", min + max);
        }

        private int FindIndex(int preamble, long[] numbers)
        {
            int index = preamble;
            while (index < numbers.Length)
            {
                int i = index - preamble;
                int j = index;
                long[] rez = NumberSearcher.Find2Numbers(numbers[i..j], numbers[index]);
                if (rez == null)
                {
                    break;
                }
                index++;
            }

            return index;
        }

        private long[] FindSubArray(long[] numbers, long number)
        {
            long sum = 0;
            int index = Array.IndexOf(numbers, number);
            int firstIndex = 0;
            int lastIndex = 0;
            while (lastIndex < numbers.Length)
            {
                if (sum == number)
                {
                    break;
                }
                if (lastIndex == index)
                {
                    sum = 0;
                    lastIndex++;
                    firstIndex = lastIndex;
                    continue;
                }
                if (sum < number)
                {
                    sum += numbers[lastIndex];
                    lastIndex++;
                }
                if (sum > number)
                {
                    sum -= numbers[firstIndex];
                    firstIndex++;
                }
            }

            return numbers[firstIndex..lastIndex];
        }
    }
}