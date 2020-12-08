using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;


namespace AdventOfCode2020.Day7
{
    public class Solution:ITask
    {
        public void ExecuteTask()
        {
            const string path = @".\Day7\data.txt";
            string[] lines=File.ReadAllLines(path);

            Regex regex = new Regex(@"(?:(\w+ \w+) bags contain(?: ([0-9]+) (\w+ \w+) bag(?:s)?[,.]| no other bags.)+)");

            Dictionary<string, (string, int)[]> bags = new Dictionary<string, (string, int)[]>(lines.Length);

            foreach (string line in lines)
            {
                Match bagInfo = regex.Match(line);
                string bag = bagInfo.Groups[1].Value; 

                int numberBags = bagInfo.Groups[2].Captures.Count;
                (string, int)[] inBag = new (string, int)[numberBags];
                for (int i = 0; i < numberBags; i++)
                {
                    string nameBag = bagInfo.Groups[3].Captures[i].Value;
                    int countBag = int.Parse(bagInfo.Groups[2].Captures[i].Value);
                    inBag[i] = (nameBag, countBag);
                }
                bags.Add(bag, inBag);
            }

            string searchBag = "shiny gold";

            int countBags = bags.Count(pair => BagContain(bags, pair.Key, searchBag));
            Console.Out.WriteLine("Answer #1 = {0}", countBags);

            int countInnerBags = CountInnerBags(searchBag, bags);
            Console.Out.WriteLine("Answer #2 = {0}", countInnerBags);
        }

        private bool BagContain(Dictionary<string, (string, int)[]> bags, string externalBag, string internalBag)
        {
            return bags[externalBag].Any(b => b.Item1== internalBag || BagContain(bags, b.Item1, internalBag));
        }

        private int CountInnerBags(string bag, Dictionary<string, (string, int)[]> bags)
        {
            return bags[bag].Sum(b => b.Item2 * (CountInnerBags(b.Item1, bags) + 1));
        }
    }
}

