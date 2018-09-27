using NUnit.Framework;
using Coding.Exercise;

namespace ParserUnitTest
{
    [TestFixture]
    public class ExpressionProcessorTest
    {
        private ExpressionProcessor ep;

        [SetUp]
        public void SetUp()
        {
            ep = new ExpressionProcessor();
        }

        [Test]
        public void TestOneValue()
        {
            ep.Variables.Add('y', 7);
            var result = ep.Calculate("y");
            Assert.That(result, Is.EqualTo(7));
            result = ep.Calculate("8");
            Assert.That(result, Is.EqualTo(8));
        }

        [Test]
        public void TestConstants()
        {
            var result = ep.Calculate("1 + 2 + 3");
            Assert.That(result, Is.EqualTo(6));
        }

        [Test]
        public void TextVar()
        {
            ep.Variables['x'] = 3;
            var result = ep.Calculate("10 - 2 - x");
            Assert.That(result, Is.EqualTo(5));
        }

        [Test]
        public void TextError()
        {
            var result = ep.Calculate("1 + 2 + xy");
            Assert.That(result, Is.EqualTo(0));
        }
    }
}
