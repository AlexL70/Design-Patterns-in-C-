using System;
using static System.Console;

namespace MethodChain
{
    public class Creature
    {
        public string Name { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }

        public Creature(string name, int attack, int defense)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Attack = attack;
            Defense = defense;
        }

        public override string ToString() =>
            $"{nameof(Name)}: {Name}, {nameof(Attack)}: {Attack}, {nameof(Defense)}: {Defense}";
    }

    public class CreatureModifier
    {
        protected Creature creature { get; set; }
        protected CreatureModifier next { get; set; }   //  Linked List

        public CreatureModifier(Creature creature)
        {
            this.creature = creature ?? throw new ArgumentNullException(nameof(creature));
        }

        public void Add(CreatureModifier cm)
        {
            if (next != null)
            {
                next.Add(cm);
            }
            else
            {
                next = cm;
            }
        }

        public virtual void Handle() => next?.Handle();
    }

    public class DoubleAttackModifier : CreatureModifier
    {
        public DoubleAttackModifier(Creature creature) : base(creature) {}

        public override void Handle()
        {
            WriteLine($"Doubling {creature.Name}'s attack value.");
            creature.Attack *= 2;
            base.Handle();
        }
    }

    public class IncreaseDefenseModifier : CreatureModifier
    {
        public IncreaseDefenseModifier(Creature creature) : base(creature) {}

        public override void Handle()
        {
            WriteLine($"Increment {creature.Name}'s defense value.");
            creature.Defense++;
            base.Handle();
        }
    }

    public class NoBonusesModifier : CreatureModifier
    {
        public NoBonusesModifier(Creature creature) : base(creature) {}

        public override void Handle()
        {
            //  Do nothing so the chain of responsibility is interrupted here.
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var goblin = new Creature("Goblin", 2, 2);
            WriteLine(goblin);
            var root = new CreatureModifier(goblin);
            WriteLine("Let's double the Goblin's attack.");
            root.Add(new DoubleAttackModifier(goblin));
            WriteLine("Let's put 'No Bonuses' spell on it...");
            root.Add(new NoBonusesModifier(goblin));
            WriteLine("Let's increment the Goblin's defense.");
            root.Add(new IncreaseDefenseModifier(goblin));
            root.Handle();
            WriteLine(goblin);
        }
    }
}
