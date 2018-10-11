using System;
using static System.Console;

namespace ObserverViaEventKeyword
{
    public class FallsIllEventArgs : EventArgs
    {
        public string Address { get; set; }
    }

    public class Person
    {
        public void CatchACold()
        {
            FallsIll?.Invoke(this, new FallsIllEventArgs() { Address = "123, London Road" });
        }

        public event EventHandler<FallsIllEventArgs> FallsIll;
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            var person = new Person();

            person.FallsIll += CallDoctor;
            person.CatchACold();
        }

        private static void CallDoctor(object sender, FallsIllEventArgs e)
        {
            WriteLine($"A doctor should be called to {e.Address}");
        }
    }
}
