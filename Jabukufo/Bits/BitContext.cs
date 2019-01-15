using Jabukufo.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Jabukufo.Bits
{
    public class BitContext
    {
        private readonly IList<bool> Bits;

        public int BitOffset;

        public int BitLength => this.Bits.Count;

        public BitContext(byte[] data)
        {
            var bits = new bool[data.Length * BitMath.BitCount(data.Length)];
            new BitArray(data).CopyTo(bits, 0);
            this.Bits = bits;
        }

        public BitContext(IList<bool> bits)
        {
            this.Bits = bits.ToList();
        }

        public BitContext GetBits(int count, bool printOutput = true)
        {
            Debug.WriteLine(this.BitOffset);

            var result = new bool[count];
            for (var b = 0; b < count; b++)
            {
                result[b] = this.Bits[this.BitOffset + b];

                if (printOutput)
                {
                    char c = result[b] ? '1' : '0';
                    Debug.Write(c);
                }
            }

            if (printOutput)
                Debug.WriteLine("");

            this.BitOffset += count;
            return new BitContext(result);
        }

        public void InsertBits(BitContext srcContext, int index)
        {
            for (var b = 0; b < srcContext.Bits.Count; b++)
                this.Bits.Insert(index + b, srcContext.Bits[b]);
        }

        public void Concat(BitContext srcContext)
        {
            this.Bits.Concat(srcContext.Bits);
        }

        public unsafe T[] ToArray<T>(Endianness endianness = Endianness.LE_LSB) where T : unmanaged
        {
            var bitBuffer = this.Bits.ToArray();
            var byteLength = BitMath.RoundBitsUp<byte>(bitBuffer.Length);
            var byteBuffer = new byte[byteLength];

            if ((endianness & Endianness.MSB) == Endianness.MSB)
                Array.Reverse(bitBuffer);

            var bitArray = new BitArray(bitBuffer);
            bitArray.CopyTo(byteBuffer, 0);

            if ((endianness & Endianness.BE) == Endianness.BE)
                Array.Reverse(byteBuffer);

            var resultLength = BitMath.RoundBitsUp<T>(bitBuffer.Length);
            var resultBuffer = new T[resultLength];

            fixed (void* pByteBuffer = byteBuffer, pResultBuffer = resultBuffer)
                Buffer.MemoryCopy(pByteBuffer, pResultBuffer, (resultLength * sizeof(T)), byteLength);

            return resultBuffer;
        }

        public unsafe T ReadValue<T>(Endianness endianness = Endianness.LE_LSB) where T : unmanaged
            => this.ReadValue<T>(BitMath.SizeOf<T>(), endianness);

        public unsafe T ReadValue<T>(int bitCount, Endianness endianness = Endianness.LE_LSB) where T : unmanaged
        {
            var bits = this.GetBits(bitCount);

            if ((endianness & Endianness.MSB) == Endianness.MSB)
                bits = new BitContext(bits.Bits.Reverse().ToList());

            while (bits.BitLength < BitMath.SizeOf<T>())
                bits.Bits.Insert(0, false);

            var bitBuffer = bits.Bits.ToArray();
            var byteLength = BitMath.RoundBitsUp<byte>(bitBuffer.Length);
            var byteBuffer = new byte[byteLength];

            var bitArray = new BitArray(bitBuffer);
            bitArray.CopyTo(byteBuffer, 0);

            if ((endianness & Endianness.BE) == Endianness.BE)
                Array.Reverse(byteBuffer);

            fixed (void* pByteBuffer = byteBuffer)
                return *(T*)pByteBuffer;
        }
    }
}
