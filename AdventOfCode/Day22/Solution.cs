using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Day22
{
    public class Solution:ITask
    {
        public void ExecuteTask()
        {
            const string path = @".\Day22\data.txt";
            var cards = File.ReadAllText(path).Split("\r\n\r\n")
                .Select(s => s.Split("\r\n")[1..]
                    .Select(s1 => int.Parse(s1)).ToArray()).ToArray();


            Queue<int> player1 = new Queue<int>(cards[0]);
            Queue<int> player2 = new Queue<int>(cards[1]);

            int result = PlayInCombat(player1, player2, false);
            var winner = result == 1 ? player1 : player2;
            Console.Out.WriteLine("Answer 1 = {0}", GetScore(winner));

            player1 = new Queue<int>(cards[0]);
            player2 = new Queue<int>(cards[1]);

            result = PlayInCombat(player1, player2, true);
            winner = result == 1 ? player1 : player2;
            Console.Out.WriteLine("Answer 2 = {0}", GetScore(winner));
        }

        private int GetScore(Queue<int> player)
        {
            return player.Select((i, i1) => i * (player.Count - i1)).Sum();
        }

        private int PlayInCombat(Queue<int> player1, Queue<int> player2, bool isRecursive)
        {
            HashSet<string> previousPlayer1 = new HashSet<string>();
            HashSet<string> previousPlayer2 = new HashSet<string>();
            int winner = 0;
            while (player1.Count != 0 && player2.Count != 0)
            {
                if (isRecursive)
                {
                    string play1 = string.Join(' ', player1.ToArray());
                    string play2 = string.Join(' ', player2.ToArray());
                    if (previousPlayer1.Contains(play1) && previousPlayer2.Contains(play2))
                    {
                        return 1;
                    }
                    else
                    {
                        previousPlayer2.Add(play2);
                        previousPlayer1.Add(play1);
                    }
                }
                int p1 = player1.Dequeue();
                int p2 = player2.Dequeue();
                if (isRecursive && p1 <= player1.Count && p2 <= player2.Count)
                {
                    winner = PlayInCombat(new Queue<int>(player1.Take(p1)), 
                        new Queue<int>(player2.Take(p2)), true);
                }
                else
                {
                    winner = (p1 < p2) ? 2 : 1;
                }

                if (winner == 1)
                {
                    player1.Enqueue(p1);
                    player1.Enqueue(p2);
                }
                else
                {
                    player2.Enqueue(p2);
                    player2.Enqueue(p1);
                }
            }

            return winner;
        }
    }
}