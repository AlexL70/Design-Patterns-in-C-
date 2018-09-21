using System;
using System.Collections.Generic;
using System.Linq;

namespace RepeatingUserNames
{
    public class User
    {
        private string _fullName;

        public User(string fullName)
        {
            _fullName = fullName ?? throw new ArgumentNullException(nameof(fullName));
        }

        public string FullName => _fullName;
    }

    public class User2
    {
        private static List<String> _strings = new List<string>();
        private int[] _names;

        public User2(string fullName)
        {
            int getOrAdd(string s)
            {
                int idx = _strings.IndexOf(s);
                if (idx < 0)
                {
                    idx = _strings.Count;
                    _strings.Add(s);
                }

                return idx;
            }

            _names = fullName.Split(' ').Select(x => getOrAdd(x)).ToArray();
        }

        public string FullName => String.Join(' ', _names.Select(i => _strings[i]));
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
