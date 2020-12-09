using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Day4
{
    public class Solution : ITask
    {
        public void ExecuteTask()
        {
            string[] lines = File.ReadAllLines(@".\Day4\data.txt");

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



            int validPassports = CountValidPassports(lines, regexesPart1);
            Console.Out.WriteLine("Answer Part1 = {0}", validPassports);
            validPassports = CountValidPassports(lines, regexesPart2);
            Console.Out.WriteLine("Answer Part2 = {0}", validPassports);
        }


        public int CountValidPassports(string[] lines, Regex[] regexes) => 
            lines.Count(line => regexes.All(regex => regex.IsMatch(line)));

    }
}