using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.dotMemoryUnit;
using NUnit.Framework;
using RepeatingUserNames;
using static System.Console;

namespace UnitTestProject1
{
    [TestFixture]
    public class Demo
    {
        [Test] //   15080955
        public void TestUser()
        {
            var firstNames = Enumerable.Range(0, 300).Select(x => RandomStrings()).ToList();
            var lastNames = Enumerable.Range(0, 300).Select(x => RandomStrings()).ToList();

            var users = new List<User>();

            foreach (var firstName in firstNames)
            {
                foreach (var lastName in lastNames)
                {
                    users.Add(new User($"{firstName} {lastName}"));
                }
            }
            ForceGC();
            dotMemory.Check(memory => WriteLine(memory.SizeInBytes));
            WriteLine(users.Count);
        }

        [Test]  //  11876967
        public void TestUser2()
        {
            var firstNames = Enumerable.Range(0, 300).Select(x => RandomStrings()).ToList();
            var lastNames = Enumerable.Range(0, 300).Select(x => RandomStrings()).ToList();

            var users = new List<User2>();

            foreach (var firstName in firstNames)
            {
                foreach (var lastName in lastNames)
                {
                    users.Add(new User2($"{firstName} {lastName}"));
                }
            }
            ForceGC();
            dotMemory.Check(memory => WriteLine(memory.SizeInBytes));
            WriteLine(users.Count);
        }

        private void ForceGC()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        private string RandomStrings()
        {
            Random rand = new Random();
            return new string(Enumerable.Range(0, 10).Select(i => (char)('a' + rand.Next(26)))
                .ToArray());
        }
    }
}
