using Jabukufo.Bits;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Jabukufo.Audio.Structures.XMA
{
    public unsafe class XMAFILE
    {
        public CHUNK_Riff RiffSubchunk;
        public CHUNK_Format FormatSubchunk;
        public CHUNK_Data DataSubchunk;
        public CHUNK_Seek SeekSubchunk;

        public XMAFILE(byte[] xmaFileData)
        {
            var metaStream = new BitStream(xmaFileData);

            this.RiffSubchunk   = new CHUNK_Riff(metaStream, this);
            this.FormatSubchunk = new CHUNK_Format(metaStream, this);
            this.DataSubchunk   = new CHUNK_Data(metaStream, this);
            this.SeekSubchunk   = new CHUNK_Seek(metaStream, this);

            return;
        }

        public readonly List<XMAPACKET> XMAPackets = new List<XMAPACKET> { };
        public readonly List<XMAFRAME> XMAFrames = new List<XMAFRAME> { };
        public void BeginDecode()
        {
            this.DecodePackets();
            this.DecodeFrames();

            ;
        }

        private void DecodePackets()
        {
            for (var p = 0; p < (this.DataSubchunk.XMADataStream.BitLength / Constants.XMA_BITS_PER_PACKET); p++)
            {
                Debug.WriteLine($"Decoding {nameof(XMAPACKET)} [{p}]");
                Debug.Indent();

                var packetContext = this.DataSubchunk.XMADataStream.ReadContext(Constants.XMA_BITS_PER_PACKET);
                var packet = new XMAPACKET(packetContext, this);
                this.XMAPackets.Add(packet);

                Debug.Unindent();
            }
        }

        private void DecodeFrames()
        {
            foreach (var packet in this.XMAPackets)
            {
                packet.PacketContext.BitOffset = Constants.XMA_PACKET_HEADER_BITS;

                if (packet.FrameOffsetInBits != 0 && this.XMAFrames.Count != 0)
                {
                    Debug.WriteLine($"Adding bits to {nameof(XMAFRAME)} [{this.XMAFrames.Count - 1}]");
                    Debug.Indent();

                    var previousFrameBits = packet.PacketContext.GetBits(packet.FrameOffsetInBits, false);
                    this.XMAFrames[this.XMAFrames.Count - 1].FrameData.Append(previousFrameBits);

                    Debug.WriteLine($"Add Amount: {previousFrameBits.BitLength}");
                    Debug.WriteLine($"Final FrameLength: {this.XMAFrames[this.XMAFrames.Count - 1].FrameData.BitLength}");
                    Debug.Unindent();
                }

                while (this.XMAFrames.Count < packet.FrameCount)
                {
                    Debug.WriteLine($"Getting bits for {nameof(XMAFRAME)}");
                    Debug.Indent();

                    var frame = new XMAFRAME(packet.PacketContext, this);
                    this.XMAFrames.Add(frame);

                    Debug.WriteLine($"Initial FrameLength: {this.XMAFrames[this.XMAFrames.Count - 1].FrameData.BitLength}");
                    Debug.Unindent();
                }
            }

            for (var f = 0; f < this.XMAFrames.Count; f++)
            {
                Debug.WriteLine($"Decoding {nameof(XMAFRAME)} [{f}]");
                Debug.Indent();

                ///
                /// Frame decoding here
                ///

                Debug.Unindent();
            }
        }
    }
}
