using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day1
{
    public static class NumberSearcher
    {

        public static long[] Find2Numbers(long[] numbers, long sum)
        {
            Array.Sort(numbers);

            int first = 0;
            int last = numbers.Length - 1;
            while (first < last)
            {
                long s = numbers[first] + numbers[last];
                if (s == sum) return new[] {numbers[first], numbers[last]};

                if (s < sum)
                    first++;
                else
                    last--;
            }

            return null;
        }

        public static long[] Find3Numbers(long[] numbers, long sum)
        {
            Array.Sort(numbers);

            for (int i = 0; i < numbers.Length - 2; i++)
            {
                int first = i + 1;
                int last = numbers.Length - 1;

                while (first < last)
                {
                    long s = numbers[first] + numbers[last] + numbers[i];
                    if (s == sum)
                    {
                        return new[] {numbers[first], numbers[last], numbers[i]};
                    }

                    if (s > sum)
                        last--;
                    else
                        first++;
                }
            }

            return null;
        }

   

    }
}