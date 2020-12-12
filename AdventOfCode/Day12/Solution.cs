using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2020.Day12
{
    class Solution:ITask
    {
        public void ExecuteTask()
        {
            int[][] directions = new int[4][]
            {
                //север юг запад восток
                new int[]{1,0,0},
                new int[]{0,-1},
                new int[]{-1,0},
                new int[]{0, 1},
            };
            int dx = 10;
            int dy = 1;
            const string path = @".\Day12\data.txt";
            string[] lines = File.ReadAllLines(path);
            int x = 0;
            int y = 0;
            int direction = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                char command = lines[i][0];
                int length = int.Parse(lines[i].Substring(1));
                switch (command)
                {
                    case 'N':
                        dy += length;
                        break;
                    case 'S':
                        dy -= length;
                        break;
                    case 'E':
                        dx += length;
                        break;
                    case 'W':
                        dx -= length;
                        break;
                    case 'R':
                        double rad1 = length * Math.PI / 180.0;
                        double dxt1 = dx * Math.Cos(rad1) + dy * Math.Sin(rad1);
                        double dyt1 = -dx * Math.Sin(rad1) + dy * Math.Cos(rad1);

                        dx = (int)Math.Round(dxt1);
                        dy = (int)Math.Round(dyt1);

                        break;
                    case 'L':
                    
                        double rad = length * Math.PI / 180.0;
                        double dxt = dx * Math.Cos(rad) - dy * Math.Sin(rad);
                        double dyt = dx * Math.Sin(rad) + dy * Math.Cos(rad);

                        dx = (int)Math.Round(dxt);
                        dy = (int)Math.Round(dyt);

                        break;
                    case 'F':
                        x += dx * length;
                        y += dy * length;
                        break;
                }
            }

            Console.Out.WriteLine("x = {0}", Math.Abs(x)+Math.Abs(y));
        }

        public void FirstTask()
        {
            int[][] directions = new int[4][]
            {
                new int[]{1,0},
                new int[]{0,-1},
                new int[]{-1,0},
                new int[]{0, 1},
            };
            const string path = @".\Day12\data.txt";
            string[] lines = File.ReadAllLines(path);
            int x = 0;
            int y = 0;
            int direction = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                char command = lines[i][0];
                int length = int.Parse(lines[i].Substring(1));
                switch (command)
                {
                    case 'N':
                        y += length;
                        break;
                    case 'S':
                        y -= length;
                        break;
                    case 'E':
                        x += length;
                        break;
                    case 'W':
                        x -= length;
                        break;
                    case 'R':
                        int count = length / 90;
                        for (int j = 0; j < count; j++)
                        {
                            direction = direction == 3 ? 0 : direction + 1;
                        }
                        break;
                    case 'L':
                        int numLeft = length / 90;
                        for (int j = 0; j < numLeft; j++)
                        {
                            direction = direction == 0 ? 3 : direction - 1;
                        }
                        break;
                    case 'F':
                        x += directions[direction][0] * length;
                        y += directions[direction][1] * length;
                        break;
                }
            }

            Console.Out.WriteLine("x = {0}", Math.Abs(x) + Math.Abs(y));
        }
    }
}
