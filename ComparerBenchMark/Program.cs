using System;
using BenchmarkDotNet.Attributes;
using Yinyue200.EasyComparer;
using System.Collections.Generic;
using BenchmarkDotNet.Running;

namespace ComparerBenchMark
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<OvHashCodeTest>();
            BenchmarkRunner.Run<OveqTest>();

        }
    }
    public class OvHashCodeTest
    {
        string str = "test";
        static IEqualityComparer<String> a = EqualityComparer<string>.Default.OverrideHashCodeMethod(b => b.GetHashCode());
        static IEqualityComparer<String> b = EasyComparerBuilder.BuildEqualAndGetHashCodeMethod<string>((a, b) => false, a => a.GetHashCode());
        [Benchmark]
        public void Ovhashcode()
        {
            a.GetHashCode(str);
        }
        [Benchmark]
        public void basic()
        {
            str.GetHashCode();
        }
        [Benchmark]
        public void bd()
        {
            b.GetHashCode(str);
        }
        [Benchmark]
        public void df()
        {
            EqualityComparer<string>.Default.GetHashCode();
        }
    }
    public class OveqTest
    {
        string str = "test";
        static IEqualityComparer<String> a = EqualityComparer<string>.Default.OverrideHashCodeMethod(b => b.GetHashCode());
        static IEqualityComparer<String> b = EasyComparerBuilder.BuildEqualMethod<string>((a, b) => a.Equals(b));
        [Benchmark]
        public void Ovhashcode()
        {
            a.Equals(str,string.Empty);
        }
        [Benchmark]
        public void basic()
        {
            str.Equals(string.Empty);
        }
        [Benchmark]
        public void bd()
        {
            b.Equals(str, string.Empty);
        }
        [Benchmark]
        public void df()
        {
            EqualityComparer<string>.Default.Equals(str,string.Empty);
        }
    }
    class Test
    {
        [Benchmark]
        public void Ovhashcode()
        {
            var rs = new Random().Next();
            var a = EqualityComparer<string>.Default.OverrideHashCodeMethod(b => rs);
            a.GetHashCode(string.Empty);
        }
        [Benchmark]
        public void Ovequal()
        {
            var rs = new Random().Next();
            var a = EqualityComparer<string>.Default.OverrideEqualMethod((c, b) => false);
            a.Equals(string.Empty, string.Empty);
            a.GetHashCode(string.Empty);
        }
        [Benchmark]
        public void Buildequal()
        {
            var c = EasyComparerBuilder.BuildEqualMethod<string>((a, b) => false);
            c.Equals(string.Empty, string.Empty);
        }
        [Benchmark]
        public void Buildhashcode()
        {
            var rs = new Random().Next();
            var c = EasyComparerBuilder.BuildEqualAndGetHashCodeMethod<string>((a, b) => false, a => rs);
            c.Equals(string.Empty, string.Empty);
            c.GetHashCode(string.Empty);
        }
    }
}
