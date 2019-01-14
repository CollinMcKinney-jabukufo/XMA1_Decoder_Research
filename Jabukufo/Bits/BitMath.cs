using System;

namespace Jabukufo.Bits
{
    public static class BitMath
    {
        /// <summary>
        /// Calculates the size in bits of type 'T'.
        /// </summary>
        public unsafe static int SizeOf<T>() where T : unmanaged
            => (sizeof(T) * 8);

        /// <summary>
        /// Calculates an amount of bits from an amount of bytes (byteCount * <see cref="BitMath.SizeOf{T}"/> where T : byte)
        /// </summary>
        public static int BitCount(int byteCount)
            => byteCount * BitMath.SizeOf<byte>();

        /// <summary>
        /// Calculates an amount of bytes from an amount of bits (bitCount / <see cref="BitMath.SizeOf{T}"/> where T : byte)
        /// <para>
        /// Consider using <see cref="BitMath.RoundBitsUp{T}(int)"/> OR <see cref="BitMath.RoundBitsDown{T}(int)"/> if there is
        /// a chance that the 'bitCount' argument is non-byte-aligned.
        /// </para>
        /// </summary>
        public static int ByteCount(int bitCount)
        {
            if (bitCount % BitMath.SizeOf<byte>() != 0)
                throw new DataMisalignedException();
            return bitCount / BitMath.SizeOf<byte>();
        }

        /// <summary>
        /// Rounds a `bitCount` *UP* to the minimum amount of 
        /// type 'T' required to contain that amount of bits.
        /// </summary>
        public static int RoundBitsUp<T>(int bitCount) where T : unmanaged
            => (bitCount + (BitMath.SizeOf<T>() - 1)) / BitMath.SizeOf<T>();

        /// <summary>
        /// Rounds a `bitCount` *DOWN* to the maximum amount of 
        /// type 'T' which can be filled with that amount of bits.
        /// </summary>
        public static int RoundBitsDown<T>(int bitCount) where T : unmanaged
            => bitCount / BitMath.SizeOf<T>();
        
        /// <summary>
        /// Calculates how many additional bits are needed to align to type 'T'.
        /// </summary>
        public static int CalcUnderflow<T>(int bitCount) where T : unmanaged
            => (BitMath.RoundBitsUp<T>(bitCount) * BitMath.SizeOf<T>()) - bitCount;

        /// <summary>
        /// Calculates how many additional bits are needed to align to type 'T'.
        /// </summary>
        public static int CalcOverflow<T>(int bitCount) where T : unmanaged
            => bitCount - (BitMath.RoundBitsDown<T>(bitCount) * BitMath.SizeOf<T>());

        /// <summary>
        /// Reverses the order of bits within a single byte.
        /// http://graphics.stanford.edu/~seander/bithacks.html#ReverseByteWith64Bits
        /// </summary>
        public static byte ReverseBits(byte input)
        {
            ulong ret = input;
            ret *= 0x0080200802;
            ret &= 0x0884422110;
            ret *= 0x0101010101;
            ret >>= 32;
            return (byte)ret;
        }

        /// <summary>
        /// Reverses the order of bits in each byte within an entire structure.
        /// </summary>
        public unsafe static T ReverseBits<T>(T input) where T : unmanaged
        {
            for (var b = 0; b < sizeof(T); b++)
                (&input)[b] = BitMath.ReverseBits((&input)[b]);
            return input;
        }

        /// <summary>
        /// Reverses the order of bytes within an entire structure.
        /// </summary>
        public unsafe static T ReverseBytes<T>(T input) where T : unmanaged
        {
            for (var b = 0; b < sizeof(T); b++)
            {
                var swap = (&input)[sizeof(T) - b];
                (&input)[sizeof(T) - b] = (&input)[b];
                (&input)[b] = swap;
            }
            return input;
        }

        public unsafe static void ShiftLeft(byte[] array, int shift)
        {
            for (var b = 0; (b + 1) < array.Length; b++)
            {
                array[b] <<= shift;         // Shift the first byte.
                var carry = array[b + 1];   // Carry the next byte.
                carry >>= shift;            // Shift the carry right -
                carry <<= shift;            // - and then left to remove insignificant bits.
                array[b] |= carry;          // OR the carry onto the first byte.
            }
            array[array.Length - 1] <<= shift; // Shift the final byte.
        }
    }
}
