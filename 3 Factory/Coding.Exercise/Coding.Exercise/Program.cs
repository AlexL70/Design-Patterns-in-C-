using System;
using System.Collections.Generic;

namespace Coding.Exercise
{
    class Program
    {
        public class Person
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public override string ToString() => $"Id = {Id}, Name = {Name}";
        }

        public class PersonFactory
        {
            private int _index;

            public Person CreatePerson(string name)
            {
                return new Person {Id = _index++, Name = name};
            }

            public PersonFactory()
            {
                _index = 0;
            }
        }

        static void Main(string[] args)
        {
            var factory = new PersonFactory();
            List<Person> persons = new List<Person>
            {
                factory.CreatePerson("John"),
                factory.CreatePerson("Jack"),
                factory.CreatePerson("Suzy")
            };
            foreach (var p in persons)
            {
                Console.WriteLine(p);
            }
        }
    }
}
