using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using AdventOfCode2020.Day12;

namespace AdventOfCode2020.Day12
{
    public class Solution:ITask
    {
        public void ExecuteTask()
        {
            
            const string path = @".\Day12\data.txt";
            string[] lines = File.ReadAllLines(path);
            

            SimpleCoordinator coordinator1 = new SimpleCoordinator();
            AdvancedCoordinator coordinator2 = new AdvancedCoordinator(10,1);

            foreach (string line in lines)
            {
                coordinator1.ProcessCommand(line);
                coordinator2.ProcessCommand(line);
            }

            (int x, int y) answer1 = coordinator1.GetShipLocation();
            (int x, int y) answer2 = coordinator2.GetShipLocation();
            Console.Out.WriteLine("Answer 1 = {0}", Math.Abs(answer1.x) + Math.Abs(answer1.y));
            Console.Out.WriteLine("Answer 2 = {0}", Math.Abs(answer2.x)+Math.Abs(answer2.y));
        }

    }

    public class AdvancedCoordinator : ICoordinator
    {
        public AdvancedCoordinator(int x, int y)
        {
            waypoint.dx = x;
            waypoint.dy = y;
        }

        private (int dx, int dy) waypoint;
        private (int x, int y) ShipLocation;

        public (int x, int y) GetShipLocation() => ShipLocation;

        public void ProcessCommand(string command)
        {
            int length = int.Parse(command.Substring(1));
            char commandType = command[0];
            switch (commandType)
            {
                case 'N':
                    waypoint.dy += length;
                    break;
                case 'S':
                    waypoint.dy -= length;
                    break;
                case 'E':
                    waypoint.dx += length;
                    break;
                case 'W':
                    waypoint.dx -= length;
                    break;
                case 'R':
                    RotateWaypoint(length);
                    break;
                case 'L':
                    RotateWaypoint(-length);
                    break;
                case 'F':
                    ShipLocation.x += waypoint.dx * length;
                    ShipLocation.y += waypoint.dy * length;
                    break;
            }
        }

        private void RotateWaypoint(double angle)
        {
            double rad1 = angle * Math.PI / 180.0;
            double dxt1 = waypoint.dx * Math.Cos(rad1) + waypoint.dy * Math.Sin(rad1);
            double dyt1 = -waypoint.dx * Math.Sin(rad1) + waypoint.dy * Math.Cos(rad1);

            waypoint.dx = (int)Math.Round(dxt1);
            waypoint.dy = (int)Math.Round(dyt1);
        }
    }



    public class SimpleCoordinator : ICoordinator
    {
        public SimpleCoordinator()
        {
        }

        public (int x, int y) GetShipLocation()
        {
            int x = handlers['N'] - handlers['S'];
            int y = handlers['E'] - handlers['W'];

            return (x, y);
        }

        private char[] directions = new char[4]
        {
            'E','S','W','N'
        };

        private readonly Dictionary<char,int> handlers = new Dictionary<char, int>()
        {
            {'N',  0},
            {'S',  0},
            {'E',  0},
            {'W',  0},
            {'R',  0},
            {'L',  0},
        };


        public void ProcessCommand(string command)
        {
            int length = int.Parse(command.Substring(1));
            char commandType = command[0];
            if (commandType != 'F')
            {
                handlers[commandType] += length;
            }
            else
            {
                int route = (handlers['R']- handlers['L'])%360;
                route /= 90;
                route = route  == 4 ? 0 : route;
                route = route  < 0 ? 3 + route : route;
                handlers[directions[route]] += length;
            }
        }
    }
}
