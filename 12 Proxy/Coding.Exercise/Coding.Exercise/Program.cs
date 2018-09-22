using System;
using static System.Console;

namespace Coding.Exercise
{
    public class Person
    {
        public int Age { get; set; }

        public string Drink()
        {
            return "drinking";
        }

        public string Drive()
        {
            return "driving";
        }

        public string DrinkAndDrive()
        {
            return "driving while drunk";
        }
    }

    public class ResponsiblePerson
    {
        private readonly Person _person;

        public ResponsiblePerson(Person person)
        {
            _person = person ?? throw new ArgumentNullException(nameof(person));
        }

        public int Age
        {
            get => _person.Age;
            set => _person.Age = value;
        }

        public string Drink()
        {
            return Age >= 18 ? _person.Drink() : "too young";
        }

        public string Drive()
        {
            return Age > 16 ? _person.Drive() : "too young";
        }

        public string DrinkAndDrive()
        {
            return "dead";
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            var rp = new ResponsiblePerson(new Person() { Age = 13 });
            WriteLine(rp.Drink());
            WriteLine(rp.Drive());
            rp.Age = 18;
            WriteLine(rp.Drink());
            WriteLine(rp.Drive());
            WriteLine(rp.DrinkAndDrive());
        }
    }
}
