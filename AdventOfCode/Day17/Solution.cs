using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Day17
{

    public class Solution : ITask
    {
        private IEnumerable<Cell> Offsets()
        {
            for (int i = 0; i < 26; i++)
            {
                yield return new Cell(new int[3]);
            }
        }

        public void ExecuteTask()
        {
            const string path = @".\Day17\data.txt";
            string[] lines = File.ReadAllLines(path);
            HashSet<Cell> activeCells3d = new HashSet<Cell>();
            HashSet<Cell> activeCells4d = new HashSet<Cell>();

            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines.First().Length; x++)
                {
                    if (!lines[y][x].Equals('#')) continue;
                    activeCells3d.Add(new Cell(x, y, 0));
                    activeCells4d.Add(new Cell(x, y, 0, 0));
                }
            }

            const int steps = 6;

            int answer1 = Solve(activeCells3d,steps);
            int answer2 = Solve(activeCells4d,steps);
            Console.Out.WriteLine("Answer 1 = {0}\nAnswer 1 = {1}", answer1, answer2);
        }

        public int Solve(HashSet<Cell> activeCells, int steps)
        {
            for (int i = 0; i < steps; i++)
            {
                activeCells = MakeStep(activeCells);
            }

            return activeCells.Count;
        }


        private HashSet<Cell> MakeStep(HashSet<Cell> activeCells)
        {
            HashSet<Cell> activeCells2 = activeCells.ToHashSet();

            List<int[]> offsets = GetOffsets(activeCells.First().dimension);

            HashSet<Cell> allCells = activeCells.Concat(activeCells.SelectMany(c => 
                offsets.Select(c.Move))).ToHashSet();

            foreach (Cell cell in allCells)
            {
                int active = offsets.Select(cell.Move)
                    .Count(activeCells.Contains);
                if (activeCells.Contains(cell))
                {
                    if (active < 2 || active > 3)
                    {
                        activeCells2.Remove(cell);
                    }
                }
                else
                {
                    if (active == 3)
                    {
                        activeCells2.Add(cell);
                    }
                }
            }

            return activeCells2;
        }

        private List<int[]> GetOffsets(int dimension)
        {
            List<int[]> result = new List<int[]>();
            int[] offset = Enumerable.Repeat(0, dimension).ToArray();
            GenerateOffset(result, offset, 0);

            return result;
        }

        private void GenerateOffset(List<int[]> offsets, int[] offset, int dim)
        {
            if (dim == offset.Length)
            {
                if (offset.Any(i => i != 0))
                {
                    offsets.Add(offset);
                }
                return;
            }

            for (int i = -1; i < 2; i++)
            {
                offset[dim] = i;
                GenerateOffset(offsets, (int[])offset.Clone(), dim+1);
            }
        }
    }

   

}