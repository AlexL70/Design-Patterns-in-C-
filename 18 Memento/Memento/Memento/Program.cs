using System;
using static System.Console;

namespace Memento
{
    class Program
    {
        public class Memento
        {
            public int Balance { get; }

            public Memento(int balance)
            {
                Balance = balance;
            }
        }

        public class BankAccount
        {
            public int Balance { get; private set; }

            public BankAccount(int balance)
            {
                Balance = balance;
            }

            public Memento Deposit(int amount)
            {
                Balance += amount;
                return new Memento(Balance);
            }

            public void Restore(Memento m)
            {
                Balance = m.Balance;
            }

            public override string ToString() => $"{nameof(Balance)}: {Balance}";
        }

        static void Main(string[] args)
        {
            var ba = new BankAccount(100);
            WriteLine(ba);
            var m1 = ba.Deposit(50); //  150
            var m2 = ba.Deposit(25); //  75
            WriteLine(ba);

            ba.Restore(m1);
            WriteLine(ba);

            ba.Restore(m2);
            WriteLine(ba);
        }
    }
}
