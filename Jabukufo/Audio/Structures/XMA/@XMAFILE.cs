using Jabukufo.Bits;
using System;
using System.Diagnostics;

namespace Jabukufo.Audio.Structures.XMA
{
    public unsafe class XMAFILE
    {
        public CHUNK_Riff RiffSubchunk;
        public CHUNK_Format FormatSubchunk;
        public CHUNK_Data DataSubchunk;
        public CHUNK_Seek SeekSubchunk;

        public XMASTREAM[] XMAStreams;

        public XMAFILE(byte[] xmaFileData)
        {
            var xmaStream = new BitStream(xmaFileData);

            this.RiffSubchunk   = new CHUNK_Riff(xmaStream, this);
            this.FormatSubchunk = new CHUNK_Format(xmaStream, this);
            this.DataSubchunk   = new CHUNK_Data(xmaStream, this);
            this.SeekSubchunk   = new CHUNK_Seek(xmaStream, this);

            return;
        }

        public void BeginDecode()
        {
            this.XMAStreams = new XMASTREAM[this.FormatSubchunk.XMAWAVEFormat.NumStreams];

            // Create the streams.
            for (var s = 0; s < this.XMAStreams.Length; s++)
                this.XMAStreams[s] = new XMASTREAM();

            // Decode the first packet for each stream.
            for (var s = 0; s < this.XMAStreams.Length; s++)
            {
                Debug.WriteLine($"Decoding First {nameof(XMAPACKET)} In {nameof(XMAStreams)}[{s}]");
                Debug.Indent();

                this.DataSubchunk.XMAData.BitOffset = Constants.XMA_BITS_PER_PACKET * s;
                var packet = new XMAPACKET(this.DataSubchunk.XMAData, this);
                this.XMAStreams[s].XMAPackets.Add(packet);

                Debug.Unindent();
            }

            // Decode the remainder of each stream.
            for (var s = 0; s < this.XMAStreams.Length; s++)
            {
                Debug.WriteLine($"Decoding Remaining {nameof(XMAPACKET)}s in {nameof(XMAStreams)}[{s}]");
                Debug.Indent();

                this.DecodeStream(s);

                Debug.Unindent();
            }
        }

        private void DecodeStream(int s)
        {
            var packet = this.XMAStreams[s].XMAPackets[0];
            var skipCount = packet.PacketSkipCount;

            while (this.DataSubchunk.XMAData.BitOffset < this.DataSubchunk.XMAData.BitLength)
            {
                this.DataSubchunk.XMAData.BitOffset = packet.BitOffset + (Constants.XMA_BITS_PER_PACKET * (skipCount + 1));

                packet = new XMAPACKET(this.DataSubchunk.XMAData, this);
                this.XMAStreams[s].XMAPackets.Add(packet);
                skipCount = packet.PacketSkipCount;
            }

            for (var p = 0; p < this.XMAStreams[s].XMAPackets.Count; p++)
            {
                for (var f = 0; f < this.XMAStreams[s].XMAPackets[p].XMAFrames.Length; f++)
                {

                }
            }
        }
    }
}
