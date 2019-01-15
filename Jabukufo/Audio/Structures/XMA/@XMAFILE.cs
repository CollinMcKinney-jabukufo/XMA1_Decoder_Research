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
            var metaContext = new BitContext(xmaFileData);

            this.RiffSubchunk   = new CHUNK_Riff(metaContext, this);
            this.FormatSubchunk = new CHUNK_Format(metaContext, this);
            this.DataSubchunk   = new CHUNK_Data(metaContext, this);
            this.SeekSubchunk   = new CHUNK_Seek(metaContext, this);

            return;
        }

        public void DecodeBlocks()
        {
            this.XMAStreams = new XMASTREAM[this.SeekSubchunk.NumStreams];
            var blockLength = this.SeekSubchunk.NumStreams * Constants.XMA_BITS_PER_PACKET;

            for (var b = 0; b < this.SeekSubchunk.BlockTableCount; b++)
            {
                this.DataSubchunk.XMAContext.BitOffset = b * blockLength;
                var blockContext = this.DataSubchunk.XMAContext.GetBits(blockLength, false);

                for (var s = 0; s < this.SeekSubchunk.NumStreams; s++)
                {
                    Debug.WriteLine($"Decoding First {nameof(XMAPACKET)} In {nameof(XMAStreams)}[{s}]");
                    Debug.Indent();

                    // Initialize the stream
                    this.XMAStreams[s] = new XMASTREAM();

                    // Decode the packets belonging to the stream
                    var skipCount = 0;
                    while (blockContext.BitOffset < blockContext.BitLength)
                    {
                        blockContext.BitOffset += (Constants.XMA_BITS_PER_PACKET * skipCount);
                        var packet = new XMAPACKET(blockContext, this);
                        this.XMAStreams[s].XMAPackets.Add(packet);
                        skipCount = packet.PacketSkipCount;
                    }

                    Debug.Unindent();
                }
            }
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

                this.DataSubchunk.XMAContext.BitOffset = Constants.XMA_BITS_PER_PACKET * s;
                var packet = new XMAPACKET(this.DataSubchunk.XMAContext, this);
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

            while (this.DataSubchunk.XMAContext.BitOffset < this.DataSubchunk.XMAContext.BitLength)
            {
                this.DataSubchunk.XMAContext.BitOffset += (Constants.XMA_BITS_PER_PACKET * skipCount);

                packet = new XMAPACKET(this.DataSubchunk.XMAContext, this);
                this.XMAStreams[s].XMAPackets.Add(packet);
                skipCount = packet.PacketSkipCount;
            }
        }
    }
}
