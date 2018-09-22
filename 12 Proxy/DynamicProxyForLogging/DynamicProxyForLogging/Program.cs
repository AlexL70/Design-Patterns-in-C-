using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using ImpromptuInterface;
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

    public class Log<T> : DynamicObject
        where T : class, new()
    {
        private readonly T _subject;
        private Dictionary<string, int> _methodCallCount = new Dictionary<string, int>();

        public Log(T subject) => _subject = subject ?? throw new ArgumentNullException(nameof(subject));

        public static I As<I>() where I : class
        {
            if (!typeof(I).IsInterface)
                throw  new ArgumentException($"{nameof(I)} must be an interface type");

            return new Log<T>(new T()).ActLike<I>();
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            try
            {
                WriteLine($"Invoking {_subject.GetType().Name}.{binder.Name} with arguments [{string.Join(',', args)}]");
                if (_methodCallCount.ContainsKey(binder.Name))
                {
                    _methodCallCount[binder.Name]++;
                }
                else
                {
                    _methodCallCount[binder.Name] = 1;
                }

                result = _subject.GetType().GetMethod(binder.Name).Invoke(_subject, args);
                return true;
            }
            catch (Exception e)
            {
                WriteLine($"Invocation failed: {e}");
                result = null;
                return false;
            }
        }

        public string Info
        {
            get
            {
                var sb = new StringBuilder();
                foreach (var kv in _methodCallCount)
                {
                    sb.AppendLine($"{kv.Key} method has been called {kv.Value} time(s).");
                }
                return sb.ToString();
            }
        }

        public override string ToString()
        {
            return $"{Info}{_subject}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var ba = Log<BankAccount>.As<IBankAccount>();
            ba.Deposit(100);
            ba.Withdraw(50);
            ba.Deposit(70);
            WriteLine($"{Environment.NewLine}Log:{Environment.NewLine}{ba}");
        }
    }
}
