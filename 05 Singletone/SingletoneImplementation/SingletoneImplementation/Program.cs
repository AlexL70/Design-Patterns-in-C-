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

    public class SingletonDatabase : IDatabase
    {
        private readonly Dictionary<string, int> _capitals;
        private static int _instanceCount = 0;
        public static int Count => _instanceCount;

        private SingletonDatabase()
        {
            _instanceCount++;
            WriteLine("Initializing Database");
            _capitals = File.ReadAllLines(
                    Path.Combine(new FileInfo(typeof(IDatabase).Assembly.Location).DirectoryName,
                    "Capitals.txt"))
                .Batch(2).ToDictionary(
                    list => list.ElementAt(0).Trim(),
                    list => int.Parse(list.ElementAt(1))
                    );
        }

        public int GetPopulation(string name)
        {
            return _capitals[name];
        }

        private static readonly Lazy<SingletonDatabase> instance = new Lazy<SingletonDatabase>(() => new SingletonDatabase());

        public static SingletonDatabase Instance => instance.Value;
    }

    public class OrdinaryDatabase : IDatabase
    {
        private readonly Dictionary<string, int> _capitals;

        public OrdinaryDatabase()
        {
            WriteLine("Initializing Database");
            _capitals = File.ReadAllLines(
                    Path.Combine(new FileInfo(typeof(IDatabase).Assembly.Location).DirectoryName,
                        "Capitals.txt"))
                .Batch(2).ToDictionary(
                    list => list.ElementAt(0).Trim(),
                    list => int.Parse(list.ElementAt(1))
                );
        }

        public int GetPopulation(string name)
        {
            return _capitals[name];
        }
    }

    public class SingletonRecordFinder
    {
        public int GetTotalPopulation(IEnumerable<string> names)
        {
            int result = 0;
            foreach (var name in names)
            {
                result += SingletonDatabase.Instance.GetPopulation(name);
            }
            return result;
        }
    }

    public class ConfigurableRecordFinder
    {
        private readonly IDatabase _database;

        public ConfigurableRecordFinder(IDatabase database)
        {
            this._database = database ?? throw new ArgumentNullException(nameof(database));
        }

        public int GetTotalPopulation(IEnumerable<string> names)
        {
            int result = 0;
            foreach (var name in names)
            {
                result += _database.GetPopulation(name);
            }
            return result;
        }
    }

    public class DummyDatabase : IDatabase
    {
        private Dictionary<string, int> _capitals = new Dictionary<string, int>()
        {
            ["alpha"] = 1,
            ["beta"] = 2,
            ["gamma"] = 3
        };

        public int GetPopulation(string name)
        {
            return _capitals[name];
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var db = SingletonDatabase.Instance;
            const string city = "Osaka";
            WriteLine($"The population of {city} is {db.GetPopulation(city)} people");
        }
    }
}
