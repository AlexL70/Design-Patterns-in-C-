using System;
using System.Dynamic;
using Autofac;
using ImpromptuInterface;
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

    public class Null<TInterface> : DynamicObject where TInterface : class
    {
        public static TInterface Instance => 
            new Null<TInterface>().ActLike<TInterface>();

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            result = Activator.CreateInstance(binder.ReturnType);
            return true;
        }
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
            //  Use normal call
            var log = new ConsoleLog();
            var ba = new BankAccount(log, 100);
            ba.Deposit(100);
            //  Use static null object instead as ILog
            var cb = new ContainerBuilder();
            cb.RegisterType<BankAccount>();
            cb.RegisterType<NullLog>().As<ILog>();
            using (var c = cb.Build())
            {
                WriteLine();
                WriteLine("Use static null object");
                ba = c.Resolve<BankAccount>();
                ba.Deposit(300);
                WriteLine($"Current balance is {ba.Balance}");
            }
            //  Use dynamic null object as ILog
            WriteLine();
            WriteLine("Use dynamic null object");
            var dnLog = Null<ILog>.Instance;
            ba = new BankAccount(dnLog);
            ba.Deposit(500);
            WriteLine($"Current balance is {ba.Balance}");
        }
    }
}
