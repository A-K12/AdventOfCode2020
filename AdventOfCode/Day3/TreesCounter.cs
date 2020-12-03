﻿using System;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day3
{
    public class TreesCounter : ITask
    {
        private readonly string[] map;
        private readonly int mapLength;
        internal int vPosition, hPosition;

        public TreesCounter(string[] map)
        {
            this.map = map;
            vPosition = 0;
            hPosition = 0;
            mapLength = map.First().Length;
        }

        public TreesCounter(string mapPath) : this(ReadMap(mapPath))
        {
        }

        public void ExecuteTask()
        {
            string path = @"D:\TempProjects\AdventOfCode\AdventOfCode\Day3\data.txt";
            Coordinator[] coordinators =
            {
                new Coordinator(stepsRight: 1, stepsDown: 1),
                new Coordinator(stepsRight: 3, stepsDown: 1),
                new Coordinator(stepsRight: 5, stepsDown: 1),
                new Coordinator(stepsRight: 7, stepsDown: 1),
                new Coordinator(stepsRight: 1, stepsDown: 2)
            };
            TreesCounter counter = new TreesCounter(path);
            int trees = 1;
            foreach (Coordinator coordinator in coordinators) trees *= counter.CountTrees(coordinator);


            Console.Out.WriteLine("trees = {0}", trees);
        }

        private static string[] ReadMap(string mapPath)
        {
            string input;
            using StreamReader sr = new StreamReader(mapPath);
            input = sr.ReadToEnd();

            return input.Split(new[] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries);
        }

        public int CountTrees(ICoordinator coordinator)
        {
            int trees = 0;
            vPosition = 0;
            hPosition = 0;
            do
            {
                coordinator.MakeMove(this);
                int realP = hPosition % mapLength;
                trees += map[vPosition][realP] == '#' ? 1 : 0;
            } while (vPosition < map.Length - 1);

            return trees;
        }
    }
}