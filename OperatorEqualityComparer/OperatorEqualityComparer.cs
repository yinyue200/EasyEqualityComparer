using System;
using System.Collections.Generic;
using System.Text;

namespace Yinyue200.EasyComparer
{
    internal class EasyEqualityComparer<T> : IEqualityComparer<T>
    {
        public EasyEqualityComparer(Func<T, T, bool> func)
        {
            this.func = func;
        }
        readonly Func<T, T, bool> func;
        public bool Equals(T x, T y) => func(x, y);

        public int GetHashCode(T obj) => obj.GetHashCode();
    }
    internal class EasyHashCodeEqualityComparer<T> : IEqualityComparer<T>
    {
        public EasyHashCodeEqualityComparer(Func<T, T, bool> func, Func<T, int> func2)
        {
            this.func = func;
            this.func2 = func2;
        }
        readonly Func<T, T, bool> func;
        readonly Func<T, int> func2;

        public bool Equals(T x, T y) => func(x, y);

        public int GetHashCode(T obj) => func2(obj);
    }
    public static class EasyComparerBuilder
    {
        public static IEqualityComparer<T> BuildEqualMethod<T>(Func<T, T, bool> func) => new EasyEqualityComparer<T>(func);
        public static IEqualityComparer<T> BuildEqualAndGetHashCodeMethod<T>(Func<T, T, bool> func, Func<T, int> func2) => new EasyHashCodeEqualityComparer<T>(func,func2);

        public static IEqualityComparer<T> OverrideEqualMethod<T>(this IEqualityComparer<T> equalityComparer, Func<T, T, bool> func) => BuildEqualAndGetHashCodeMethod(func, equalityComparer.GetHashCode);
        public static IEqualityComparer<T> OverrideHashCodeMethod<T>(this IEqualityComparer<T> equalityComparer, Func<T, int> func) => BuildEqualAndGetHashCodeMethod(equalityComparer.Equals, func);

    }
}
