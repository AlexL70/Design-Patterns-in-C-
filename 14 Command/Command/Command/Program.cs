using System;
using System.Collections.Generic;
using static System.Console;

namespace Command
{
    public class BankAccount
    {
        private int _balance;
        private int _overdraftLimit = -500;

        public int Balance => _balance;
        public int OverdraftLimit => _overdraftLimit;

        public void Deposit(int amount)
        {
            _balance += amount;
            WriteLine($"Deposited ${amount}, balance is now ${_balance}");
        }

        public void Withdraw(int amount)
        {
            if (_balance - amount >= _overdraftLimit)
            {
                _balance -= amount;
                WriteLine($"Withdrew ${amount}, balance is now ${_balance}");
            }
        }

        public override string ToString()
        {
            return $"{nameof(Balance)}: {Balance}, {nameof(OverdraftLimit)}: {OverdraftLimit}";
        }
    }

    public interface ICommand
    {
        void Call();
    }

    public class BankAccountCommand : ICommand
    {
        private BankAccount _account;

        public enum Action
        {
            Deposit, Withdraw
        }

        private Action _action;
        private int _amount;

        public BankAccountCommand(BankAccount account, Action action, int amount)
        {
            _account = account ?? throw new ArgumentNullException(nameof(account));
            _action = action;
            _amount = amount;
        }

        public void Call()
        {
            switch (_action)
            {
                case Action.Deposit:
                    _account.Deposit(_amount);
                    break;
                case Action.Withdraw:
                    _account.Withdraw(_amount);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(_action.ToString());
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
                new BankAccountCommand(ba, BankAccountCommand.Action.Withdraw, 100),
                new BankAccountCommand(ba, BankAccountCommand.Action.Deposit, 500)

            };
            WriteLine(ba);
            foreach (var command in commands)
                command.Call();
            WriteLine(ba);
        }
    }
}
