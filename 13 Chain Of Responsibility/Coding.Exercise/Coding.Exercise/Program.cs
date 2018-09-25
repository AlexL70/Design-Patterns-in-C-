using System;
using System.Collections.Generic;
using System.Linq;

namespace Coding.Exercise
{
    public abstract class Creature : IDisposable
    {
        protected Game _game;

        public abstract int Attack { get; }
        public abstract int Defense { get; }

        public void Dispose()
        {
            var index = _game.Creatures.IndexOf(this);
            if (index >= 0)
            {
                _game.Creatures.RemoveAt(index);
            }
        }
    }

    public class Goblin : Creature
    {
        public Goblin(Game game)
        {
            _game = game;
        }

        public override int Attack => 1 + _game.Creatures.Count(c => c.GetType() == (typeof(GoblinKing)));

        public override int Defense => _game.Creatures.Count(c => c.GetType() == (typeof(GoblinKing)) || c.GetType() == typeof(Goblin));
    }

    public class GoblinKing : Goblin
    {
        public GoblinKing(Game game) : base(game) { }

        public override int Attack => 3 + (_game.Creatures.Count(c => c.GetType() == typeof(GoblinKing)) - 1);

        public override int Defense => 2 + _game.Creatures.Count(c => c.GetType() == typeof(GoblinKing) || c.GetType() == typeof(Goblin));
    }

    public class Game
    {
        public IList<Creature> Creatures = new List<Creature>();
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
