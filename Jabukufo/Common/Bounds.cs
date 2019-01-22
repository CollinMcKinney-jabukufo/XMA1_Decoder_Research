using System;

namespace Jabukufo.Common
{
    public unsafe struct Bounds<T> : IFormattable where T : unmanaged, IFormattable, IComparable
    {
        private readonly T* _value;
        public T Lower => this._value[0];
        public T Upper => this._value[1];
        
        public unsafe Bounds(T lower, T upper)
        {
            // Ensure the 'lower' and 'upper' are actually ordered properly.
            var temp = stackalloc T[2];
            temp[0] = lower.CompareTo(upper) <= 0 ? lower : upper;
            temp[1] = lower.CompareTo(upper) <= 0 ? upper : lower;
            this._value = temp;
        }

        public bool Check(T value) => (value.CompareTo(this.Lower) >= 0) && (value.CompareTo(this.Upper) <= 0);

        public string ToString(string format, IFormatProvider formatProvider)
            => $"[{this.Lower.ToString(format, formatProvider)}..{this.Upper.ToString(format, formatProvider)}]";
    }
}
