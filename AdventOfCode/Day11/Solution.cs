using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day11
{
    public class Solution : ITask
    {
        public void ExecuteTask()
        {
            const string path = @".\Day11\data.txt";
            char[][] oldLayout = File.ReadAllLines(path).Select(s => s.ToCharArray()).ToArray();
            char[][] newLayout = new char[oldLayout.Length][];
            int column = oldLayout.First().Length;
            bool wasChange = true;
            while(wasChange)
            {
                wasChange = false;
                string pastLine = oldLayout.Select(chars => new string(chars)).Aggregate((a, b) => a + '\n' + b);
                Console.Out.WriteLine("-----------------\n\n");
                for (int i = 0; i < oldLayout.Length; i++)
                {
                    newLayout[i] = new char[column];
                    for (int j = 0; j < column; j++)
                    {
                        switch (oldLayout[i][j])
                        {
                            case 'L':
                                newLayout[i][j] = FirstRuleTask1(i, j, oldLayout) ? '#' : 'L';
                                wasChange = true;
                                break;
                            case '#':
                                newLayout[i][j] = SecondRuleTask1(i, j, oldLayout) ? 'L' : '#';
                                wasChange = true;
                                break;
                            default:
                                newLayout[i][j] = '.';
                                break;
                        }
                    }
                }
                string newLine1 = newLayout.Select(chars => new string(chars)).Aggregate((a, b) => a + '\n' + b);
                Console.Out.WriteLine(newLine1);
                oldLayout = newLayout;
                newLayout = new char[oldLayout.Length][];

            }
            string newLine = oldLayout.Select(chars => new string(chars)).Aggregate((a, b) => a + '\n' + b);
            int count = newLine.Count(c => c == '#');
            Console.Out.WriteLine("count = {0}", count);
        }

        public bool FirstRuleTask1(int n, int m, char[][] lines)
        {
            var left = new char?[]
            {
                GetStatus(n - 1, m, lines),
                GetStatus(n - 1, m - 1, lines),
                GetStatus(n - 1, m + 1, lines),
                GetStatus(n + 1, m + 1, lines),
                GetStatus(n + 1, m - 1, lines),
                GetStatus(n + 1, m, lines),
                GetStatus(n, m - 1, lines),
                GetStatus(n, m + 1, lines),
            };

            return left.Count(c => c == '#')>=4;

        }
        
        public bool SecondRuleTask1(int n, int m, char[][] lines)
        {
            var left = new char?[]
            {
                GetStatus1(n, m, lines, arrou.down),
                GetStatus1(n, m, lines,arrou.downLeft),
                GetStatus1(n, m, lines,arrou.downRight),
                GetStatus1(n, m, lines, arrou.left),
                GetStatus1(n, m, lines, arrou.right),
                GetStatus1(n, m, lines, arrou.up),
                GetStatus1(n, m, lines, arrou.upLeft),
                GetStatus1(n, m, lines, arrou.upRight),
            };

            return left.Count(c => c == '#')>=5;

        }


        public bool IsFreeSpace(int n, int m, char[][] lines)
        {
            var left = new char?[]
            {
                GetStatus(n - 1, m, lines),
                GetStatus(n - 1, m - 1, lines),
                GetStatus(n, m - 1, lines),
                GetStatus(n + 1, m, lines),
                GetStatus(n + 1, m + 1, lines),
                GetStatus(n, m + 1, lines),
                GetStatus(n - 1, m + 1, lines),
                GetStatus(n + 1, m - 1, lines),
            };

            
            return left.All(c => c != '#');
        }

        public bool FirstRuleTask2(int n, int m, char[][] lines)
        {
            var left = new char?[]
            {
                GetStatus1(n, m, lines, arrou.down),
                GetStatus1(n, m, lines,arrou.downLeft),
                GetStatus1(n, m, lines,arrou.downRight),
                GetStatus1(n, m, lines, arrou.left),
                GetStatus1(n, m, lines, arrou.right),
                GetStatus1(n, m, lines, arrou.up),
                GetStatus1(n, m, lines, arrou.upLeft),
                GetStatus1(n, m, lines, arrou.upRight),
            };

            
            return left.All(c => c != '#');
        }


        public char? GetStatus(int n, int m, char[][] lines)
        {
            if (n < 0 || m < 0 || n >= lines.Length || m >= lines.First().Length)
            {
                return null;
            }

            return lines[n][m];
        }

        public char? GetStatus1(int n, int m, char[][] lines, arrou arrou)
        {
            int m1 = m;
            int n1 = n;
            while (n1 >= 0 && m1 >= 0 && n1 < lines.Length && m1 < lines.First().Length)
            {
                switch (arrou)
                {
                    case arrou.left: 
                        m1--;
                        break;
                    case arrou.right:
                        m1++;
                        break;
                    case arrou.up:
                        n1++;
                        break;
                    case arrou.down:
                        n1--;
                        break;
                    case arrou.downLeft:
                        n1--;
                        m1--;
                        break;
                    case arrou.upLeft:
                        m1--;
                        n1++;
                        break;
                    case arrou.downRight:
                        n1--;
                        m1++;
                        break;
                    case arrou.upRight:
                        n1++;
                        m1++;
                        break;
                }

                char? tempLetter = GetStatus(n1, m1, lines);
                if (tempLetter != '.' && tempLetter != null)
                {
                    return tempLetter;
                }
            }

            return null;
        }

        
    }

    public enum arrou
    {
        up,
        down,
        left,
        right,
        upRight,
        downRight,
        upLeft,
        downLeft
    }
}