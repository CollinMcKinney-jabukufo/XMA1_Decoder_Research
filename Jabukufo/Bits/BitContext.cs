using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Jabukufo.Bits
{
    public class BitContext
    {
        private readonly IList<bool> Bits;

        public BitContext(byte[] data)
        {
            var bits = new bool[data.Length * BitMath.BitCount(data.Length)];
            new BitArray(data).CopyTo(bits, 0);
            this.Bits = bits;
        }

        public BitContext(bool[] bits)
        {
            this.Bits = bits;
        }

        public IList<bool> GetBits(int index, int count)
        {
            var res = new bool[count];
            for (var b = 0; b < count; b++)
                res[b] = this.Bits[index + b];
            return res;
        }

        public void InsertBits(IList<bool> bits, int index)
        {
            for (var b = 0; b < bits.Count; b++)
                this.Bits.Insert(index + b, bits[b]);
        }

        public void AddBits(IList<bool> bits)
        {
            foreach (var bit in bits)
                this.Bits.Add(bit);
        }

        public unsafe T[] ToArray<T>() where T : unmanaged
        {
            var bitBuffer = this.Bits.ToArray();
            var byteLength = BitMath.RoundBitsUp<byte>(bitBuffer.Length);
            var byteBuffer = new byte[byteLength];

            var bitArray = new BitArray(bitBuffer);
            bitArray.CopyTo(byteBuffer, 0);

            var resultLength = BitMath.RoundBitsUp<T>(bitBuffer.Length);
            var resultBuffer = new T[resultLength];

            fixed (void* pByteBuffer = byteBuffer, pResultBuffer = resultBuffer)
                Buffer.MemoryCopy(pByteBuffer, pResultBuffer, (resultLength * sizeof(T)), byteLength);

            return resultBuffer;
        }

        public unsafe T Cast<T>() where T : unmanaged
        {
            throw new NotImplementedException();
        }
    }
}
