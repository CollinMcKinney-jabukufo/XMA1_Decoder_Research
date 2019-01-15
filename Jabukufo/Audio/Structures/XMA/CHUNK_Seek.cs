using Jabukufo.Bits;
using Jabukufo.Common;
using System.Diagnostics;

namespace Jabukufo.Audio.Structures.XMA
{
    /// <summary>
    /// 'seek' chunk: A seek table to help navigate the XMA data.
    /// </summary>
    public class CHUNK_Seek
    {
        public static readonly FourCC Tag = new FourCC("seek");
        public CHUNK_HEADER Header;

        /// <summary>
        /// Number of interleaved audio streams.
        /// </summary>
        public int NumStreams;

        /// <summary>
        /// The length of <see cref="BlockTable"/>.
        /// </summary>
        public int BlockTableCount;

        /// <summary>
        /// <para>
        /// Entries here tell the amount of PCM samples in all previous "blocks". The amount of PCM samples in <see cref="BlockTable"/>[n]
        /// can be calculated as: (<see cref="BlockTable"/>[n + 1] - <see cref="BlockTable"/>[n]).
        /// </para>
        /// <para>
        /// The <see cref="XMAFRAME"/> count in <see cref="BlockTable"/>[n] can be calculated as: 
        /// (<see cref="BlockTable"/>[n + 1] - <see cref="BlockTable"/>[n]) / <see cref="Constants.XMA_SAMPLES_PER_FRAME"/>.
        /// </para>
        /// <para>
        /// The <see cref="XMASUBFRAME"/> count in <see cref="BlockTable"/>[n] can be calculated as: 
        /// (<see cref="BlockTable"/>[n + 1] - <see cref="BlockTable"/>[n]) / <see cref="Constants.XMA_SAMPLES_PER_SUBFRAME"/>.
        /// </para>
        /// <para>
        /// TODO: Figure out how to find the amount of samples for <see cref="BlockTable"/>[<see cref="BlockTable"/>.Length - 1].
        /// </para>
        /// </summary>
        public int[] BlockTable;

        public CHUNK_Seek(BitContext metaContext, XMAFILE xmaFile)
        {
            Debug.WriteLine(typeof(CHUNK_Seek).FullName);
            Debug.Indent();

            this.Header = new CHUNK_HEADER(metaContext, CHUNK_Seek.Tag);

            this.NumStreams = metaContext.ReadValue<int>();
            Debug.WriteLine($"{nameof(NumStreams)}: {NumStreams}");

            this.BlockTableCount = metaContext.ReadValue<int>();
            Debug.WriteLine($"{nameof(BlockTableCount)}: {BlockTableCount}");

            Debug.WriteLine($"BlockSize: {xmaFile.DataSubchunk.Header.ChunkSize / this.BlockTableCount}");
            Assert.Debug(xmaFile.DataSubchunk.Header.ChunkSize % this.BlockTableCount == 0);

            this.BlockTable = new int[this.BlockTableCount];
            Debug.WriteLine(nameof(this.BlockTable));
            Debug.Indent();
            for (var i = 0; i < this.BlockTable.Length; i++)
            {
                this.BlockTable[i] = metaContext.ReadValue<int>();
                Debug.WriteLine($"[{i.ToString().PadLeft(this.BlockTableCount.ToString().Length, '0')}]: {this.BlockTable[i]}");
                Assert.Debug(this.BlockTable[i] % Constants.XMA_SAMPLES_PER_FRAME == 0);
            }
            Debug.Unindent();

            Debug.Unindent();
        }
    }
}
