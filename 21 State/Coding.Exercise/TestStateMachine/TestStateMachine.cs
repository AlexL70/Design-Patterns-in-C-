using System;
using Coding.Exercise;
using Xunit;

namespace TestStateMachine
{
    public class TestStateMachine
    {
        [Fact]
        public void Success()
        {
            var cl = new CombinationLock(new int[] {1, 2, 3, 4, 5});
            Assert.Equal("LOCKED", cl.Status);
            cl.EnterDigit(1);
            Assert.Equal("1", cl.Status);
            cl.EnterDigit(2);
            Assert.Equal("12", cl.Status);
            cl.EnterDigit(3);
            Assert.Equal("123", cl.Status);
            cl.EnterDigit(4);
            Assert.Equal("1234", cl.Status);
            cl.EnterDigit(5);
            Assert.Equal("OPEN", cl.Status);
        }

        [Fact]
        public void Error()
        {
            var cl = new CombinationLock(new int[] { 1, 2, 3, 4, 5 });
            Assert.Equal("LOCKED", cl.Status);
            cl.EnterDigit(1);
            Assert.Equal("1", cl.Status);
            cl.EnterDigit(2);
            Assert.Equal("12", cl.Status);
            cl.EnterDigit(4);
            Assert.Equal("ERROR", cl.Status);
        }
    }
}
