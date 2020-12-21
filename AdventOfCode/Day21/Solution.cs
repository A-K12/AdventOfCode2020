using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Day21
{
    public class Solution:ITask
    {
        public void ExecuteTask()
        {
            const string path = @".\Day21\data.txt";
            Regex regex = new Regex(@"(?:(?'in'\w+) )+\(contains(?: (?'al'\w+)[,)])+");
            var products = File.ReadAllLines(path).Select(s => regex.Match(s))
                .Aggregate(new List<(HashSet<string>, string[])>(), (list, match) =>
                {
                    list.Add((match.Groups["in"].Captures.Select(c =>c.Value).ToHashSet(), 
                        match.Groups["al"].Captures.Select(c =>c.Value).ToArray()));
                    return list;
                });

            Dictionary<string, HashSet<string>> allergicDict = new Dictionary<string, HashSet<string>>();

            foreach ((HashSet<string> ingredients, string[] allergens) in products)
            {
                foreach (string allergen in allergens)
                {
                    allergicDict[allergen] = ingredients.TryGetValue(allergen, out var ingredient) ?
                        allergicDict[allergen].Intersect(ingredients).ToHashSet(): ingredients;
                }
            }

            var allergicIngredients = allergicDict.SelectMany(pair => pair.Value).ToHashSet();

            var answer1 = products.SelectMany(pair => pair.Item1).
                Count(s => !allergicIngredients.Contains(s));

            Console.Out.WriteLine("Answer 1 = {0}", answer1);

            SortedDictionary<string, string> answer2 = new SortedDictionary<string, string>();

            while (allergicIngredients.Count > 0)
            {
                foreach (KeyValuePair<string, HashSet<string>> allergen in allergicDict)
                {
                    if(allergen.Value.Count !=1) continue;

                    string ingredient = allergen.Value.First();

                    answer2.Add(allergen.Key, ingredient);

                    allergicIngredients.Remove(ingredient);

                    foreach (var value in allergicDict.Values)
                    {
                        value.Remove(ingredient);
                    }
                }
            }

            Console.Out.WriteLine("Answer 2 = {0}", string.Join(',',answer2.Values));
        }
    }
}