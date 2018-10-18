using System;
using static System.Console;

namespace Coding.Exercise
{
    public class Creature
    {
        public int Attack, Health;

        public Creature(int attack, int health)
        {
            Attack = attack;
            Health = health;
        }
    }

    public abstract class CardGame
    {
        public Creature[] Creatures;

        public CardGame(Creature[] creatures)
        {
            Creatures = creatures;
        }

        // returns -1 if no clear winner (both alive or both dead)
        public int Combat(int creature1, int creature2)
        {
            Creature first = Creatures[creature1];
            Creature second = Creatures[creature2];
            Hit(first, second);
            Hit(second, first);
            bool firstAlive = first.Health > 0;
            bool secondAlive = second.Health > 0;
            if (firstAlive == secondAlive) return -1;
            return firstAlive ? creature1 : creature2;
        }

        // attacker hits other creature
        protected abstract void Hit(Creature attacker, Creature other);
    }

    public class TemporaryCardDamageGame : CardGame
    {
        public TemporaryCardDamageGame(Creature[] creatures) : base(creatures) {}

        protected override void Hit(Creature attacker, Creature other)
        {
            if (attacker.Attack >= other.Health)
            {
                other.Health -= attacker.Attack;
            }
        }
    }

    public class PermanentCardDamage : CardGame
    {
        public PermanentCardDamage(Creature[] creatures) : base(creatures){}

        protected override void Hit(Creature attacker, Creature other)
        {
            other.Health -= attacker.Attack;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var creatures = new Creature[]
            {
                new Creature(1, 2),
                new Creature(1, 3) 
            };
            var gm0 = new PermanentCardDamage(creatures);
            WriteLine($"The winner is creature #{FightToDeath(gm0)}");
            var gm1 = new TemporaryCardDamageGame(creatures);
            WriteLine($"The winner is creature #{FightToDeath(gm1)}");
        }

        private static int FightToDeath(CardGame gm0)
        {
            var winner = -1;
            var turn = 0;
            while (winner == -1 && turn < 100)
            {
                winner = gm0.Combat(0, 1);
                turn++;
            }

            return winner;
        }
    }
}
