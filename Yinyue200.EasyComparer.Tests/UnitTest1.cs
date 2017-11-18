using System;
using System.Collections.Generic;
using Xunit;

namespace Yinyue200.EasyComparer.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Ovhashcode()
        {
            var rs = new Random().Next();
            var a = EqualityComparer<string>.Default.OverrideHashCodeMethod(b=>rs);
            Assert.Equal(rs, a.GetHashCode(string.Empty));
        }
        [Fact]
        public void Ovequal()
        {
            var rs = new Random().Next();
            var a = EqualityComparer<string>.Default.OverrideEqualMethod((c, b) => false);
            Assert.False(a.Equals(string.Empty,string.Empty));
            Assert.Equal(string.Empty.GetHashCode(), a.GetHashCode(string.Empty));

        }
        [Fact]
        public void Buildequal()
        {
            var c=EasyComparerBuilder.BuildEqualMethod<string>((a, b) => false);
            Assert.False(c.Equals(string.Empty, string.Empty));
        }
        [Fact]
        public void Buildhashcode()
        {
            var rs = new Random().Next();
            var c = EasyComparerBuilder.BuildEqualAndGetHashCodeMethod<string>((a, b) => false, a => rs);
            Assert.False(c.Equals(string.Empty, string.Empty));
            Assert.Equal(rs, c.GetHashCode(string.Empty));
        }
    }
}
