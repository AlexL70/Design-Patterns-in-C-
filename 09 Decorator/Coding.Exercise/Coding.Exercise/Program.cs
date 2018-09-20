using System;
using static System.Console;

namespace Coding.Exercise
{
    public class Bird
    {
        public int Age { get; set; }

        public string Fly()
        {
            return (Age < 10) ? "flying" : "too old";
        }
    }

    public class Lizard
    {
        public int Age { get; set; }

        public string Crawl()
        {
            return (Age > 1) ? "crawling" : "too young";
        }
    }

    public class Dragon // no need for interfaces
    {
        private readonly Lizard _lizard = new Lizard();
        private readonly Bird _bird = new Bird();

        public int Age
        {
            get
            {
                return _bird.Age;
            }
            set { _bird.Age = value;
                _lizard.Age = value;
            }
        }

        public string Fly()
        {
            return _bird.Fly();
        }

        public string Crawl()
        {
            return _lizard.Crawl();
        }
    }



    class Program
    {
        static void Main(string[] args)
        {
            var dr = new Dragon();
            dr.Age = 1;
            WriteLine(dr.Fly());
            WriteLine(dr.Crawl());
            dr.Age = 5;
            WriteLine(dr.Fly());
            WriteLine(dr.Crawl());
            dr.Age = 11;
            WriteLine(dr.Fly());
            WriteLine(dr.Crawl());
        }
    }
}
