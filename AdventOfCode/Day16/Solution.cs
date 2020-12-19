using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Day16
{
    public class Solution:ITask

    {
        public void ExecuteTask()
        {
            const string path = @".\Day16\data.txt";
            string[] groups = File.ReadAllText(path).Split("\r\n\r\n");
            Regex ruleMask = new Regex(@"(?'name'\w+(?: \w+)?): (?'l1'\d+)-(?'l2'\d+) or (?'r1'\d+)-(?'r2'\d+)");

            (string name,int[] l, int[] r)[] rules = groups[0].Split("\r\n").Select(s => ruleMask.Match(s))
                .Select(m => (m.Groups["name"].Value, new int[]{int.Parse(m.Groups["l1"].Value), int.Parse(m.Groups["l2"].Value)}, 
                    new int[] { int.Parse(m.Groups["r1"].Value), int.Parse(m.Groups["r2"].Value) })).ToArray();

            int[][] tickets = groups[2].Split("\r\n")[1..]
                .Select(s => s.Split(',').Select(int.Parse).ToArray()).ToArray();
            
            int[] myTicket = groups[1].Split("\r\n")[1].Split(',').Select(int.Parse).ToArray();

            int sumErrors = tickets.Sum(ticket => ticket.Where(i => !rules.Any(t => (i >= t.l[0] && t.l[1] >= i) || (i >= t.r[0] && t.r[1] >= i))).Sum());

            Console.Out.WriteLine("Answer 1 = {0}", sumErrors);

            int[][] rightTickets = tickets.Where(ticket => 
                ticket.Count(i => 
                    !rules.Any(t => 
                        (i >= t.l[0] && t.l[1] >= i) || (i >= t.r[0] && t.r[1] >= i)))==0).ToArray();

            var test = new (int[], int)[rightTickets[0].Length];
            for (int i = 0; i < test.Length; i++)
            {
                test[i] = (rightTickets.Select(n => n[i]).ToArray(), i);
            }

            int[] rightLocation = new int[rightTickets[0].Length];
            HashSet<int> usedColumn = new HashSet<int>();
            while (rightLocation.Count(i => i == 0) != 1)
            {
                for (int i = 0; i < rightLocation.Length; i++)
                {
                    (int[], int)[] match = test.Where(tuple => tuple.Item1.All(index =>
                        ((index >= rules[i].l[0] && rules[i].l[1] >= index) ||
                        (index >= rules[i].r[0] && rules[i].r[1] >= index))&&(!usedColumn.Contains(tuple.Item2)))).ToArray();
                    if (match.Length != 1) continue;
                    rightLocation[i] = match[0].Item2;
                    bool add = usedColumn.Add(match[0].Item2);
                }
            }

            long result = rules.Select((tuple, i) => (tuple, i)).Where((rule, i) 
                => rule.tuple.name.StartsWith("departure"))
                .ToArray().Select(t => myTicket[rightLocation[t.i]])
                .Aggregate<int, long>((long)1, (i, i1) => i * i1);

            Console.Out.WriteLine("Answer 2 = {0}", result);
        }
    }
}