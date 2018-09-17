using NUnit.Framework;
using SingletoneImplementation;

namespace SingletonImplTest
{
    [TestFixture]
    public class SingletonTest
    {
        [Test]
        public void IsSingletonTest()
        {
            var db = SingletonDatabase.Instance;
            var db2 = SingletonDatabase.Instance;
            Assert.That(db, Is.SameAs(db2));
            Assert.That(SingletonDatabase.Count, Is.EqualTo(1));
        }

        [Test]
        public void SingletonTotalPopulationTest()
        {
            var rf = new SingletonRecordFinder();
            var names = new string[] {"Seoul", "Mexico City"};
            int total = rf.GetTotalPopulation(names);
            Assert.That(total, Is.EqualTo(34900000));
        }
    }

}
