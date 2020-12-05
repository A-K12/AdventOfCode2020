using System;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day1
{
    public class NumberSearcher:ITask
    {
        public void ExecuteTask()
        {
            string path = @".Day1\nums.txt";
            int[] numbers = NumberSearcher.ReadNumbers(path);
            int[] result = NumberSearcher.Find3Numbers(numbers, 2020);

            Console.Out.WriteLine("result = {0}", result.Aggregate((a, b) => a * b));
        }

        public static int[] Find2Numbers(int[] nums, int sum)
        {
            Array.Sort(nums);

            int first = 0;
            int last = nums.Length - 1;
            while (first < last)
            {
                int s = nums[first] + nums[last];
                if (s == sum) return new[] {nums[first], nums[last]};

                if (s < sum)
                    first++;
                else
                    last--;
            }

            throw new Exception("Numbers not found");
        }

        public static int[] Find3Numbers(int[] numbers, int sum)
        {
            Array.Sort(numbers);


            for (int i = 0; i < numbers.Length - 2; i++)
            {
                int first = i + 1;
                int last = numbers.Length - 1;

                while (first < last)
                {
                    int s = numbers[first] + numbers[last] + numbers[i];
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

            throw new Exception("Numbers not found");
        }

        public static int[] ReadNumbers(string path)
        {
            string input;
            using (StreamReader sr = new StreamReader(path))
            {
                input = sr.ReadToEnd();
            }

            string[] lines = input.Split(new[] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries);

            return lines.Select(int.Parse).ToArray();
        }

    }
}