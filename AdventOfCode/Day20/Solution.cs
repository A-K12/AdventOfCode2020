using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2020.Day20
{
    public class Solution:ITask
    {
        public void ExecuteTask()
        {
            const string path = @".\Day20\data.txt";
            string[] tiles = File.ReadAllText(path).Split("\r\n\r\n");

            Dictionary<int, string> nums = new Dictionary<int, string>();

            Dictionary<string, string> adjacentTiles = new Dictionary<string, string>(tiles.Length*4);
            Dictionary<string, int> tiles1 = new Dictionary<string, int>(tiles.Length);
            
            foreach (string tile in tiles)
            {
                string[] parsedTile = tile.Split("\r\n");
                string tileName = parsedTile[0].Substring(5, 4);
                string up = parsedTile[1];
                tiles1[up]++;
                tiles1[new string(up.Reverse().ToArray())]++;
                adjacentTiles[up] = tileName;
                adjacentTiles[new string(up.Reverse().ToArray())] = tileName;
                string down = parsedTile.Last();
                tiles1[down]++;
                tiles1[new string(down.Reverse().ToArray())]++;
                adjacentTiles[down] = tileName;
                adjacentTiles[new string(down.Reverse().ToArray())] = tileName;

                StringBuilder leftSide = new StringBuilder(), 
                    rightSide = new StringBuilder();
                for (int i = 1; i < parsedTile.Length; i++)
                {
                    leftSide.Append(parsedTile[i][0]);
                    rightSide.Append(parsedTile[i].Last());
                }

                int upf = Convert.ToInt32(up, 2);
                string left = leftSide.ToString();
                string right = rightSide.ToString();
                tiles1[left]++;
                tiles1[new string(left.Reverse().ToArray())]++;
                adjacentTiles[left] = tileName;
                adjacentTiles[new string(left.Reverse().ToArray())] = tileName;
                tiles1[right]++;
                tiles1[new string(right.Reverse().ToArray())]++;
                adjacentTiles[right] = tileName;
                adjacentTiles[new string(right.Reverse().ToArray())] = tileName;
            }

            var only = tiles1.Where((pair, i) => pair.Value == 1);


            //var result = adjacentTiles.Where(pair => pair.Value == 2).Select(pair => int.Parse(pair.Key))
                //.Aggregate<int,long>(1, (l, i) => l*=i);

            Console.Out.WriteLine("result = {0}", "");
        }
    }
}