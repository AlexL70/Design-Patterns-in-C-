using Coding.Exercise;
using NUnit.Framework;

namespace GoblinTest
{
    [TestFixture]
    public class GoblinTest
    {
        [Test]
        public void Test()
        {
            var game = new Game();
            var goblin = new Goblin(game);
            game.Creatures.Add(goblin);
            Assert.That(goblin.Attack, Is.EqualTo(1));
            Assert.That(goblin.Defense, Is.EqualTo(1));
            var g2 = new Goblin(game);
            game.Creatures.Add(g2);
            Assert.That(g2.Attack, Is.EqualTo(1));
            Assert.That(g2.Defense, Is.EqualTo(2));
            var king = new GoblinKing(game);
            game.Creatures.Add(king);
            Assert.That(g2.Attack, Is.EqualTo(2));
            Assert.That(g2.Defense, Is.EqualTo(3));
            Assert.That(king.Attack, Is.EqualTo(3));
            Assert.That(king.Defense, Is.EqualTo(5));
        }
    }
}
