using System;
using Autofac;
using static System.Console;

namespace NullObject
{
    public interface ILog
    {
        void Info(string msg);
        void Warn(string msg);
    }

    public class ConsoleLog : ILog
    {
        public void Info(string msg)
        {
            WriteLine(msg);
        }

        public void Warn(string msg)
        {
            WriteLine($"WARNING!!! {msg}");
        }
    }

    public class NullLog : ILog
    {
        public void Info(string msg) {}

        public void Warn(string msg) {}
    }

    public class BankAccount
    {
        private readonly ILog _log;
        public int Balance { get; private set; }

        public BankAccount(ILog log, int balance = 0)
        {
            _log = log ?? throw new ArgumentNullException(nameof(log));
            Balance = balance;
        }

        public void Deposit(int amount)
        {
            Balance += amount;
            _log.Info($"Deposited {amount}, balance is now {Balance}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var log = new ConsoleLog();
            var ba = new BankAccount(log, 100);
            ba.Deposit(100);
            var cb = new ContainerBuilder();
            cb.RegisterType<BankAccount>();
            cb.RegisterType<NullLog>().As<ILog>();
            using (var c = cb.Build())
            {
                ba = c.Resolve<BankAccount>();
                ba.Deposit(300);
                WriteLine($"Current balance is {ba.Balance}");
            }
        }
    }
}
