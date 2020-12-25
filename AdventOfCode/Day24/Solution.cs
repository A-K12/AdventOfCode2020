using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;

namespace AdventOfCode2020.Day24
{
    
    public class Solution:ITask
    {
        public void ExecuteTask()
        {
            const string path = @".\Day24\data.txt";
            var coords = File.ReadAllLines(path);

            Dictionary<Vector3, bool> tiles = new Dictionary<Vector3, bool>();
            foreach (string coord in coords)
            {
                int i = 0;
                Vector3 nextTile = Vector3.Zero;
                while (coord.Length > i)
                {
                    int len = (coord[i] == 's' || coord[i] == 'n') ? 2 : 1;
                    string command = coord.Substring(i, len);
                    nextTile += GetOffsets(command);
                    i += len;
                }

                tiles[nextTile] = !tiles.ContainsKey(nextTile) || !tiles[nextTile];
            }

            Console.Out.WriteLine("Answer 1 = {0}", tiles.Values.Count(b => b));

           
            for (int i = 0; i < 100; i++)
            {
                Dictionary<Vector3, int> newDay = new Dictionary<Vector3, int>();

                foreach ((Vector3 key, bool value) in tiles)
                {
                    if (!newDay.ContainsKey(key))
                    {
                        newDay[key] = 0;
                    }
                    foreach (Vector3 v in offsets)
                    {
                        Vector3 adjacent = key + v;
                        if (newDay.ContainsKey(adjacent))
                        {
                            newDay[adjacent] += value ? 1 : 0;
                        }
                        else
                        {
                            newDay[adjacent] = value ? 1 : 0;
                        }
                    }
                }

                Dictionary<Vector3, bool> newMap = new Dictionary<Vector3, bool>();

                foreach ((Vector3 key, int value) in newDay)
                {
                    bool side;
                   
                    if (tiles.GetValueOrDefault(key, false))
                    {
                        side = value != 0 && value <= 2;
                    }
                    else
                    {
                        side = value == 2;
                    }
                    newMap.Add(key, side);
                }

                tiles = newMap;
            }

            Console.Out.WriteLine("Answer 2 = {0}", tiles.Count(pair => pair.Value));
        }

        public Vector3 GetOffsets(string coords) => coords switch
        {
            "e" => offsets[0],
            "se" => offsets[1],
            "sw" => offsets[2],
            "w" => offsets[3],
            "nw" => offsets[4],
            "ne" => offsets[5],
            _ => throw new System.NotImplementedException()
        };

       private Vector3[] offsets = new Vector3[]
        {
            new Vector3(1, 0, -1),
            new Vector3(1, -1, 0),
            new Vector3(0, -1, 1),
            new Vector3(-1, 0, 1),
            new Vector3(-1, 1, 0),
            new Vector3(0, 1, -1),
        };

    }
}