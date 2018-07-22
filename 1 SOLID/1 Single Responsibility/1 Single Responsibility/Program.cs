using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _1_Single_Responsibility
{
    class Program
    {
        public class Journal
        {
            private List<string> _entries = new List<string>();

            private static int _count = 0;

            public int AddEntry(string text)
            {
                _entries.Add($"{++_count} {text}");
                return _count;
            }

            public void RemoveEntry(int index)
            {
                _entries.RemoveAt(index);
            }

            public override string ToString()
            {
                return string.Join(Environment.NewLine, _entries);
            }

            public void Save(string fileName)
            {
                File.WriteAllText(fileName, ToString());
            }

            public static Journal Load(string fileName)
            {
                var j = new Journal();
                var lines = File.ReadAllLines(fileName);
                j._entries = lines.ToList();
                return j;
            }
         }

        static void Main(string[] args)
        {
            var j = new Journal();
            j.AddEntry("I cried today");
            j.AddEntry("Delete me!");
            j.AddEntry("I ate a bug");
            j.RemoveEntry(1);
            Console.WriteLine(j);
        }
    }
}
