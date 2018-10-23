using System;
using Xunit;
using Coding.Exercise;

namespace VisitorTest
{
    public class VTest
    {
        [Fact]
        public void SimpleAddition()
        {
            var simple = new AdditionExpression(new Value(2), new Value(3));
            var ep = new ExpressionPrinter();
            ep.Visit(simple);
            Assert.Equal("(2+3)", ep.ToString());
        }
    }
}
