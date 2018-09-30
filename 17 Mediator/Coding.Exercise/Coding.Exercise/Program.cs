using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

namespace Coding.Exercise
{
    public class Participant
    {
        private Mediator _mediator;
        public int Value { get; set; }

        public Participant(Mediator mediator)
        {
            _mediator = mediator;
            _mediator.Join(this);
        }

        public void Say(int n)
        {
            _mediator.Broadcast(n, this);
        }

        public void Receive(int i)
        {
            this.Value += i;
        }
    }

    public class Mediator
    {
        private static readonly List<Participant> _participants = new List<Participant>();

        public void Broadcast(int i, Participant current)
        {
            foreach (var participant in _participants.Where(p => p != current))
            {
                participant.Receive(i);
            }
        }

        public void Join(Participant participant)
        {
            _participants.Add(participant);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var med = new Mediator();
            var p1 = new Participant(med) { Value = 0};
            var p2 = new Participant(med) { Value = 0};
            p1.Say(3);
            WriteLine($"p1 = {p1.Value}, p2 = {p2.Value}");
            p2.Say(2);
            WriteLine($"p1 = {p1.Value}, p2 = {p2.Value}");
        }
    }
}
