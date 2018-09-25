using System;
using static System.Console;

namespace BrokerChain
{
    public class Game
    {
        public event EventHandler<Query> Queries;

        public void PerformQuery(object sender, Query q)
        {
            Queries?.Invoke(sender, q);
        }
    }

    public class Query
    {
        public string CreatureName { get; set; }

        public enum Argument
        {
            Attack, Defense
        }

        public Argument WhatToQuery { get; set; }
        public int Value { get; set; }

        public Query(string creatureName, Argument whatToQuery, int value)
        {
            CreatureName = creatureName ?? throw new ArgumentNullException(nameof(creatureName));
            WhatToQuery = whatToQuery;
            Value = value;
        }
    }

    public class Creature
    {
        private Game _game;
        public string Name { get; private set; }

        private int _attack;
        public int Attack
        {
            get
            {
                var q = new Query(Name, Query.Argument.Attack, _attack);
                _game?.PerformQuery(this, q);
                return q.Value;
            }
        }

        private int _defense;
        public int Defense
        {
            get
            {
                var q = new Query(Name, Query.Argument.Defense, _defense);
                _game?.PerformQuery(this, q);
                return q.Value;
            }
        }

        public Creature(Game game, string name, int attack, int defense)
        {
            _game = game ?? throw new ArgumentNullException(nameof(game));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            _attack = attack;
            _defense = defense;
        }

        public override string ToString() => 
            $"{nameof(Name)}: {Name}, {nameof(Attack)}: {Attack}, {nameof(Defense)}: {Defense}";
    }

    public abstract class CreatureModifier : IDisposable
    {
        protected Game Game;
        protected Creature Creature;

        protected CreatureModifier(Game game, Creature creature)
        {
            Game = game ?? throw new ArgumentNullException(nameof(game));
            Creature = creature ?? throw new ArgumentNullException(nameof(creature));
            Game.Queries += Handle;
        }

        protected abstract void Handle(object sender, Query q);

        public void Dispose()
        {
            Game.Queries -= Handle;
        }
    }

    public class DoubleAttackModifier : CreatureModifier
    {
        public DoubleAttackModifier(Game game, Creature creature) : base(game, creature) {}

        protected override void Handle(object sender, Query q)
        {
            if (q.WhatToQuery == Query.Argument.Attack && q.CreatureName == Creature.Name)
            {
                q.Value *= 2;
            }
        }
    }

    public class  IncrementDefenseModifier : CreatureModifier
    {
        public IncrementDefenseModifier(Game game, Creature creature) : base(game, creature) {}

        protected override void Handle(object sender, Query q)
        {
            if (q.WhatToQuery == Query.Argument.Defense && q.CreatureName == Creature.Name)
            {
                ++q.Value;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game();
            var goblin = new Creature(game, "Goblin", 2, 2);
            WriteLine(goblin);
            WriteLine("Let's double attack...");
            var da = new DoubleAttackModifier(game, goblin);
            WriteLine(goblin);
            WriteLine("Let's increment defense...");
            var id = new IncrementDefenseModifier(game, goblin);
            WriteLine(goblin);
            WriteLine("Let's remove attack modifier...");
            da.Dispose();
            WriteLine(goblin);
        }
    }
}
