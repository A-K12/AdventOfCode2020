using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day13
{
    public class Solution:ITask
    {
        public void ExecuteTask()
        {
            const string path = @".\Day13\data.txt";
            string[] lines = File.ReadAllLines(path);
            int time = int.Parse(lines[0]);
            string[] busInfo = lines[1].Split(',');
            int[] busIds  = busInfo
                .Where(s => s!="x").Select(s => (int.Parse(s))).ToArray();
            int[] a = busInfo.Select((s, i) => (s, i)).Where(tuple => tuple.s != "x").Select(tuple => -tuple.i)
                .ToArray();

            int minValue = int.MaxValue, busIndex = 0;
            for (int i = 0; i < busIds.Length; i++)
            {
                int newValue = (busIds[i] - time%busIds[i]);
                if(newValue > minValue ) continue;
                minValue = newValue;
                busIndex = i;
            }

            Console.Out.WriteLine("Answer 1 = {0}", minValue*busIds[busIndex]);

            long m = busIds.Aggregate<int, long>(1, (i, j) => i* j);
            long sm = 0;
            for (int i = 0; i < busIds.Length; i++)
            {
                long mi = m / busIds[i];
                sm += a[i] * mi * ModularMultiplicativeInverse(mi, busIds[i]);
            }

            long answer2 = (sm % m + m) % m;

            Console.Out.WriteLine("Answer 2 = {0}", answer2);

        }


        private static long ModularMultiplicativeInverse(long a, long mod)
        {
            long b = a % mod;
            for (int x = 1; x < mod; x++)
            {
                if ((b * x) % mod == 1)
                {
                    return x;
                }
            }
            return 1;
        }

    }
}