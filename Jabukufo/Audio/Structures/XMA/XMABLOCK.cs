﻿using System.Diagnostics;
using Jabukufo.Bits;

namespace Jabukufo.Audio.Structures.XMA
{
    public class XMABLOCK
    {
        public int BitOffset { get; }

        public XMAPACKET[] XMAPackets;

        public XMABLOCK(BitStream xmaStream, XMAFILE xmaFile)
        {
            this.BitOffset = xmaStream.BitOffset;

            Assert.Debug(xmaFile.DataSubchunk.Header.ChunkSize % xmaFile.SeekSubchunk.BlockTableLength == 0);
            var blockLengthInBytes = xmaFile.DataSubchunk.Header.ChunkSize / xmaFile.SeekSubchunk.BlockTableLength;
            Assert.Debug(blockLengthInBytes == Constants.XMA_BYTES_PER_PACKET * xmaFile.FormatSubchunk.XMAWAVEFormat.NumStreams);

            var blockLengthInBits = BitMath.BitCount(blockLengthInBytes);
            var blockData = xmaStream.ReadBytes(blockLengthInBits);
            var blockStream = new BitStream(blockData);

            this.XMAPackets = new XMAPACKET[xmaFile.FormatSubchunk.XMAWAVEFormat.NumStreams];

            Debug.WriteLine(typeof(XMABLOCK).FullName);
            Debug.Indent();

            for (var p = 0; p < this.XMAPackets.Length; p++)
            {
                Debug.Write($"{nameof(this.XMAPackets)}[{p}]");
                var packet = new XMAPACKET(blockStream, xmaFile);
                this.XMAPackets[p] = packet;
            }

            Debug.Unindent();
        }
    }
}
