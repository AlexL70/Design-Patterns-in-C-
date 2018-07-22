using System;
using System.Collections.Generic;
using System.Linq;

namespace _5_Dependency_Inversion
{
    class Program
    {

        public enum Relationship
        {
            Parent, Child, Sibling
        }

        public class Person
        {
            public string Name { get; set; }
            public DateTime DateOfBirth { get; set; }
        }

        //  Low-level part
        public class Relationships
        {
            private List<(Person,Relationship,Person)> relations = new List<(Person, Relationship, Person)>();

            public void AddParentAndChild(Person parent, Person child)
            {
                relations.Add((parent, Relationship.Parent, child));
                relations.Add((child, Relationship.Child, parent));
            }

            public List<(Person, Relationship, Person)> Relations => relations;
        }

        //  High-level part
        public class Research
        {
            public Research(Relationships relationships)
            {
                foreach ((Person, Relationship, Person) relation in relationships.Relations
                    .Where(r => r.Item1.Name == "John" && r.Item2 == Relationship.Parent))
                {
                    Console.WriteLine($"John has child called {relation.Item3.Name}");   
                }
            }
        }
        static void Main(string[] args)
        {
            var parent = new Person {Name = "John"};
            var child1 = new Person {Name = "Chris"};
            var child2 = new Person {Name = "Mary"};

            var relarionships = new Relationships();
            relarionships.AddParentAndChild(parent,child1);
            relarionships.AddParentAndChild(parent, child2);

            var r = new Research(relarionships);
        }
    }
}
