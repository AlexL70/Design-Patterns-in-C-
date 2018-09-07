using System;

namespace InheritanceWithRecursiveGenerics
{
    public class Person
    {
        public string Name { get; set; }
        public string Position { get; set; }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
        }

        public class Builder : PersonJobBuilder<Builder> { }

        public static Builder Create = new Builder();
    }

    public class PersonBuilder
    {
        protected Person person = new Person();

        public Person Build()
        {
            return person;
        }
    }

    public class PersonInfoBuilder<SELF> : PersonBuilder
        where SELF : PersonInfoBuilder<SELF>
    {
        public SELF Called(string name)
        {
            person.Name = name;
            return (SELF)this;
        }
    }

    public class PersonJobBuilder<SELF> : PersonInfoBuilder<PersonJobBuilder<SELF>>
        where SELF : PersonJobBuilder<SELF>
    {
        public SELF WorkAsA(string position)
        {
            person.Position = position;
            return (SELF)this;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Person.Create.Called("Jack").WorkAsA("postman").Build());
        }
    }
}
