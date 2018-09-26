using System;
using System.Collections.Generic;
using static System.Console;

namespace Coding.Exercise
{
    public class Command
    {
        public enum Action
        {
            Deposit,
            Withdraw
        }

        public Action TheAction;
        public int Amount;
        public bool Success;
    }

    public class Account
    {
        public int Balance { get; set; }

        public void Process(Command c)
        {
            switch (c.TheAction)
            {
                case Command.Action.Deposit:
                    Balance += c.Amount;
                    c.Success = true;
                    break;
                case Command.Action.Withdraw:
                    if (Balance > c.Amount)
                    {
                        Balance -= c.Amount;
                        c.Success = true;
                    }
                    else
                    {
                        c.Success = false;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override string ToString()
        {
            return $"{nameof(Balance)}: {Balance}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var acc = new Account();
            var commands = new List<Command>
            {
                new Command() {Amount = 100, TheAction = Command.Action.Deposit},
                new Command() {Amount = 50, TheAction = Command.Action.Withdraw}
            };
            WriteLine(acc);
            foreach (var command in commands)
                acc.Process(command);
            WriteLine(acc);
        }
    }
}
