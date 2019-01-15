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
            var metaStream = new BitStream(xmaFileData);

            this.RiffSubchunk   = new CHUNK_Riff(metaStream, this);
            this.FormatSubchunk = new CHUNK_Format(metaStream, this);
            this.DataSubchunk   = new CHUNK_Data(metaStream, this);
            this.SeekSubchunk   = new CHUNK_Seek(metaStream, this);

            return;
        }

        public void DecodeBlocks()
        {
            this.XMAStreams = new XMASTREAM[this.SeekSubchunk.NumStreams];
            var blockLength = this.SeekSubchunk.NumStreams * Constants.XMA_BITS_PER_PACKET;

            for (var b = 0; b < this.SeekSubchunk.BlockTableCount; b++)
            {
                this.DataSubchunk.XMADataStream.BitOffset = b * blockLength;
                var blockContext = this.DataSubchunk.XMADataStream.ReadContext(blockLength);

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
    }
}
