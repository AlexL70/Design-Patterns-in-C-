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
        public class Relationships : IRelationshipBrowser
        {
            private List<(Person,Relationship,Person)> relations = new List<(Person, Relationship, Person)>();

            public void AddParentAndChild(Person parent, Person child)
            {
                relations.Add((parent, Relationship.Parent, child));
                relations.Add((child, Relationship.Child, parent));
            }

            public IEnumerable<Person> FindAllChildrenOf(string name)
            {
                return relations.Where(r => r.Item1.Name == name && r.Item2 == Relationship.Parent)
                    .Select(r => r.Item3);
            }
        }

        public interface IRelationshipBrowser
        {
            IEnumerable<Person> FindAllChildrenOf(string name);
        }

        //  High-level part
        public class Research
        {
            public Research(IRelationshipBrowser relBrowser)
            {
                foreach (var child in relBrowser.FindAllChildrenOf("John"))
                {
                    Console.WriteLine($"John has child called {child.Name}");   
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
