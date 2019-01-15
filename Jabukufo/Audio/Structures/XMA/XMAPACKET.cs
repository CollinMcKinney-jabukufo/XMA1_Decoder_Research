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
        /// <summary>
        /// Number of XMA frames that begin in this packet. 6-bits.
        /// </summary>
        public int FrameCount;

        /// <summary>
        /// Bit of XmaData where the first complete frame begins. 15-bits.
        /// </summary>
        public int FrameOffsetInBits;

        /// <summary>
        ///  Metadata stored in the packet (always 1 for XMA2). 3-bits.
        /// </summary>
        public int PacketMetaData;

        /// <summary>
        /// How many packets belonging to other streams must be skipped to find the next packet belonging to the same
        /// stream as this one. 8-bits.
        /// </summary>
        public int PacketSkipCount;

        /// <summary>
        /// XMA encoded data.
        /// </summary>
        public XMAFRAME[] XMAFrames;

        public XMAPACKET(BitContext blockContext, XMAFILE xmaFile)
        {
            Debug.WriteLine(typeof(XMAPACKET).FullName);
            Debug.Indent();

            var packetContext = blockContext.GetBits(Constants.XMA_BITS_PER_PACKET, false);
            var packetHeader = packetContext.ReadValue<int>(Endianness.LE_MSB);

            // E.g. if the first DWORD of a packet is 0x30107902:
            //
            // 001100 000001000001111 001 00000010
            //    |          |         |      |____ Skip 2 packets to find the next one for this stream
            //    |          |         |___________ XMA signature (always 000)
            //    |          |_____________________ First frame starts 527 bits into packet
            //    |________________________________ Packet contains 12 frames
            this.FrameCount         = (packetHeader >> 26) & 0b111111;
            Debug.WriteLine($"FrameCount: {this.FrameCount}");

            this.FrameOffsetInBits  = (packetHeader >> 11) & 0b111111111111111;
            Debug.WriteLine($"FrameOffsetInBits: {this.FrameOffsetInBits}");

            this.PacketMetaData     = (packetHeader >> 08) & 0b111;
            Debug.WriteLine($"PacketMetaData: {this.PacketMetaData}");
            Assert.Debug(this.PacketMetaData == 0);

            this.PacketSkipCount    = (packetHeader >> 00) & 0b11111111;
            Debug.WriteLine($"PacketSkipCount: {this.PacketSkipCount}");

            /// TODO: Figure out what's wrong with this `FrameOffsetInBits` offset. Current code assumes offset to be relative to the
            /// end of the packet header (32 bits into the packet).
    //        if (this.FrameOffsetInBits != 0)
    //        {
    //            var previousFrameBits = packetContext.GetBits(this.FrameOffsetInBits - BitMath.SizeOf<uint>(), false);
    //            // TODO: add this onto the previous frame
    //        }
    //
    //        var xmaFrames = new List<XMAFRAME> { };
    //        for (var f = 0; f < this.FrameCount; f++)
    //        {
    //            Debug.Write($"{nameof(this.XMAFrames)}[{f}]");
    //            var frame = new XMAFRAME(packetContext, xmaFile);
    //            if (frame.FrameLength == 0)
    //                break;
    //
    //            xmaFrames.Add(frame);
    //        }
    //        this.XMAFrames = xmaFrames.ToArray();

            Debug.Unindent();
        }

        private static unsafe int GetXmaPacketFrameCount(byte* pPacket)
        {
            return (int)(pPacket[0] >> 2);
        }

        private static unsafe int GetXmaPacketFirstFrameOffsetInBits(byte* pPacket)
        {
            return ((int)(pPacket[0] & 0x3) << 13) |
                   ((int)(pPacket[1]) << 5) |
                   ((int)(pPacket[2]) >> 3);
        }

        private static unsafe int GetXmaPacketMetadata(byte* pPacket)
        {
            return (int)(pPacket[2] & 0x7);
        }

        private static unsafe int GetXmaPacketSkipCount(byte* pPacket)
        {
            return (int)(pPacket[3]);
        }
    }
}
