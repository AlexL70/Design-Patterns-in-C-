using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

namespace Command
{
    public class BankAccount
    {
        public int Balance { get; private set; }
        public int OverdraftLimit { get; private set; } = -500;

        public void Deposit(int amount)
        {
            Balance += amount;
            WriteLine($"Deposited ${amount}, balance is now ${Balance}");
        }

        public bool Withdraw(int amount)
        {
            if (Balance - amount >= OverdraftLimit)
            {
                Balance -= amount;
                WriteLine($"Withdrew ${amount}, balance is now ${Balance}");
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            return $"{nameof(Balance)}: {Balance}, {nameof(OverdraftLimit)}: {OverdraftLimit}";
        }
    }

    public interface ICommand
    {
        bool Executed { get; }
        void Call();
        void Undo();
    }

    public class BankAccountCommand : ICommand
    {
        private BankAccount _account;

        public enum Action
        {
            Deposit, Withdraw
        }

        private readonly Action _action;
        private readonly int _amount;

        public BankAccountCommand(BankAccount account, Action action, int amount)
        {
            _account = account ?? throw new ArgumentNullException(nameof(account));
            _action = action;
            _amount = amount;
        }

        public bool Executed { get; private set; } = false;

        public void Call()
        {
            switch (_action)
            {
                case Action.Deposit:
                    _account.Deposit(_amount);
                    Executed = true;
                    break;
                case Action.Withdraw:
                    Executed = _account.Withdraw(_amount);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(_action.ToString());
            }
        }

        public void Undo()
        {
            if (Executed)
            {
                switch (_action)
                {
                    case Action.Deposit:
                        Executed = ! _account.Withdraw(_amount);
                        break;
                    case Action.Withdraw:
                        _account.Deposit(_amount);
                        Executed = false;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var ba = new BankAccount();
            var commands = new List<ICommand>
            {
                new BankAccountCommand(ba, BankAccountCommand.Action.Withdraw, 1000),
                new BankAccountCommand(ba, BankAccountCommand.Action.Deposit, 500),
                new BankAccountCommand(ba, BankAccountCommand.Action.Withdraw, 100)

            };
            WriteLine(ba);
            foreach (var command in commands)
                command.Call();
            WriteLine(ba);
            foreach (var command in Enumerable.Reverse(commands))
                command.Undo();
            WriteLine(ba);
        }
    }
}
