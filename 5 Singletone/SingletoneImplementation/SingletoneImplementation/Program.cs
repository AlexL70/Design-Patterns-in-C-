using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MoreLinq;
using static System.Console;

namespace SingletoneImplementation
{
    public interface IDatabase
    {
        int GetPopulation(string name);
    }

    public class SingletoneDatabase : IDatabase
    {
        private Dictionary<string, int> _capitals;

        private SingletoneDatabase()
        {
            WriteLine("Initializing Database");
            _capitals = File.ReadAllLines("Capitals.txt")
                .Batch(2).ToDictionary(
                    list => list.ElementAt(0).Trim(),
                    list => int.Parse(list.ElementAt(1))
                    );
        }

        public int GetPopulation(string name)
        {
            return _capitals[name];
        }

        private static readonly Lazy<SingletoneDatabase> instance = new Lazy<SingletoneDatabase>(() => new SingletoneDatabase());

        public static SingletoneDatabase Instance => instance.Value;
    }

    class Program
    {
        static void Main(string[] args)
        {
            var db = SingletoneDatabase.Instance;
            string city = "Osaka";
            WriteLine($"The population of {city} is {db.GetPopulation(city)} people");
        }
    }
}
