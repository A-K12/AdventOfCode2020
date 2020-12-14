using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day14
{
    public class Solution : ITask
    {
        public void ExecuteTask()
        {
            int maskSize = 36;
            const string path = @".\Day14\data.txt";
            string[] lines = File.ReadAllLines(path);
            Dictionary<long, long> memory = new Dictionary<long, long>();
            long[] mask = new long[2];
            foreach (string line in lines)
            {
                if (line.Contains("mask"))
                {
                    mask = line.Substring(7).Select((c, i) => (c, i))
                        .Aggregate(new long[2], (result, tuple) =>
                        {
                            long shift = (long) 1 << maskSize - tuple.i - 1;
                            if (tuple.c == '1')
                                result[1] |= shift;
                            else if(tuple.c == '0')
                                result[0] |= ~shift;
                            return result;
                        });
                    continue;
                }
                long[] numbers = line.Split(new[] {"mem[", "] = "}, StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => long.Parse(s)).ToArray();
                numbers[1] |= mask[1];
                numbers[1] &= mask[0];

                memory[numbers[0]] = numbers[1];
            }

            Console.Out.WriteLine("Answer 1 = {0}", memory.Values.Sum());

            memory.Clear();
            long oneMask = 0;
            int[] xIndexes = default;

            foreach (string line in lines)
            {
                if (line.Contains("mask"))
                {
                    var maskInfo = line.Substring(7).Select((c, i1) => (c, maskSize - i1 - 1));
                    oneMask = maskInfo.Where(tuple => tuple.c == '1').Select(tuple => tuple.Item2)
                        .Aggregate((long) 0, (result, index) => result |= ((long) 1 << index));
                    xIndexes = maskInfo.Where(tuple => tuple.c == 'X').Select(tuple => tuple.Item2).ToArray();
                    continue;
                }
                long[] numbers = line.Split(new[] {"mem[", "] = "}, StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => long.Parse(s)).ToArray();
                numbers[0] |= oneMask;

                IEnumerable<long> allNumbers = FindAllNumbers(numbers[0], xIndexes, 0);

                foreach (long number in allNumbers) memory[number] = numbers[1];
            }

            Console.Out.WriteLine("Answer 2 = {0}", memory.Values.Sum());
        }

        private IEnumerable<long> FindAllNumbers(long number, int[] indexesX, int currentIndex)
        {
            if (currentIndex >= indexesX.Length) return new[] {number};
            List<long> result = new List<long>();
            long shift = ((long) 1 << indexesX[currentIndex]);
            currentIndex++;
            result.AddRange(FindAllNumbers(number | shift, indexesX, currentIndex));
            result.AddRange(FindAllNumbers(number & ~shift, indexesX, currentIndex));
            return result;
        }
    }
}