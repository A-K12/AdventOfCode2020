using System;
using System.Linq;

namespace AdventOfCode2020.Day17
{
    public class Cell
    {

        public int[] Coordinates { get; }
        public int dimension => Coordinates.Length;


        public Cell(params int[] coordinates)
        {
            this.Coordinates = coordinates;
        }

        protected bool Equals(Cell other)
        {
            if (other.dimension != this.dimension) return false;
            bool result = true;
            for (int i = 0; i < other.dimension; i++)
            {
                result &= Coordinates[i].Equals(other.Coordinates[i]);
            }

            return result;
        }

        public Cell Move(params int[] numers)
        {
            if (numers.Length != this.dimension) throw new ArgumentException();
            numers = numers.Select((n, i) => n + Coordinates[i]).ToArray();

            return new Cell(numers);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Cell)obj);
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            foreach (int coordinate in Coordinates)
            {
                hash.Add(coordinate);
            }

            return (Coordinates != null ? hash.ToHashCode() : 0);
        }
    }
}