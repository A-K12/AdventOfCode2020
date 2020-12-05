using System;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day2
{
    internal class PasswordValidator:ITask
    {
        public void ExecuteTask()
        {
            string path = @".\Day2\data.txt";
            string[] lines = PasswordValidator.ReadLines(path);
            int count = 0;
            foreach (string line in lines)
            {
                count += PasswordValidator.ValidateV2(line) ? 1 : 0;
            }

            Console.Out.WriteLine("count = {0}", count);
        }

        public static bool IsValid(int min, int max, char letter, string password)
        {
            int count = password.Count(c => c == letter);

            return min <= count && max >= count;
        }

        public static bool ValidateV1(string line)
        {
            string[] lines = line.Split(new[] {'-', ':', ' '}, StringSplitOptions.RemoveEmptyEntries);


            int min = int.Parse(lines[0]);
            int max = int.Parse(lines[1]);

            return IsValid(min, max, lines[2].First(), lines[3]);
        }

        public static bool ValidateV2(string line)
        {
            string[] lines = line.Split(new[] {'-', ':', ' '}, StringSplitOptions.RemoveEmptyEntries);

            int first = int.Parse(lines[0]) - 1;
            int second = int.Parse(lines[1]) - 1;
            char letter = lines[2].First();
            string password = lines[3];

            return password[first] == letter != (password[second] == letter);
        }

        public static string[] ReadLines(string path)
        {
            using StreamReader sr = new StreamReader(path);
            string input = sr.ReadToEnd();
            string[] lines = input.Split(new[] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries);

            return lines;
        }
    }
}