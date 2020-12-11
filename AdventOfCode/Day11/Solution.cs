using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace AdventOfCode2020.Day11
{
    public class Solution : ITask
    {
        public void ExecuteTask()
        {
            const string path = @".\Day11\data.txt";
            char[][] oldLayout = File.ReadAllLines(path).Select(s => s.ToCharArray()).ToArray();
            char[][] newLayout = new char[oldLayout.Length][];

            int x = 0;
            int y = 0;
            int max_x = oldLayout.Length;
            int max_y = oldLayout.First().Length;
           


            int column = oldLayout.First().Length;
            bool wasChange = true;
            while(wasChange)
            {
                wasChange = false;
                for (int i = 0; i < oldLayout.Length; i++)
                {
                    newLayout[i] = new char[column];
                    for (int j = 0; j < column; j++)
                    {
                        if(oldLayout[i][j]=='.')
                        {
                            newLayout[i][j] = '.';
                            continue;
                        }
                        char[] adjacentElements = GetNearestElements(i, j, oldLayout);
                        int occupiedSeat = adjacentElements.Count(c => c == '#');
                        if (occupiedSeat == 0&& oldLayout[i][j]=='L')
                        {
                            newLayout[i][j] = '#';
                            wasChange = true;
                        }
                        else if (occupiedSeat >= 5 && oldLayout[i][j] == '#')
                        {
                            newLayout[i][j] = 'L';
                            wasChange = true;
                        }
                        else
                        {
                            newLayout[i][j] = oldLayout[i][j];
                        }
                    }
                }
                string newLine1 = newLayout.Select(chars => new string(chars)).Aggregate((a, b) => a + '\n' + b);
                Console.Out.WriteLine(newLine1);
                oldLayout = newLayout;
                newLayout = new char[oldLayout.Length][];

            }
            
            int count = oldLayout.Sum(c => c.Count(s => s=='#'));
            Console.Out.WriteLine("count = {0}", count);
        }

        private void WriteToConsole(char[][] array)
        {
            string pastLine = array.Select(chars => new string(chars)).Aggregate((a, b) => a + '\n' + b);
            Console.Out.WriteLine("-----------------\n");
        }

        public static T[] GetAdjacentElements<T>(int x, int y, T[][] array)
        {
            List<T> result = new List<T>(8);
            for (int dx = (x > 0 ? -1 : 0); dx <= (x < array.Length-1 ? 1 : 0); ++dx)
            {
                for (int dy = (y > 0 ? -1 : 0); dy <= (y < array.First().Length-1? 1 : 0); ++dy)
                {
                    if (dx != 0 || dy != 0)
                    {
                        result.Add(array[x + dx][y + dy]);
                    }
                }
            }

            return result.ToArray();
        }

        public char[] GetNearestElements(int x, int y, char[][] array)
        {
            int[][] directions = new int[8][]
            {
                new int[]{1,1}, 
                new int[]{1,0},
                new int[]{0,1},
                new int[]{-1,0},
                new int[]{0,-1},
                new int[]{-1,-1},
                new int[]{-1,1},
                new int[]{1,-1},
            };
            List<char> result = new List<char>(8);
            foreach(int[] direction in directions)
            {
                char symbol = GetNearestElement(x, y, array, direction);
                if(symbol==default) continue;
                result.Add(symbol);
            }

            return result.ToArray();
        }

        public char GetNearestElement(int x, int y, char[][] array, int[] direction)
        {
            char symbol = default;
            while (true)
            {
                x += direction[0];
                y += direction[1];

               if (x < 0 || y < 0 || x >= array.Length || y >= array.First().Length) break;
               symbol = array[x][y];
               if (symbol == 'L' || symbol == '#') break;
            }

            return symbol;
        }


    }
}