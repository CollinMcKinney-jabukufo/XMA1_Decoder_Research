using Jabukufo.Common;
using System;
using System.IO;

namespace Jabukufo.Bits
{
    public class BitStream
    {
        public MemoryStream BaseStream { get; }
        public int BitOffset { get; set; }
        public int BitLength { get; }

        #region Constructors
        public BitStream(byte[] data, int bitOffset = 0)
        {
            this.BaseStream = new MemoryStream(data.Length);
            this.BaseStream.Write(data, 0, data.Length);
            this.BitOffset = bitOffset;
            this.BitLength = data.Length * BitMath.SizeOf<byte>();
        }

        private BitStream(BitStream parentView, int bitOffset, int bitLength, bool isRelOffset)
        {
            this.BaseStream = parentView.BaseStream;
            this.BitOffset = isRelOffset ? parentView.BitOffset + bitOffset : bitOffset;
            this.BitLength = (bitLength <= 0) ? parentView.BitLength - this.BitOffset : bitLength;
        }

        private BitStream()
        {
            this.BaseStream = new MemoryStream();
            this.BitOffset = 0;
            this.BitLength = 0;
        }
        #endregion

        public BitStream Split(int bitLength)
        {
            var resultPadding = this.BitOffset % 8;
            var data = this.ReadBytes(bitLength);
            var result = new BitStream(data, resultPadding);
            return result;
        }

        public BitStream CreateView(int bitLength, int bitOffset = 0, bool isRelOffset = true)
            => new BitStream(this, bitOffset, bitLength, isRelOffset);

        public void CopyTo(BitStream dest, int bitCount)
        {
            var buffer = this.ReadBytes(bitCount);
            dest.WriteBytes(buffer, bitCount);
        }

        public T ReadValue<T>(Endianness endianness = Endianness.LE_LSB) where T : unmanaged
            => this.ReadValue<T>(BitMath.SizeOf<T>(), endianness);

        public unsafe T ReadValue<T>(int bitCount, Endianness endianness = Endianness.LE_LSB) where T : unmanaged
        {
            var byteCount = BitMath.RoundBitsUp<byte>(bitCount);
            var buffer = (this as BitStream).ReadBytes(bitCount, byteCount, endianness);

            fixed (byte* pBuffer = buffer)
                return *(T*)pBuffer;
        }

        public byte[] ReadBytes(int bitCount, int resultSizeInBytes = 0, Endianness endianness = Endianness.LE_LSB)
        {
            var byteCount = BitMath.RoundBitsUp<byte>(bitCount);
            var readCount = BitMath.RoundBitsUp<byte>((this.BitOffset % 8) + bitCount);

            var buffer = new byte[readCount];
            this.BaseStream.Position = this.BitOffset / 8;
            this.BaseStream.Read(buffer, 0, readCount);

            var leftAlign = this.BitOffset % 8;
            var rightAlign = (8 - ((this.BitOffset + bitCount) % 8)) % 8;

            buffer[0] <<= leftAlign;
            buffer[0] >>= leftAlign;
            for (var b = readCount; --b > 0;)
            {
                buffer[b] >>= rightAlign;
                var carry = buffer[b - 1];
                carry <<= (8 - rightAlign);
                buffer[b] |= carry;
            }
            buffer[0] >>= rightAlign;

            resultSizeInBytes = (resultSizeInBytes == 0) ? byteCount : resultSizeInBytes;
            var resultPadding = (resultSizeInBytes - byteCount);
            var bufferPadding = (readCount - byteCount);
            var result = new byte[resultSizeInBytes];
            Array.ConstrainedCopy(buffer, bufferPadding, result, resultPadding, byteCount);

            if ((endianness & Endianness.MSB) != 0)
                for (var b = 0; b < result.Length; b++)
                    result[b] = BitMath.ReverseBits(result[b]);

            if ((endianness & Endianness.BE) != 0 && BitConverter.IsLittleEndian)
                Array.Reverse(result, 0, result.Length);
            else if ((endianness & Endianness.LE) != 0 && !BitConverter.IsLittleEndian)
                Array.Reverse(result, 0, result.Length);

            this.BitOffset += bitCount;
            return result;
        }

        public void WriteBytes(byte[] data, int bitCount)
        {
            var bitAlignment = BitMath.SizeOf<byte>();

            var leftLength = this.BitOffset % bitAlignment;
            var rightLength = (bitAlignment - leftLength) % bitAlignment;

            if (leftLength != 0)
            {
                this.BaseStream.Position = this.BitOffset / 8;
                var firstByte = (byte)this.BaseStream.ReadByte();
                var carry = data[0];
                carry <<= leftLength;
                carry >>= leftLength;
                firstByte |= carry;
                this.BaseStream.Position--;
                this.BaseStream.WriteByte(firstByte);

                for (var b = 1; (b + 1) < data.Length; b++)
                {
                    data[b] <<= rightLength;
                    carry = data[b + 1];
                    carry <<= leftLength;
                    carry >>= leftLength;
                    data[b] |= carry;
                }
                data[data.Length - 1] <<= rightLength;
            }

            var finalLength = BitMath.RoundBitsUp<byte>(bitCount - rightLength);
            this.BaseStream.Write(data, 0, finalLength);
            this.BitOffset += bitCount;
        }

        public BitContext ReadContext(int bitCount)
        {
            var buffer = new bool[bitCount];

            for (var b = 0; b < bitCount; b++)
                buffer[b] = ReadValue<bool>(1);

            var result = new BitContext(buffer);
            return result;
        }
    }
}
