using System;
using static System.Console;

namespace Coding.Exercise
{
    class Program
    {
        public interface IFactory<T>
        {
            T Create();
        }

        public class ObjectFactory : IFactory<object>
        {
            public object Create()
            {
                return new object();
            }
        }

        public class SingletonObjectFactory : IFactory<object>
        {
            private static object _obj = null;

            public object Create()
            {
                if (_obj == null)
                {
                    _obj = new object();
                }

                return _obj;
            }
        }

        static void Main(string[] args)
        {
            var simpleFactory = new ObjectFactory();
            WriteLine(
                $"Is {nameof(simpleFactory)} a singleton? It is {SingletonTester.IsSingleton(simpleFactory.Create)}.");
            var singletonFactory = new SingletonObjectFactory();
            WriteLine($"Is {nameof(singletonFactory)} a singleton? If is {SingletonTester.IsSingleton(singletonFactory.Create)}");
        }
    }
}
