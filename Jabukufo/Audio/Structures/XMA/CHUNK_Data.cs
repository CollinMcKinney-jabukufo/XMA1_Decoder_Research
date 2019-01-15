using Jabukufo.Bits;
using Jabukufo.Common;
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
        public BitContext XMAContext;

        public CHUNK_Data(BitContext metaContext, XMAFILE xmaFile)
        {
            Debug.WriteLine(typeof(CHUNK_Data).FullName);
            Debug.Indent();

            this.Header = new CHUNK_HEADER(metaContext, CHUNK_Data.Tag);

            var packetModulus = this.Header.ChunkSize % Constants.XMA_BYTES_PER_PACKET;
            Assert.Debug(packetModulus == 0);

            var bitLength = BitMath.BitCount(this.Header.ChunkSize);
            this.XMAContext = metaContext.GetBits(bitLength, false);

            Debug.Unindent();
        }
    }
}
