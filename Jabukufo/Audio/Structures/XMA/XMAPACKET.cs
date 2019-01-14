using Jabukufo.Bits;
using Jabukufo.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Jabukufo.Audio.Structures.XMA
{
    /// <summary>
    /// XMA packet structure (in big-endian form).
    /// Size = <see cref="Constants.XMA_BYTES_PER_PACKET"/>.
    /// https://github.com/xenia-project/libav/blob/9919df9ebe02baa4f4a8082e82d6807680977f34/libavcodec/xma2dec.c#L1495
    /// </summary>
    public class XMAPACKET
    {
        public int BitOffset { get; }

        /// <summary>
        /// Number of XMA frames that begin in this packet. 6-bits.
        /// </summary>
        public byte FrameCount;

        /// <summary>
        /// Bit of XmaData where the first complete frame begins. 15-bits.
        /// </summary>
        public ushort FrameOffsetInBits;

        /// <summary>
        ///  Metadata stored in the packet (always 1 for XMA2). 3-bits.
        /// </summary>
        public byte PacketMetaData;

        /// <summary>
        /// How many packets belonging to other streams must be skipped to find the next packet belonging to the same
        /// stream as this one. 8-bits.
        /// </summary>
        public byte PacketSkipCount;

        /// <summary>
        /// XMA encoded data.
        /// </summary>
        public XMAFRAME[] XMAFrames;

        public XMAPACKET(BitStream xmaStream, XMAFILE xmaFile)
        {
            this.BitOffset = xmaStream.BitOffset;

            Debug.WriteLine(typeof(XMAPACKET).FullName);
            Debug.Indent();

            var packetHeader = xmaStream.ReadValue<uint>(Endianness.BE);
            xmaStream.BitOffset = this.BitOffset;
            Debug.WriteLine($"--Header-Bits: {Convert.ToString(packetHeader, 2).PadLeft(32, '0')}--");
            Debug.WriteLine($"{nameof(this.BitOffset)}: {this.BitOffset}");

            // E.g. if the first DWORD of a packet is 0x30107902:
            //
            // 001100 000001000001111 001 00000010
            //    |          |         |      |____ Skip 2 packets to find the next one for this stream
            //    |          |         |___________ XMA signature (always 000)
            //    |          |_____________________ First frame starts 527 bits into packet
            //    |________________________________ Packet contains 12 frames
            this.FrameCount         = xmaStream.ReadValue<byte>(6);
            Debug.WriteLine($"FrameCount: {this.FrameCount}");

            this.FrameOffsetInBits  = xmaStream.ReadValue<ushort>(15);
            Debug.WriteLine($"FrameOffsetInBits: {this.FrameOffsetInBits}");

            this.PacketMetaData     = xmaStream.ReadValue<byte>(3);
            Debug.WriteLine($"PacketMetaData: {this.PacketMetaData}");
            Assert.Debug(this.PacketMetaData == 0);

            this.PacketSkipCount    = xmaStream.ReadValue<byte>(8);
            Debug.WriteLine($"PacketSkipCount: {this.PacketSkipCount}");

            //this.PacketData = xmaStream.ReadBytes(Constants.XMA_BITS_PER_PACKET - BitMath.SizeOf<int>());

            var xmaFrames = new List<XMAFRAME> { };
            xmaStream.BitOffset = this.BitOffset + this.FrameOffsetInBits;

            //for (var f = 0; xmaStream.BitOffset < this.BitOffset + Constants.XMA_BITS_PER_PACKET; f++)
            {
                Debug.Write($"{nameof(this.XMAFrames)}[{0}]");
                var frame = new XMAFRAME(xmaStream, xmaFile);
                xmaFrames.Add(frame);
            }
            this.XMAFrames = xmaFrames.ToArray();

            xmaStream.BitOffset = this.BitOffset + Constants.XMA_BITS_PER_PACKET;
            Debug.Unindent();
        }
    }
}
