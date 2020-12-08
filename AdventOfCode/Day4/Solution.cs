using System;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Day4
{
    public class Solution : ITask
    {
        public void ExecuteTask()
        {
            string input = File.ReadAllText(@".\Day4\data.txt");
            var list = input.Split(new string[] {"\r\n\r\n"}, StringSplitOptions.RemoveEmptyEntries);

            Regex[] regexesPart1 = new Regex[]
            {
                new Regex(@"\bbyr:"),
                new Regex(@"\biyr:"),
                new Regex(@"\beyr:"),
                new Regex(@"\bhgt:"),
                new Regex(@"\bhcl:"),
                new Regex(@"\becl:"),
                new Regex(@"\bpid:"),

            };

            Regex[] regexesPart2 = new Regex[]
            {
                new Regex(@"\bbyr:(19[2-9][0-9]|200[0-2])\b"),
                new Regex(@"\biyr:(201[0-9]|2020)\b"),
                new Regex(@"\beyr:(202[0-9]|2030)\b"),
                new Regex(@"\bhgt:(1[5-8][0-9]cm|19[0-3]cm|59in|6[0-9]in|7[0-6]in)\b"),
                new Regex(@"\bhcl:#([0-9a-f]{6})\b"),
                new Regex(@"\becl:(amb|blu|brn|gry|grn|hzl|oth)\b"),
                new Regex(@"\bpid:[0-9]{9}\b"),

            };

            int validPassports = ValidPassports(list, regexesPart1);
            Console.Out.WriteLine("Answer Part1 = {0}", validPassports);
            validPassports = ValidPassports(list, regexesPart2);
            Console.Out.WriteLine("Answer Part2 = {0}", validPassports);
        }

        public int ValidPassports(string[] passports, Regex[] regexes)
        {
            int validPassports = 0;
            foreach (string passport in passports)
            {
                bool check = true;
                foreach (Regex regex in regexes)
                {
                    MatchCollection match = regex.Matches(passport);
                    if (match.Count != 1)
                    {
                        check = false;
                        break;
                    }
                }

                validPassports += check ? 1 : 0;
            }

            return validPassports;
        }
    }
}