using System;

namespace Jabukufo.Common
{
    public struct Bounds<T> where T : IComparable
    {
        public readonly T Lower;
        public readonly T Upper;

        public Bounds(T lower, T upper)
        {
            this.Lower = lower;
            this.Upper = upper;
        }

        public bool Check(T value) => (value.CompareTo(this.Lower) >= 0) && (value.CompareTo(this.Upper) <= 0);
    }
}
