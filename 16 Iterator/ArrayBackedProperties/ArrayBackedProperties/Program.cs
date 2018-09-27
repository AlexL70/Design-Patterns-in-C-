using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

namespace ArrayBackedProperties
{
    class Program
    {
        public class Creature : IEnumerable<int>
        {
            private int[] _stats = new int[3];

            public enum Stats
            {
                Strength = 0,
                Agility = 1,
                Intelligence = 2
            }

            public int Strength { get => _stats[(int) Stats.Strength]; set => _stats[(int)Stats.Strength] = value; }
            public int Agility { get => _stats[(int)Stats.Agility]; set => _stats[(int)Stats.Agility] = value; }
            public int Intelligence { get => _stats[(int)Stats.Intelligence]; set => _stats[(int)Stats.Intelligence] = value; }

            public double Average => _stats.Average();

            public int this[int index] => _stats[index];

            public Creature(int strength, int agility, int intelligence)
            {
                Strength = strength;
                Agility = agility;
                Intelligence = intelligence;
            }

            public IEnumerator<int> GetEnumerator()
            {
                return _stats.AsEnumerable().GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return _stats.AsEnumerable().GetEnumerator();
            }
        }

        static void Main(string[] args)
        {
            var cr = new Creature(7, 9, 4);
            WriteLine("Creature statistics:");
            foreach (var stat in cr)
            {
                WriteLine(stat);
            }
            WriteLine($"Average: {cr.Average}");
        }
    }
}
