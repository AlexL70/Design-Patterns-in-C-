using System;
using static System.Console;

namespace MultipleInheritance
{
    public interface IBird
    {
        void Fly();
        int Weight { get; set; }
    }

    public class Bird : IBird
    {
        public int Weight { get; set; }
        public void Fly() => WriteLine($"Soaring in the sky with weight {Weight}");
    }

    public interface ILizard
    {
        void Crawl();
        int Weight { get; set; }
    }

    public class Lizard : ILizard
    {
        public int Weight { get; set; }
        public void Crawl() => WriteLine($"Crawling in the dirt with weight {Weight}");
    }

    public class Dragon : IBird, ILizard
    {
        private readonly Bird _bird;
        private readonly Lizard _lizard;

        public Dragon(Bird bird, Lizard lizard, int weight)
        {
            _bird = bird ?? throw new ArgumentNullException(nameof(bird));
            _lizard = lizard ?? throw new ArgumentNullException(nameof(lizard));
            Weight = weight;
        }

        public void Crawl() => _lizard.Crawl();

        public void Fly() => _bird.Fly();
        public int Weight
        {
            get => _lizard.Weight;
            set
            {
                _bird.Weight = value;
                _lizard.Weight = value;
            }
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            var dragon = new Dragon(new Bird(), new Lizard(), 22);
            dragon.Fly();
            dragon.Crawl();
        }
    }
}
