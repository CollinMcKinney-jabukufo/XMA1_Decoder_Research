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

        /// <summary>
        /// The offset in bits where this chunk begins (following the <see cref="CHUNK_HEADER"/>).
        /// <para>
        /// This is completely abstract and not an actual part of the <see cref="CHUNK_HEADER"/>.
        /// </para>
        /// </summary>
        public int BitOffset { get; }

        public CHUNK_HEADER(BitStream bitStream, FourCC validTag)
        {
            Debug.WriteLine(typeof(CHUNK_HEADER).FullName);
            Debug.Indent();

            this.ChunkTag = bitStream.ReadValue<FourCC>(Endianness.BE);
            Debug.WriteLine($"{nameof(this.ChunkTag)}: {this.ChunkTag}");
            Assert.Debug(this.ChunkTag == validTag);

            this.ChunkSize = bitStream.ReadValue<int>();
            Debug.WriteLine($"{nameof(this.ChunkSize)}: {this.ChunkSize}");

            this.BitOffset = bitStream.BitOffset;
            Debug.WriteLine($"{nameof(this.BitOffset)}: {this.BitOffset}");

            Debug.Unindent();
        }

        private CHUNK_HEADER() { }
    }
}
