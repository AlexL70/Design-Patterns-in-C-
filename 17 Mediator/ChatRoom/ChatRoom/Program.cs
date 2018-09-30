using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

namespace Chat.Room
{
    public class Person
    {
        public string Name { get; set; }
        public ChatRoom Room { get; set; }

        private List<string> _chatLog = new List<string>();

        public Person(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public void Say(string message)
        {
            Room.Broadcast(Name, message);
        }

        public void PrivateMessage(string who, string message)
        {
            Room.Message(Name, who, message);
        }

        public void Receive(string sender, string message)
        {
            string s = $"{sender}: '{message}'";
            _chatLog.Add(s);
            WriteLine($"[{Name}'s chat session] {s}");
        }
    }

    public class ChatRoom
    {
        private List<Person> _people = new List<Person>();

        public void Join(Person p)
        {
            var joinMsg = $"{p.Name} joins the chat";
            Broadcast(p.Name, joinMsg);
            p.Room = this;
            if (_people.All(person => person.Name != p.Name))
            {
                _people.Add(p);
            }
        }

        public void Broadcast(string source, string message)
        {
            foreach (var person in _people)
            {
                if (person.Name != source)
                    person.Receive( source, message);
            }
        }

        public void Message(string source, string destination, string message)
        {
            _people.FirstOrDefault(p => p.Name == destination)
                ?.Receive(source, message);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var room = new ChatRoom();
            var john = new Person("John");
            var jane = new Person("Jane");
            room.Join(john);
            room.Join(jane);

            john.Say("Hi!");
            jane.Say("Hey, John!");

            var simon = new Person("Simon");
            room.Join(simon);
            simon.Say("Hi everybody!");
            jane.PrivateMessage("Simon", "Glad you could join us!");
        }
    }
}
