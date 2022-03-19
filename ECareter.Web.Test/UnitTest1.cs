using FluentAssertions;
using System;
using Xunit;

namespace ECareter.Web.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var a = 2;
            var b = 2;
            var c = a + b;
            c.Should().Be(4);
        }
    }
}
