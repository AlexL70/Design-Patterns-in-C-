using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks.Dataflow;

namespace _1_Single_Responsibility
{
    class Program
    {
        public class Journal
        {
            public Journal() {}

            public Journal(IEnumerable<string> entries)
            {
                _entries = entries.ToList();
            }

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
         }

        public class JournalPersistence
        {
            public void Save(Journal j, string fileName, bool overwrite = false)
            {
                if ( overwrite || !File.Exists(fileName))
                {
                    File.WriteAllText(fileName, j.ToString());
                }
            }

            public static Journal Load(string fileName)
            {
                var lines = File.ReadAllLines(fileName);
                return new Journal(lines);
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

            var p = new JournalPersistence();
            var fileName = @"c:\Temp\Journal.txt";
            p.Save(j, fileName, true);
            Process.Start("notepad.exe", fileName);
        }
    }
}
