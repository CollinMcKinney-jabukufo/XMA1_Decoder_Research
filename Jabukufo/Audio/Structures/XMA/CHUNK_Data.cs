using Jabukufo.Bits;
using Jabukufo.Common;
using System;
using System.Diagnostics;

namespace Jabukufo.Audio.Structures.XMA
{
    /// <summary>
    /// 'data' chunk: The encoded XMA data.
    /// </summary>
    public class CHUNK_Data
    {
        public static readonly FourCC Tag = new FourCC("data");
        public CHUNK_HEADER Header;

        /// <summary>
        /// The actual sound data.
        /// </summary>
        public BitStream XMADataStream;

        public CHUNK_Data(BitStream metaStream, XMAFILE xmaFile)
        {
            Debug.WriteLine(typeof(CHUNK_Data).FullName);
            Debug.Indent();

            this.Header = new CHUNK_HEADER(metaStream, CHUNK_Data.Tag);

            var packetModulus = this.Header.ChunkSize % Constants.XMA_BYTES_PER_PACKET;
            Assert.Debug(packetModulus == 0);

            var bitLength = BitMath.BitCount(this.Header.ChunkSize);
            var xmaData = metaStream.ReadBytes(bitLength);
            this.XMADataStream = new BitStream(xmaData);

            Debug.Unindent();
        }
    }
}
