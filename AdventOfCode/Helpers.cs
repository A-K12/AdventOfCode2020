using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020
{
    public static class Helpers
    {
        public static LinkedListNode<T> NextCircular<T>(this LinkedListNode<T> node)
        {
            return node.Next ?? node.List.First;
        }
    }
}