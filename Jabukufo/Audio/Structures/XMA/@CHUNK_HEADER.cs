using Jabukufo.Bits;
using Jabukufo.Common;
using System.Diagnostics;

namespace Jabukufo.Audio.Structures.XMA
{
    /// <summary>
    /// Determines the type and size of the chunk.
    /// </summary>
    public class CHUNK_HEADER
    {
        /// <summary>
        /// The <see cref="FourCC"/> tag that signifies the <see cref="SUBCHUNK"/> type.
        /// </summary>
        public FourCC ChunkTag;

        /// <summary>
        /// The size in bytes of the chunk (not counting this <see cref="CHUNK_HEADER"/>).
        /// </summary>
        public int ChunkSize;

        public CHUNK_HEADER(BitContext metaContext, FourCC validTag)
        {
            Debug.WriteLine(typeof(CHUNK_HEADER).FullName);
            Debug.Indent();

            this.ChunkTag = metaContext.ReadValue<FourCC>(Endianness.BE);
            Debug.WriteLine($"{nameof(this.ChunkTag)}: {this.ChunkTag}");
            Assert.Debug(this.ChunkTag == validTag);

            this.ChunkSize = metaContext.ReadValue<int>();
            Debug.WriteLine($"{nameof(this.ChunkSize)}: {this.ChunkSize}");

            Debug.Unindent();
        }

        private CHUNK_HEADER() { }
    }
}
