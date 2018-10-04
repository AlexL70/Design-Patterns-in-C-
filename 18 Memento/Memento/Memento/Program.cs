using System;
using System.Collections.Generic;
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
            private List<Memento> _changes = new List<Memento>();
            private int _current;

            public BankAccount(int balance)
            {
                Balance = balance;
                _changes.Add(new Memento(Balance));
            }

            public Memento Deposit(int amount)
            {
                Balance += amount;
                var m = new Memento(Balance);
                _changes.Add(m);
                ++_current;
                return m;
            }

            public Memento Restore(Memento m)
            {
                if (m != null)
                {
                    Balance = m.Balance;
                    _changes.Add(m);
                    return m;
                }

                return null;
            }

            public Memento Undo()
            {
                if (_current > 0)
                {
                    var memento = _changes[--_current];
                    Balance = memento.Balance;
                    return memento;
                }

                return null;
            }

            public Memento Redo()
            {
                if (_current < _changes.Count - 1)
                {
                    var memento = _changes[++_current];
                    Balance = memento.Balance;
                    return memento;
                }

                return null;
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

            ba.Undo();
            WriteLine($"Undo 1: {ba}");
            ba.Undo();
            WriteLine($"Undo 2: {ba}");
            ba.Undo();
            WriteLine($"Undo 3: {ba}");
            ba.Redo();
            WriteLine($"Redo 1: {ba}");
            ba.Redo();
            WriteLine($"Redo 2: {ba}");
        }
    }
}
