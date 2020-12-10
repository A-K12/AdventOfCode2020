using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day2
{
    internal class PasswordValidator:ITask
    {
        public void ExecuteTask()
        {
            string path = @".\Day02\data.txt";
            string[] lines = File.ReadAllLines(path);

            int count = lines.Count(PasswordValidator.ValidateV1);

            Console.Out.WriteLine("Answer 1 = {0}", count);
            
            count = lines.Count(PasswordValidator.ValidateV2);

            Console.Out.WriteLine("Answer 2 = {0}", count);
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

    }
}