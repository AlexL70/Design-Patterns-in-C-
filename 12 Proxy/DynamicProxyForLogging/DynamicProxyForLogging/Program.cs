using System;
using static System.Console;

namespace DynamicProxyForLogging
{
    public interface IBankAccount
    {
        void Deposit(int amount);
        bool Withdraw(int amount);
        string ToString();
    }

    public class BankAccount : IBankAccount
    {
        private int _balance;
        private int _overdraftLimit = -500;
        public int Balance => _balance;

        public void Deposit(int amount)
        {
            _balance += amount;
            WriteLine($"Deposited {amount}, balance is now {_balance}");
        }

        public bool Withdraw(int amount)
        {
            if (_balance - amount > _overdraftLimit)
            {
                _balance -= amount;
                WriteLine($"Withdrew {amount}, balance is now {_balance}");
                return true;
            }

            return false;
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
            var ba = new BankAccount();
            ba.Deposit(100);
            ba.Withdraw(50);
            WriteLine(ba);
        }
    }
}
